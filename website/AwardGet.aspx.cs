using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AwardGet : System.Web.UI.Page
{
    private Boolean InitByOpenId(String openid)
    {
        Int32 id = Convert.ToInt32(openid);

        XmlDatabase database = XmlDatabase.GetInstance;
        String text = "";

        switch (id)
        {
            case 1:
                text = database.OpenBronzeBox();
                BoxImage.ImageUrl = @"image/bronze.png";
                BoxName.Text = @"铜宝箱";
                break;
            case 2:
                text = database.OpenSliverBox();
                BoxImage.ImageUrl = @"image/sliver.png";
                BoxName.Text = @"银宝箱";
                break;
            case 3:
                text = database.OpenGoldBox();
                BoxImage.ImageUrl = @"image/gold.png";
                BoxName.Text = @"金宝箱";
                break;
            case 4:
                text = database.OpenDiamondBox();
                BoxImage.ImageUrl = @"image/diamond.png";
                BoxName.Text = @"白金宝箱";
                break;
            default:
                break;
        }

        if (text == null)
        {
            text = "积分不够啦，开启失败";
        }

        ItemName.Text = text;

        return true;
    }

    private Boolean InitByWeekAwardId(String weekawardId)
    {
        Int32 id = Convert.ToInt32(weekawardId);

        XmlDatabase database = XmlDatabase.GetInstance;
        String text = "";
        BoxName.Text = @"每周积分奖励";

        text = database.OpenWeekyAward(id);

        switch (id)
        {
            case 0:
            case 1:
                BoxImage.ImageUrl = @"image/bronze.png";
                break;
            case 2:
            case 3:
                BoxImage.ImageUrl = @"image/sliver.png";
                break;
            case 4:
                BoxImage.ImageUrl = @"image/gold.png";
                break;
            default:
                break;
        }

        if (text == null)
        {
            ItemName.Text = "每周积分不够啦，开启失败";
        }

        ItemName.Text = text;

        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "宝箱";
        }

        if (HttpUtils.GetUserIdInCookies(Request) == null)
        {
            Response.Redirect(@"~/Login.aspx");
        }

        String weekawardid = Request.QueryString["week_award"];
        String openid = Request.QueryString["openid"];
        if (openid != null)
        {
            InitByOpenId(openid);
            BackButton.NavigateUrl = "~/Award.aspx";
        }
        else if (weekawardid != null)
        {
            InitByWeekAwardId(weekawardid);
            BackButton.NavigateUrl = "~/Mission.aspx";
        }
    }
}