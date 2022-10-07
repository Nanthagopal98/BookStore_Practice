﻿using ModelLayer;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Registration(UserModel userModel);
        public string Login(UserLoginModel userLoginModel);
    }
}
