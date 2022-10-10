using BusinessLayer.Interface;
using ModelLayer;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iUserRL;
        public UserBL(IUserRL iUserRL)
        {
            this.iUserRL = iUserRL;
        }

        public UserEntity Registration(UserModel userModel)
        {
            try
            {
                return iUserRL.Registration(userModel);
            }
            catch
            {
               throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return this.iUserRL.Login(userLoginModel);
            }
            catch
            {
                throw;
            }
        }
        public bool ResetPassord(string email)
        {
            try
            {
                return iUserRL.ResetPassord(email);
            }
            catch
            {
                throw;
            }
        }
    }
}
