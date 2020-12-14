using Ioasys.IMDb.API.Configurations;
using Ioasys.IMDb.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Ioasys.IMDb.API.Controllers
{

    public class UsuarioController : AuthController
    {
        public UsuarioController(SignInManager<IdentityUser> signInManager,
                                     UserManager<IdentityUser> userManager,
                                     IOptions<AppSettings> appSettings) : base(signInManager, userManager, appSettings)
        {
        }

        [HttpPost]
        public Task<ActionResult> Registrar(InfoUserViewModel infoUser)
        {
            infoUser.Role = RoleType.USUARIO;

            return RegistrarUsuario(infoUser);
        }
    }
}
