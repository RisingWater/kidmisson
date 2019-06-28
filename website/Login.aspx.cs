using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "登录";
        }

        Label RestPoint = (Label)Master.FindControl("RestPoint");
        if (RestPoint != null)
        {
            RestPoint.Visible = false;
        }

        ErrorMessageLabel.Visible = false;
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        String username = UserNameTextBox.Text;

        String password = PasswordTextBox.Text;
        byte[] byteArray = System.Text.Encoding.Default.GetBytes(password);
        password = Convert.ToBase64String(byteArray);

        CertificationController Controller = new CertificationController();
        String id = Controller.GetUserId(username, password);
        if (id == null)
        {
            ErrorMessageLabel.Text = @"用户名或密码错误";
            ErrorMessageLabel.Visible = true;
        }
        else
        {
            if (Context.Request.Browser.Cookies == false)
            {
                ErrorMessageLabel.Text = "登录失败";
                return;
            }

            ErrorMessageLabel.Visible = false;

            HttpCookie cookie = Request.Cookies["userid"];
            if (cookie == null)
            {
                cookie = new HttpCookie("userid");
            }

            cookie.Value = id;
            cookie.Expires = DateTime.Now.AddDays(7);
            Response.SetCookie(cookie);
            Response.Redirect(@"~/Mission.aspx");
        }
    }
}