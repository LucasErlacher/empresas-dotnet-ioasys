namespace Ioasys.IMDb.API.ViewModels
{
    public class InfoUserViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }

        public RoleType Role { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; internal set; }
        public double ExpiresIn { get; internal set; }
    }

    public enum RoleType
    {
        ADMINISTRADOR,
        USUARIO
    }
}
