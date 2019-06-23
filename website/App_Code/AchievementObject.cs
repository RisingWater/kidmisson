using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class AchievementObject
{
    private String description;
    private Int32 total;
    private XmlNode current;
    private Boolean done;
    private Int32 point;
    private Int32 groupid;
    private Int32 acheid;
    private XmlNode node;

    public AchievementObject(Int32 GroupId, Int32 AcheId, String description, Int32 Total, XmlNode Current, Boolean Done, Int32 Point, XmlNode node)
	{
        this.groupid = GroupId;
        this.acheid = AcheId;
        this.description = description;
        this.total = Total;
        this.current = Current;
        this.done = Done;
        this.point = Point;
        this.node = node;
	}

    public String Description
    {
        get { return description; }
    }

    public Int32 GroupId
    {
        get
        {
            return groupid;
        }
    }

    public Int32 Current
    {
        get
        {
            try
            {
                return Convert.ToInt32(current.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    public void UpdateCurrent(Int32 change)
    {
        Int32 cur = Convert.ToInt32(current.Value);
        cur += change;
        current.Value = cur.ToString();
    }

    public String Progress
    {
        get
        {
            if (Current < total)
            {
                return Current + "/" + total;
            }
            else
            {
                return total + "/" + total;
            }
        }
    }

    public String ProgressBarStyle
    {
        get
        {
            Int32 percent = Current * 100 / total;
            if (percent > 100)
            {
                percent = 100;
            }

            return "width:" + percent + "%";
        }
    }

    public String ButtonStyle
    {
        get
        {
            Boolean hide = true;

            if (!done && Current >= total)
            {
                hide = false;
            }

            return "display:" + (hide ? "none":"block");
        }
    }

    public Int32 Point
    {
        get { return point; }
    }

    public String ButtonUrl
    {
        get
        {
            return "./Achievement.aspx?done=1&gid=" + groupid + "&aid=" + acheid;
        }
    }

    public Boolean CheckId(Int32 groupId, Int32 acheId)
    {
        if (!done)
        {
            return (this.groupid == groupId && this.acheid == acheId);
        }
        else
        {
            return false;
        }
    }

    public XmlNode XmlNode
    {
        get { return node; }
    }
}