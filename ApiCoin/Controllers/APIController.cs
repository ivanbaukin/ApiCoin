using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace ApiCoin.Controllers
{
    public class APIController : Controller
    {

        private static string API_KEY = "write your API";

        public string makeAPICall()
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["start"] = "1";
            queryString["convert"] = "USD";
            queryString["limit"] = "100";

            URL.Query = queryString.ToString();

            var client = new WebClient();

            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(URL.ToString());
        }

        public string makeAPILogo(string slug)
        {
            var URL = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["slug"] = slug;

            URL.Query = queryString.ToString();

            var client = new WebClient();

            client.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            client.Headers.Add("Accepts", "application/json");

            return client.DownloadString(URL.ToString());
        }

    }
}