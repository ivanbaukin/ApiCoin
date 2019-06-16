using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApiCoin.Models;
using System.Configuration;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace ApiCoin.Controllers
{
    public class AccountController : Controller
    {
        ///Block Registration
        public ActionResult Registration()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("~/Home/ApiCoin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(RegModel model)
        {
            if (ModelState.IsValid)
            {
                User userReg = null;
                using (UserDataContext db = new UserDataContext())
                {
                    userReg = db.Users.FirstOrDefault(u => u.Email == model.Email);
                }
                if (userReg == null)
                {
                    //creating new user
                    using(UserDataContext db = new UserDataContext())
                    {
                        //creating hash md5
                        string pwd = CreateMD5(model.Password);

                        db.Users.Add(new User { Email = model.Email, Password = pwd });
                        db.SaveChanges();

                        userReg = db.Users.Where(u => u.Email == model.Email && u.Password == pwd).FirstOrDefault();
                    }

                    //if user suсcessfully added in db
                    if (userReg != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, true);
                        return RedirectToAction("ApiCoin", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "This Email already exists");
                }
            }

            return View(model);
        }
        ///Block Registration END
        

        ///Block Login
        [HttpGet]
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Redirect("~/Home/ApiCoin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //search user in db
                User user = null;
                using(UserDataContext db = new UserDataContext())
                {
                    string pwd = CreateMD5(model.Password);
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == pwd);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("ApiCoin", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "This email and password does not exist");
                }
            }

            return View(model);
        }
        ///Block Login END

        
        ///Block MD5
        private string CreateMD5(string getPassword)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(getPassword);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        ///Block MD5 END
    }
}