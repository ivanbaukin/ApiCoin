using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApiCoin.Models;
using System.Configuration;
using System.Web.Security;
using Newtonsoft.Json;

namespace ApiCoin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }

        [Authorize]
        public ActionResult ApiCoin()
        {
            APIController api = new APIController();

            //get json
            var result = JsonConvert.DeserializeObject<dynamic>(api.makeAPICall());

            //get list slug
            string slug = "";
            foreach (var item in result.data)
            {
                slug += item.slug + ",";
            }

            var logo = JsonConvert.DeserializeObject<dynamic>(api.makeAPILogo(slug.TrimEnd(',')));

            //adding in json "logo": url_img_coin.png
            foreach (var item in result.data)
            {
                foreach (var l in logo.data)
                {
                    if (item.id == l.First.id)
                    {
                        item.logo = l.First.logo;
                        break;
                    }
                }
            }

            ViewBag.Api = result;
            return View();
        }
    }
}