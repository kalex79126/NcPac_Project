﻿namespace NcPac_Project2.Utilities
{
    public static class MaintainURL
    {
        public static string ReturnURL(HttpContext httpContext, string ControllerName)
        {
            string cookieName = ControllerName + "URL";
            string SearchText = "/" + ControllerName + "?";
            //Get the URL of the page that sent us here
            string returnURL = httpContext.Request.Headers["Referer"].ToString();
            if (returnURL.Contains(SearchText))
            {
                //Came here from the Index with some parameters
                //Save the Parameters in a Cookie
                returnURL = returnURL[returnURL.LastIndexOf(SearchText)..];
                CookieHelper.CookieSet(httpContext, cookieName, returnURL, 50);
                return returnURL;
            }
            else
            {
                //Get it from the Cookie
                //Note that this might return an empty string but we will
                //change it to go back to the Index of the Controller.
                returnURL = httpContext.Request.Cookies[cookieName];
                return returnURL ?? "/" + ControllerName;
            }
        }
    }
}
