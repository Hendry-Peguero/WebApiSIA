using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiSIA.Core.Application.Dtos.User;
using WebApiSIA.Core.Application.Interfaces.Helpers;
using WebApiSIA.Core.Application.Interfaces.Repositories;
using WebApiSIA.Core.Application.Interfaces.Services;
using WebApiSIA.Core.Domain.Entities;

namespace WebApiSIA.Core.Application.Services
{
    public class UserService : GenericService<SaveUserDto, UserDto, UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMd5Helper _md5Helper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IMd5Helper md5Helper
        )
        : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _md5Helper = md5Helper;
        }

        public async Task<LoginResponseDto?> LoginAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            var user = await _userRepository.GetByUserNameAsync(userName.Trim());
            if (user == null) return null;

            var dbHash = user.Password ?? string.Empty;
            var isValid = _md5Helper.VerifyMd5(password, dbHash);

            if (!isValid)
                return null;

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                USER_ID = user.USER_ID,                       
                UserName = user.UserName ?? string.Empty, 
                Token = token                             
            };
        }


        private string GenerateJwtToken(UserEntity user)
        {
            var keyValue = Environment.GetEnvironmentVariable("JWT_KEY")
                ?? throw new Exception("JWT_KEY no está configurado");

            var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var expireMinutes = int.Parse(
                Environment.GetEnvironmentVariable("JWT_EXPIRE_MINUTES") ?? "60"
            );

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(keyValue)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.USER_ID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Role, user.Privilege ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
