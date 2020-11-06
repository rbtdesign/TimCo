using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    //[RoutePrefix("api/User")]
    public class UserController : ApiController
    {

        // GET: User/Id
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId(); //grabd the id from the logged in user instead of requesting the id. Otherwise you can lookup anyone
            UserData data = new UserData();

            return data.GetUserById(userId).First();

        }

      
    }
}
