
namespace WebApiSIA.Core.Application.Dtos.User
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public string? Privilege { get; set; }
        public DateTime? RegDate { get; set; }
        public string? Password { get; set; }
        public string? CanAdd { get; set; }
        public string? CanEdit { get; set; }
        public string? CanDelete { get; set; }
        public string? CanPrint { get; set; }
    }

}
