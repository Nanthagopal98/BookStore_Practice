using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ModelLayer;
using System;
using System.Net.Http;

namespace BookStore_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Registration(UserModel userModel)
        {
            try
            {
                var result = userBL.Registration(userModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "User Added Successfully", data = result });
                }
                return BadRequest(new {success = false, mesage = "Failed To Register"});
            }
            catch(System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            try
            {
                var result = this.userBL.Login(userLoginModel);
                if(result != null)
                {
                    return Ok(new { success = true, message = "Logged in Successfully", data = result });
                }
                return BadRequest(new { success = false, message = " Loggin Failed" });
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        [Route("reset")]
        public IActionResult Reset(string email)
        {
            try
            {
                var result = userBL.ResetPassord(email);
                if(result != false)
                {
                    return Ok(new { success = true, meaasge = "Reset Link Shared" });
                }
                return BadRequest(new { success = false, message = "Password Reset Failed" });
            }
            catch(System.Exception)
            {
                throw;
            }
        }
    }
}
