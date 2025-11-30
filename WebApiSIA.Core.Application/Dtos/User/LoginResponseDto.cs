namespace WebApiSIA.Core.Application.Dtos.User
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
