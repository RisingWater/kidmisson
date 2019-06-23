using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class PersonInformation
{
    private String name;
    private String age;
    private String school;
    private String grade;
    private String headimage;
    private XmlNode point;
    private XmlNode animate_piece;
    private XmlNode toy_piece;
    private XmlNode food_piece;
    private XmlNode play_piece;

	public PersonInformation(XmlDocument doc)
	{
        XmlNode tmpnode;
        try
        {
            //read header
            tmpnode = doc.SelectSingleNode(@"root/information/image");
            headimage = @"image/" + tmpnode.FirstChild.Value;
            //read name
            tmpnode = doc.SelectSingleNode(@"root/information/name");
            name = tmpnode.FirstChild.Value;
            //read age
            tmpnode = doc.SelectSingleNode(@"root/information/age");
            age = tmpnode.FirstChild.Value + "岁";
            //read school
            tmpnode = doc.SelectSingleNode(@"root/information/school");
            school = tmpnode.FirstChild.Value;
            //read grade
            tmpnode = doc.SelectSingleNode(@"root/information/grade");
            grade = tmpnode.FirstChild.Value;

            tmpnode = doc.SelectSingleNode(@"root/wallet/point");
            point = tmpnode.FirstChild;

            tmpnode = doc.SelectSingleNode(@"root/wallet/animate_piece");
            animate_piece = tmpnode.FirstChild;

            tmpnode = doc.SelectSingleNode(@"root/wallet/toy_piece");
            toy_piece = tmpnode.FirstChild;

            tmpnode = doc.SelectSingleNode(@"root/wallet/food_piece");
            food_piece = tmpnode.FirstChild;

            tmpnode = doc.SelectSingleNode(@"root/wallet/play_piece");
            play_piece = tmpnode.FirstChild;
        }
        catch (Exception exception)
        {
            name = exception.ToString();
            age = @"0";
            school = @"unknow school";
            grade = @"unknow grade";
            point = null;
            animate_piece = null;
            toy_piece = null;
            food_piece = null;
            play_piece = null;
        }
	}

    public String Name
    {
        get { return name; }
    }

    public String Age
    {
        get { return age; }
    }

    public String School
    {
        get { return school; }
    }

    public String Grade
    {
        get { return grade; }
    }

    public String Headimage
    {
        get { return headimage; }
    }

    public Int32 Point
    {
        get
        {
            try
            {
                return Convert.ToInt32(point.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdatePoint(Int32 change)
    {
        Int32 cur = Convert.ToInt32(point.Value);
        cur += change;
        point.Value = cur.ToString();
    }

    public Int32 Animate_piece
    {
        get
        {
            try
            {
                return Convert.ToInt32(animate_piece.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdateAnimatePiece(Int32 change)
    {
        Int32 cur = Convert.ToInt32(animate_piece.Value);
        cur += change;
        animate_piece.Value = cur.ToString();
    }

    public Int32 Toy_piece
    {
        get
        {
            try
            {
                return Convert.ToInt32(toy_piece.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdateToyPiece(Int32 change)
    {
        Int32 cur = Convert.ToInt32(toy_piece.Value);
        cur += change;
        toy_piece.Value = cur.ToString();
    }

    public Int32 Food_piece
    {
        get
        {
            try
            {
                return Convert.ToInt32(food_piece.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdateFoodPiece(Int32 change)
    {
        Int32 cur = Convert.ToInt32(food_piece.Value);
        cur += change;
        food_piece.Value = cur.ToString();
    }

    public Int32 Play_piece
    {
        get
        {
            try
            {
                return Convert.ToInt32(play_piece.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdatePlayPiece(Int32 change)
    {
        Int32 cur = Convert.ToInt32(play_piece.Value);
        cur += change;
        play_piece.Value = cur.ToString();
    }


}