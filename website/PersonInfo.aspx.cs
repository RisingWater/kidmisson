using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Drawing;

public partial class PersonInfo : System.Web.UI.Page
{
    private String m_szUserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "我的信息";
        }

        m_szUserId = HttpUtils.GetUserIdInCookies(Request);
        if (m_szUserId == null)
        {
            Response.Redirect(@"~/Index.aspx");
        }

        PersonInfoController personController = new PersonInfoController(m_szUserId);

        HeadImage.ImageUrl = "image/" + personController.HeadImageFileName;
        NameLabel.Text = personController.Name;
        AgeLabel.Text = personController.Age.ToString() + "岁";
        SchoolLabel.Text = personController.School;
        GradeLabel.Text = personController.Grade;

        PocketController pocketController = new PocketController(m_szUserId);
        PointLabel.Text = pocketController.Point.ToString();
    }
    protected void PocketItemReaper_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "exchange")
        {
            String Text = null;
            Int32 item_id = Convert.ToInt32(e.CommandArgument);
            PocketItemController itemController = PocketController.GetItem(m_szUserId, item_id);
            if (itemController.Exchange() == false)
            {
                Text = "兑换" + itemController.Description + "失败";
            }
            else
            {
                Text = "成功兑换" + itemController.Description + " x " + itemController.ExchangeUnit.ToString() + "个";
                RecordController.AddDetail(m_szUserId, ModelParam.EXCHANGE_RECORD_ID, Text, 0);
            }

            PocketItemReaper.DataBind();

            ClientScript.RegisterStartupScript(ClientScript.GetType(), 
                "ExchangeDoneScript", 
                "<script>swal('" + Text + "').then((value) => {window.location.href='PersonInfo.aspx';});</script>");
        }
    }

    public string GetJavaScript(int item_index)
    {
        String js = @"JAVAScript:document.getElementById('MainContent_PocketItemReaper_exchange_" + item_index + "').click();";
        return js;
    }

   
}