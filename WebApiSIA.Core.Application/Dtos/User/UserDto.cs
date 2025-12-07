
namespace WebApiSIA.Core.Application.Dtos.User
{
    public class UserDto
    {
        public int USER_ID { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Privilege { get; set; }
        public DateTime? RegDate { get; set; }
        public string? Password { get; set; }
        public string? Can_Add { get; set; }
        public string? Can_Edit { get; set; }
        public string? Can_Delete { get; set; }
        public string? Can_Print { get; set; }
    }
}
