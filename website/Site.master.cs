using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String userid = HttpUtils.GetUserIdInCookies(Request);
        if (userid != null)
        {
            try
            {
                PocketController Controller = new PocketController(userid);
                RestPoint.Text = "剩余积分: " + Controller.Point + "分";
            }
            catch
            {
                RestPoint.Text = "";
                RestPoint.Visible = false;
            }
        }
        else
        {
            RestPoint.Text = "";
            RestPoint.Visible = false;
        }
    }
}
