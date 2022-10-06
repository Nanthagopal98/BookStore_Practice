using BusinessLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ModelLayer;
using System;

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
    }
}
