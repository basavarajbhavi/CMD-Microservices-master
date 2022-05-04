using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ApplicationOAuthProvider _provider = new ApplicationOAuthProvider();
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel model)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3
            };
            IdentityResult result = manager.Create(user, model.Password);
            return result;
        }

        [HttpGet]
        [Route("api/doctorId")]
        public IHttpActionResult GetDoctorId()
        {
            var identityClaims = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identityClaims.Claims;
            var username = identityClaims.FindFirst("Username").Value;
            var doctorId = _context.Users.Where(
                user => user.UserName == username)
                .Select(user => user.DoctorId).FirstOrDefault();
            return Ok(doctorId);
        }
    }
}
