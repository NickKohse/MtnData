using MtnData.Models;
using MtnData.Models.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MtnData.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            UserConnect uc = new UserConnect();
            LoginMessage message = uc.Login(username, password);

            if (message.GetResult())
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.serverResponse = message.GetText();
                return View("Index");
            }
        }
    }
}