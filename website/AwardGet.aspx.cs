using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AwardGet : System.Web.UI.Page
{
    public String m_szBackUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "奖励";
        }

        if (HttpUtils.GetUserIdInCookies(Request) == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }

        String imagePath = Request.QueryString["Image"];

        BoxImage.ImageUrl = imagePath;

        String Target = Request.QueryString["Target"];

        BoxName.Text = Target;

        String Award = Request.QueryString["Award"];

        ItemName.Text = Award;

        String BackUrl = Request.QueryString["BackUrl"];

        m_szBackUrl = BackUrl;
    }

    protected void Back_Click(object sender, EventArgs e)
    {
        if (m_szBackUrl != null)
        {
            Response.Redirect(m_szBackUrl);
        }
        else
        {
            Response.Redirect("~/Index.aspx");
        }
    }
}