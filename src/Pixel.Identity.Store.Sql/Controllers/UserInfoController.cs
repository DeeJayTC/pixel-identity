﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pixel.Identity.Core.Controllers;

namespace Pixel.Identity.Store.Sql.Controllers
{
    /// <summary>
    /// Api endpoint for retrieving user info
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : UserinfoController<ApplicationUser>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="userManager"></param>
        public UserInfoController(UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
        }
    }
}