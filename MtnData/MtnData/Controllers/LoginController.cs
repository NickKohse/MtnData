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

        public ActionResult NewAccount()
        {
            return View();
        }

        public ActionResult NewAcct(string username, string password, string passwordConfirm, string email, string name)
        {
            if (password != passwordConfirm)
            {
                ViewBag.createResponse = "Passwords must match";
                return View("NewAccount");
            }
            UserConnect userConnect = new UserConnect();

            Message message = userConnect.AddUser(name, username, password, email);

            if (message.GetResult())
            {
                return View("Created");
            }
            else
            {
                ViewBag.createResponse = message.GetText();
                return View("NewAccount");
            }

            
        }
    }
}