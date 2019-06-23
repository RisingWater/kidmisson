using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.UI.HtmlControls;

public partial class Items : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label title = (Label)Master.FindControl("TitleLabel");
        if (title != null)
        {
            title.Text = "我的信息";
        }

        XmlDatabase database = XmlDatabase.GetInstance;

        PersonInformation info = database.Informaton;

        //read header
        headImage.ImageUrl = info.Headimage;
        headImage.Width = 80;
        headImage.Height = 80;
        //read name
        name.Text = info.Name;
        //read age
        age.Text = info.Age;
        //read school
        school.Text = info.School;
        //read grade
        grade.Text = info.Grade;
        point.Text = info.Point.ToString();
        animate_piece.Text = info.Animate_piece.ToString();
        toy_piece.Text = info.Toy_piece.ToString();
        food_piece.Text = info.Food_piece.ToString();
        play_piece.Text = info.Play_piece.ToString();
    }
}