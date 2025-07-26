using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTO_S;

namespace Services
{
    public class AuthenticationService(IUnitOfWork unitOfWork, IConfiguration configuration) : IAuthenticationService
    {
        public async Task<string> LoginAsync(UserLoginDto Udto)
        {
            var UserRepo = unitOfWork.GetRepository<User, int>();

            var User = (await UserRepo.GetAllAsync()).FirstOrDefault(U => U.UserName == Udto.UserName);

            if (User is null)
                throw new Exception("User not found.");

            if (!VerifyPassword(Udto.Password, User.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            return GenerateToken(User);
        }
        public async Task<string> RegisterAsync(UserRegisterDto UserDto)
        {
            var UserRepo = unitOfWork.GetRepository<User, int>();

            var ExistingUser = (await UserRepo.GetAllAsync()).FirstOrDefault(U => U.UserName == UserDto.UserName);
        
            if(ExistingUser is not null)
                throw new Exception("Username Already Exists");

            var PasswordHash = HashPassword(UserDto.Password);

            var User = new User()
            {
                UserName = UserDto.UserName,
                PasswordHash = PasswordHash,
                Role = Enum.Parse<Roles>(UserDto.Role, true)
            };

            await UserRepo.AddAsync(User);

            await unitOfWork.SaveChangesAsync();

            return GenerateToken(User);
        }

        private string GenerateToken(User user)
        {
            var Claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, user.Role.ToString())
            };

            var secretKey = configuration["JWTOptions:SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string HashPassword(string password)
        {
            using var Hash = SHA256.Create();

            var HashedPass = Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(HashedPass);
        }
        private bool VerifyPassword(string password, string passwordHash)
        {
            var hashedInput = HashPassword(password);

            return passwordHash == hashedInput;
        }
    }
}
