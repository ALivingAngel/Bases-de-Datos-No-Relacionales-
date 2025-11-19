using DistribuidoraWalter.Data.Repositories;
using DotNetOpenAuth.OAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System.Text;
using WebApi.Middleware;
using WebApiData.Authorization;
using WebApiData.Repository;
using WebApiDatas.Interface;
using WebApiDatas.MongoDB;
using WebApiDatas.Repository;
using WebApiDatas.Settings;
using WebModel.Configuration;
using WebModel.Security;
using MemoryMetricSettings = WebApiDatas.Repository.MemoryMetricSettings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// ------------------ CONFIGURACIÓN BASE DE DATOS ------------------
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

// Registro del contexto con la cadena de conexión (Azure SQL)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));
// ------------------ REPOSITORIOS PERSONALIZADOS ------------------
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CompraRepository>();
builder.Services.AddScoped<DevolucionCompraRepository>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();

// Asegurar que la sección coincida con appsettings.json
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Registrar MongoClient usando las opciones
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.connectionString);
});

// Registrar IMongoDatabase para que repositorios que lo requieran lo obtengan
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = new MongoClient(settings.connectionString);
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddScoped<IFailedLoginRepository, FailedLoginRepository>();
// ------------------ IDENTIDAD Y ROLES ------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ------------------ AUTORIZACIÓN ------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Gerente", policy => policy.RequireRole("Gerente"));
    options.AddPolicy("Vendedor", policy => policy.RequireRole("Vendedor"));
});

// ------------------ JWT CONFIG ------------------
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JwtConfig"));
var secret = builder.Configuration.GetValue<string>("JwtConfig:Secret");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        RequireExpirationTime = false
    };
});

// ------------------ CORS ------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// ------------------ CONTROLADORES Y SWAGGER ------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer eyJhb...'"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IMemoryMetricRepository, MemoryMetricRepository>();
builder.Services.Configure<MemoryMetricSettings>(
    builder.Configuration.GetSection("MemoryMetrics"));

builder.Services.AddHostedService<MemoryMetricsService>();

builder.Services.AddScoped<IResponseTimeLogRepository, ResponseTimeLogRepository>();

// ------------------ CACHE EN MEMORIA ------------------
builder.Services.AddMemoryCache();

// ------------------ BUILD ------------------
var app = builder.Build();

//Registro del servicio de seguridad

// ------------------ APLICAR MIGRACIONES AUTOMÁTICAMENTE EN AZURE ------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try
    {
        if (dbContext.Database.CanConnect())
        {
            dbContext.Database.Migrate();
            Console.WriteLine("✅ Migraciones aplicadas correctamente en Azure SQL");
        }
        else
        {
            Console.WriteLine("⚠ No se pudo conectar a la base de datos. Verifica tu cadena de conexión.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error aplicando migraciones: {ex.Message}");
    }
}

// ------------------ MIDDLEWARE ------------------
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ---------------------------------------------------------------
// ---------------------------------------------------------------

app.Run();