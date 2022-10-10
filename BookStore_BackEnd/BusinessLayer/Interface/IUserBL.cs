using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(UserModel userModel);
        public string Login(UserLoginModel userLoginModel);
        public bool ResetPassord(string email);
    }
}
