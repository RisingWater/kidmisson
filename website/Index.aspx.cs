using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "首页";
        }

        String userid = HttpUtils.GetUserIdInCookies(Request);
        if (userid != null)
        {
            Response.Redirect(@"~/Mission.aspx");
        }
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        Response.Redirect(@"~/Login.aspx");
    }
}