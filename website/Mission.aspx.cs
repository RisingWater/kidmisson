using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    private String m_szUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "任务";
        }

        m_szUserId = HttpUtils.GetUserIdInCookies(Request);

        if (m_szUserId == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }

        WeekController Control = new WeekController(m_szUserId);

        weeky_point.Text = Control.Progress.ToString();
        Int32 Percent = (Control.Progress * 100 / Control.Target);
        if (Percent > 100)
        {
            Percent = 100;
        }
        weeky_progress.Style["width"] = Percent + "%";

        weeky_award_text.Text = WeekMissionText(Control.Progress, Control.Target, Control.Done);

        if (Control.Done || Control.Progress < Control.Target)
        {
            weeky_button.Style["display"] = "none";
        }

        DailyMissionDataSource.DataBind();
    }

    private String WeekMissionText(Int32 Progress, Int32 Target, Boolean Done)
    {
        if (Done)
        {
            return "累计积分目标已经达成，太棒啦";
        }
        else
        {
            if (Progress >= Target)
            {
                return "请领取奖励";
            }
            else
            {
                Int32 rest = Target - Progress;
                return "再获得" + rest + "积分，就可获得奖励";
            }
        }
    }

    protected void WeekyAward_Click(object sender, EventArgs e)
    {
        String Item = "";
        WeekController Control = new WeekController(m_szUserId);
        if (Control.GetAward(ref Item))
        {
            String Text = "获得每周任务奖励，" + Item;
            RecordController.AddDetail(m_szUserId, ModelParam.AWARD_RECORD_ID, Text, 0);

            Response.Redirect("~/AwardGet.aspx?Image=~/image/point_big.png"
                    + "&Target=每周任务奖励"
                    + "&Award=" + Item
                    + "&BackUrl=~/Mission.aspx");
        }
    }

    public String GetMissionImage(String ImageFileName)
    {
        return "image/" + ImageFileName;
    }

    public String GetButtonText(Boolean Done)
    {
        if (Done)
        {
            return "已完成";
        }
        else
        {
            return "完成任务";
        }
    }

    public String GetButtonColorStyle(Boolean Done)
    {
        if (Done)
        {
            return "background-color:#2dd700";
        }
        else
        {
            return "background-color:#3887c5";
        }
    }

    public string GetJavaScript(int item_index)
    {
        String js = @"JAVAScript:document.getElementById('MainContent_DailyMissionRepeater_completeMission_" + item_index + "').click();";
        return js;
    }

    protected void DailyMissionRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "completeMission")
        {
            String Text = null;
            Int32 MissionId = Convert.ToInt32(e.CommandArgument);

            DailyMissionController Controller = DailyMissionsController.GetMission(m_szUserId, MissionId);

            Controller.SetDone();

            Text = "完成任务\"" + Controller.Name + "\", 获得积分" + Controller.Award + "分";
            RecordController.AddDetail(m_szUserId, ModelParam.POINT_GET_RECORD_ID, Text, Controller.Award);

            DailyMissionRepeater.DataBind();

            ClientScript.RegisterStartupScript(ClientScript.GetType(),
                "GetAwardScript",
                "<script>swal('" + Text + "').then((value) => {window.location.href='Mission.aspx';});</script>");
        }
    }
}
