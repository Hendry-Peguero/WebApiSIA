namespace WebApiSIA.Core.Application.Dtos.User
{
    public class LoginRequestDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
