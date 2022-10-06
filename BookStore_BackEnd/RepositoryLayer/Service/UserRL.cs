using ModelLayer;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly BookStoreContext bookStoreContext;
        public UserRL(BookStoreContext bookStoreContext)
        {
            this.bookStoreContext = bookStoreContext;
        }
        public UserEntity Registration(UserModel userModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FullName = userModel.FullName;
                userEntity.Email = userModel.Email;
                userEntity.Password = userModel.Password;
                userEntity.Phone = userModel.Phone;
                bookStoreContext.Users.Add(userEntity);
                int result = bookStoreContext.SaveChanges();
                if(result != 0)
                {
                    return userEntity;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
