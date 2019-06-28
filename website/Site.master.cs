using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDatabase database = XmlDatabase.GetInstance;

        PersonInformation info = database.Informaton;

        RestPoint.Text = "剩余积分: " + info.Point.ToString() + "分";
    }
}
