using Microsoft.Extensions.Configuration;
using System.IO;

namespace DistribuidoraWalter.Data
{
    public class DBConnection
    {
        /// <summary>
        /// Retorna la cadena de conexión según el nombre especificado en appsettings.json
        /// </summary>
        /// <param name="connectionName">Nombre de la conexión ("ConnectionString" o "ConnectionStringDW")</param>
        /// <returns>Cadena de conexión correspondiente</returns>
        public static string Connect(string connectionName = "ConnectionString")
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var connection = configuration.GetConnectionString(connectionName);

            if (string.IsNullOrEmpty(connection))
            {
                throw new System.Exception($"No se encontró la cadena de conexión para '{connectionName}' en appsettings.json.");
            }

            return connection;
        }
    }
}
