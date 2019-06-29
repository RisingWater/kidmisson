using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class History : System.Web.UI.Page
{
    private String m_szUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "历史记录";
        }

        m_szUserId = HttpUtils.GetUserIdInCookies(Request);
        if (m_szUserId == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }
    }

    public string GetPoint(int point)
    {
        if (point == 0)
        {
            return "";
        }
        else
        {
            return "+" + point.ToString() + "分";
        }
    }
}