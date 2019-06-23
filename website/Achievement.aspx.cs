using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Achievement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "成就";
        }

        String done = Request.QueryString["done"];
        if (done != null && done.Equals("1"))
        {
            XmlDatabase database = XmlDatabase.GetInstance;
            Int32 groupid = -1;
            Int32 acheid = -1;

            try
            {
                groupid = Convert.ToInt32(Request.QueryString["gid"]);
                acheid = Convert.ToInt32(Request.QueryString["aid"]);
            }
            catch (Exception ex)
            {
                title.Text = ex.ToString();
            }

            database.SetAchievementDone(groupid, acheid);
        }

        AchievementDataSource.DataBind();
    }
}