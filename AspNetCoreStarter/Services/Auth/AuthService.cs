using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreStarter.Configuration;
using AspNetCoreStarter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCoreStarter.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationConfig _authConfig;

        public AuthService(UserManager<ApplicationUser> userManager,
            IOptions<AuthenticationConfig> authConfig)
        {
            _userManager = userManager;
            _authConfig = authConfig.Value;
        }

        public async Task<object> GenerateJwtAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            }.Union(userClaims);

            var symmetricKeyAsBase64 = _authConfig.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(_authConfig.ExpiresInMinutes)),
                signingCredentials: signingCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return new
            {
                token = encodedJwt,
                expires = jwtSecurityToken.ValidTo,
            };
        }
    }
}
