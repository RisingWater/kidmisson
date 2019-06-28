using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class HttpUtils
{
    static public String GetUserIdInCookies(HttpRequest Request)
    {
        HttpCookie cookie = Request.Cookies["userid"];
        if (cookie == null)
        {
            return null;
        }

        return cookie.Value;
    }
}