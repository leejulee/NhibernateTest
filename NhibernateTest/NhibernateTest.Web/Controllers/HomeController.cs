using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhibernateTest.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserService _userService;
        
        public HomeController()
        {
            this._userService = new UserService();
        }
        // GET: Home
        public ActionResult Index()
        {
            ServiceFactory.MessageService.Add(new Message());
            var models = _userService.GetAll();
            return Content("Ok");
        }
    }
}