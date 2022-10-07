using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly BookStoreContext bookStoreContext;
        public IConfiguration Configuration { get; }
        public UserRL(BookStoreContext bookStoreContext, IConfiguration Configuration)
        {
            this.bookStoreContext = bookStoreContext;
            this.Configuration = Configuration;
        }
        
        public UserEntity Registration(UserModel userModel)
        {
            try
            {
                var checkEmail = this.bookStoreContext.Users.Where(e => e.Email == userModel.Email).FirstOrDefault();
                if (checkEmail == null)
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FullName = userModel.FullName;
                    userEntity.Email = userModel.Email;
                    userEntity.Password = userModel.Password;
                    userEntity.Phone = userModel.Phone;
                    bookStoreContext.Users.Add(userEntity);
                    int result = bookStoreContext.SaveChanges();
                    if (result != 0)
                    {
                        return userEntity;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                var findUser = this.bookStoreContext.Users.Where(e => e.Email == userLoginModel.Email && e.Password == userLoginModel.Password).FirstOrDefault();
                if(findUser != null)
                {
                    var token = GenerateSecurityToken(findUser.Email, findUser.UserId);
                    return token;
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public string GenerateSecurityToken(string email, int Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration[("JWT:Key")]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Role, "Users"),
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id", Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
