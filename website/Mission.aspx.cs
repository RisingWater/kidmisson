using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "任务";
        }

        XmlDatabase database = XmlDatabase.GetInstance;
        String done = Request.QueryString["done"];
        if (done != null && done.Equals("1"))
        {
            Int32 missoinId = -1;

            try
            {
                missoinId = Convert.ToInt32(Request.QueryString["missionindex"]);
            }
            catch (Exception ex)
            {
                title.Text = ex.ToString();
            }

            database.SetDailyMissionDone(missoinId);
        }

        DateTime time = DateTime.Now;
        GregorianCalendar calendar = new GregorianCalendar();
        int weekOfYears = calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        String weekKey = time.Year + "-" + weekOfYears;


        WeekInfo week = database.GetWeekInfo(weekKey);

        weeky_point.Text = week.CurrentPoint.ToString();
        weeky_progress.Style["width"] = (week.CurrentPoint * 100 / 500) + "%";

        weeky_award_text.Text = week.WeekMissionText();
        Int32 awardId = -1;
        awardId = week.CanbeAward();

        if (awardId < 0)
        {
            weeky_button.Style["display"] = "none";
        }
        else
        {
            GetAward.NavigateUrl = "~/AwardGet.aspx?week_award=" + awardId;
        }

        DailyMissionDataSource.DataBind();
    }
}
