﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        //Here I'll return DETAILS of the User's Profile, based on the Token. So:
        //It will have to be Logged to return data (Token will be required).

        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET: /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                //anonymous object here...
                user.FullName,
                user.Email,
                user.UserName
            };
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] //note - i can have more than one role, separeted by comma
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web Method for Admin";
        }

        [HttpGet]
        [Authorize(Roles = "Customer")] //note - i can have more than one role, separeted by comma
        [Route("ForCustomer")]
        public string GetForCustomer()
        {
            return "Web Method for Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")] //note - i can have more than one role, separeted by comma
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web Method for Admin or Customer";
        }
    }
}