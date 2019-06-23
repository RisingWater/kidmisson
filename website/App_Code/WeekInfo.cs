using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///WeakInfo 的摘要说明
/// </summary>
public class WeekInfo
{
    private XmlAttribute cur_point;
    private XmlAttribute[] awarddone;

	public WeekInfo(XmlNode root)
	{
        cur_point = root.Attributes["cur_point"];
        
		awarddone = new XmlAttribute[5];
        for (Int32 i = 0; i < 5; i++)
        {
            String tmp = "awarddone" + i;
            awarddone[i] = root.Attributes[tmp];
        }
	}

    public Int32 CurrentPoint
    {
        get
        {
            try
            {
                Int32 p = Convert.ToInt32(cur_point.Value);
                if (p > 500)
                {
                    p = 500;
                }

                return p;
            }
            catch
            {
                return 0;
            }
        }
    }

    private Boolean AwardDone(Int32 index)
    {
        try
        {
            return (Convert.ToInt32(awarddone[index].Value) != 0);
        }
        catch
        {
            return false;
        }
    }

    public void SetAwardDone(Int32 index)
    {
        try
        {
            awarddone[index].Value = "1";
            return;
        }
        catch
        {
            return;
        }
    }

    public Int32 CanbeAward()
    {
        for (Int32 i = 0; i < 5; i++)
        {
            if (CurrentPoint >= 100 * (i + 1) && !AwardDone(i))
            {
                return i;
            }
        }

        return -1;
    }

    public String WeekMissionText()
    {
        if (CanbeAward() >= 0)
        {
            return "请领取奖励";
        }
        else
        {
            if (CurrentPoint >= 500)
            {
                return "累计积分目标已经达成，太棒啦";
            }
            else
            {
                Int32 rest = 100 - CurrentPoint % 100;
                return "再获得" + rest + "积分，就可获得奖励";
            }
        }
    }

    public void UpdateCurrent(Int32 change)
    {
        Int32 newPoint = CurrentPoint + change;
        cur_point.Value = newPoint.ToString();
    }
}