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

        if (cookie.Value == null)
        {
            return null;
        }

        CertificationController c = new CertificationController();
        if (c.IsUserIdValid(cookie.Value))
        {
            return cookie.Value;
        }
        else
        {
            return null;
        }
    }
}