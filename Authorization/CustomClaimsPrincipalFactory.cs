using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using WebApiData.Repository;
using WebModel.Security;

namespace WebApiData.Authorization
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly RolRepository _rolesRepository;

        public CustomClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            RolRepository rolesRepository,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _rolesRepository = rolesRepository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            // Obtener los roles personalizados del usuario por su Id
            var roles = _rolesRepository.GetUsuarioRol(user.Id);

            foreach (var rol in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, rol));
            }

            return identity;
        }
    }
}
