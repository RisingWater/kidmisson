using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Achievement : System.Web.UI.Page
{
    private String m_szUserId = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "成就";
        }

        m_szUserId = HttpUtils.GetUserIdInCookies(Request);
        if (m_szUserId == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }
    }

    public String GetProgresBarStyle(Int32 progress, Int32 target)
    {
        Int32 percent = progress * 100 / target;
        if (percent > 100)
        {
            percent = 100;
        }

        return "width:" + percent + "%";
    }

    public String GetButtonStyle(Boolean Done, Int32 progress, Int32 target)
    {
        if (progress < target)
        {
            return "display:none";
        }
        else
        {
            if (Done)
            {
                return "display:none";
            }
            else
            {
                return "display:block";
            }
        }
    }

    public string GetJavaScript(int item_index)
    {
        String js = @"JAVAScript:document.getElementById('MainContent_AchievementRepeater_getaward_" + item_index + "').click();";
        return js;
    }

    protected void AchievementRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "getaward")
        {
            String Text = null;
            Int32 award_group_id = Convert.ToInt32(e.CommandArgument);

            AchievementGroupController Controller = AchievementController.GetAchievementGroup(m_szUserId, award_group_id);

            String desc = Controller.Description;
            Int32 point = Controller.Award;

            Controller.GetAward();
            
            Text = "获得成就\"" + desc + "\"";
            RecordController.AddDetail(m_szUserId, ModelParam.POINT_GET_RECORD_ID, Text, point);

            AchievementRepeater.DataBind();

            ClientScript.RegisterStartupScript(ClientScript.GetType(),
                "GetAwardScript",
                "<script>swal('" + Text + "').then((value) => {window.location.href='Achievement.aspx';});</script>");
        }
    }
}