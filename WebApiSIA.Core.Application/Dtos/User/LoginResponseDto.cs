namespace WebApiSIA.Core.Application.Dtos.User
{
    public class LoginResponseDto
    {
        public int USER_ID { get; set; }
        public string UserName { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
