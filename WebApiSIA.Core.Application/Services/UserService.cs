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
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IMd5Helper _md5Helper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IMd5Helper md5Helper,
            IConfiguration config
        )
        : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _md5Helper = md5Helper;
            _config = config;
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
                UserName = user.UserName ?? string.Empty,
                Token = token
            };
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Role, user.Privilege ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_config["Jwt:ExpireMinutes"] ?? "60")
                ),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
