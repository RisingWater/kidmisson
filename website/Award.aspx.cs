using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Award : System.Web.UI.Page
{
    private String m_szUserId = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "宝箱";
        }

        m_szUserId = HttpUtils.GetUserIdInCookies(Request);
        if (m_szUserId == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }
    }

    public string GetImage(string filename)
    {
        return "image/" + filename;
    }

    public string GetStyle(int index)
    {
        switch (index)
        {
            case 0:
                return "background-color:#a65c00;color:#ffffff";
            case 1:
                return "background-color:#cccccc;color:#000000";
            case 2:
                return "background-color:#ffde40;color:#000000";
            default:
            case 3:
                return "background-color:#79abcf;color:#ffffff"; 
        }
    }

    public string GetJavaScript(int item_index)
    {
        String js = @"JAVAScript:document.getElementById('MainContent_AwardRepeater_open_" + item_index + "').click();";
        return js;
    }

    protected void AwardRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "open")
        {
            String item_name = "";
            String Text = null;
            Int32 award_id = Convert.ToInt32(e.CommandArgument);
            AwardBoxController itemController = AwardController.GetAwardBox(m_szUserId, award_id);
            if (itemController.OpenBox(ref item_name) == false)
            {
                Text = "开启" + itemController.Description + "失败";

                ClientScript.RegisterStartupScript(ClientScript.GetType(),
                    "OpenFailScript",
                    "<script>swal('" + Text + "').then((value) => {window.location.href='Award.aspx';});</script>");
            }
            else
            {
                Text = "成功开启" + itemController.Description + ",获得" + item_name;
                RecordController.AddDetail(m_szUserId, ModelParam.AWARD_RECORD_ID, Text, 0);

                Response.Redirect("~/AwardGet.aspx?Image=~/image/" + itemController.AwardImageFileName
                    + "&Target=" + itemController.Description
                    + "&Award=" + item_name
                    + "&BackUrl=~/Award.aspx");
            }


        }
    }
}
