using System.Threading.Tasks;
using WebApiSIA.Core.Application.Dtos.User;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserDto, UserDto, UserEntity>
    {
        Task<LoginResponseDto?> LoginAsync(string userName, string password);
    }
}
