using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using tutorial10.DTOs;
using tutorial10.Helpers;
using tutorial10.Models;
using tutorial10.Models.AuthUser;

namespace tutorial10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase

    {
        private readonly S20697Context _context;
        private readonly IConfiguration _confugiration;

        public AccountsController(S20697Context context,IConfiguration configuration)
        {
            _confugiration= configuration;
            _context= context;
        }

        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task Register(Register reg)
        {
            var hasPassSalt = SecurityHelper.GetHashPasswordSalt(reg.Password);

            var AppUser = new AuthUsers()
            {
                Username = reg.Username,
                Password = hasPassSalt.Item1,
                Salt = hasPassSalt.Item2,
                RefreshToken = SecurityHelper.RefreshToken(),
                RefreshTokenExpiration = DateTime.Now.AddMinutes(10)
            };
            await _context.Users.AddAsync(AppUser);
            await _context.SaveChangesAsync();
        }
       
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<AccountResponseDto> Login(User_DTO loginRequest)
        {
            AuthUsers appuser = _context.Users.FirstOrDefault(x => x.Username == loginRequest.Username);

            string passwordHash = appuser.Password;
            string currentHashPass = SecurityHelper.GetPasswordWithSalt(loginRequest.Password, appuser.Salt);

            if(passwordHash != currentHashPass)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                Claim[] claims = new[]
                {
                    new Claim(ClaimTypes.Name,"harsh"),
                    new Claim(ClaimTypes.Role,"admin"),
                    new Claim(ClaimTypes.Role,"user")
                };
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confugiration["Secret"]));

                SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new JwtSecurityToken(
                     issuer: "http://localhost:53959",
                    audience: "http://localhost:53959",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                    );

                appuser.RefreshToken = SecurityHelper.RefreshToken();
                appuser.RefreshTokenExpiration= DateTime.Now.AddDays(1);
                await _context.SaveChangesAsync();

                var result = new AccountResponseDto
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = appuser.RefreshToken,
                };
                return result;
            }
        }

        [AllowAnonymous]
        [Route("Refresh")]
        [HttpPost]
        public async Task<AccountResponseDto> Refresh(string token,RefreshTokenRequest refreshToken)
        {
            AuthUsers appUser = _context.Users.FirstOrDefault(x=>x.RefreshToken==refreshToken.RefreshToken);
            if (appUser==null||appUser.RefreshTokenExpiration < DateTime.Now)
            {
                throw new SecurityTokenException("Invalid");
            }
            var login = SecurityHelper.GetUserIdFromAccToken(token.Replace("Bearer ", ""), _confugiration["Secret"]);

            Claim[] claims = new[]
            {
                    new Claim(ClaimTypes.Name,"Prashant"),
                    new Claim(ClaimTypes.Role,"admin"),
                    new Claim(ClaimTypes.Role,"user")
                };
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confugiration["Secret"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken newTok = new JwtSecurityToken(
                issuer: "http://localhost:53959",
                audience: "http://localhost:53959",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            appUser.RefreshToken = SecurityHelper.RefreshToken();
            appUser.RefreshTokenExpiration = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            var result = new AccountResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newTok),
                RefreshToken = appUser.RefreshToken
            };
            return result;
        }
    }
}
