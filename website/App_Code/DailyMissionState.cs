using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///DailyMissionState 的摘要说明
/// </summary>
public class DailyMissionState
{
    private Int32 missionIndex;
    private XmlAttribute done;

    private String[] missionTextList;

	public DailyMissionState(Int32 MissionIndex, XmlAttribute Done)
	{
        missionTextList = new String[12];
        missionIndex = MissionIndex;
        done = Done;

        missionTextList[0] = "数学大挑战1 (完成口算20题) (10积分)";
        missionTextList[1] = "数学大挑战2 (完成口算20题) (10积分)";
        missionTextList[2] = "书本真好看1 (独立大声阅读15分钟) (10积分)";
        missionTextList[3] = "书本真好看2 (独立大声阅读15分钟) (10积分)";
        missionTextList[4] = "英语很简单 (英语打卡一次) (10积分)";
        missionTextList[5] = "我的字写的真好1 (练字一页) (10积分)";
        missionTextList[6] = "我的字写的真好2 (练字一页) (10积分)";
        missionTextList[7] = "幻影魔腿 (跳绳100下) (10积分)";
        missionTextList[8] = "消灭食物1 (独立吃一顿吃饭，不挑食) (10积分)";
        missionTextList[9] = "消灭食物2 (独立吃一顿吃饭，不挑食) (10积分)";
        missionTextList[10] = "美梦成真 (独立睡觉) (10积分)";
        missionTextList[11] = "记录我的一天 (写一篇日记) (10积分)";
	}

    public String MissionText
    {
        get
        {
            if (missionIndex < 12)
            {
                return missionTextList[missionIndex];
            }

            return "";
        }
    }

    public String ButtonStyle
    {
        get
        {
            Boolean hide = true;

            Int32 State = Convert.ToInt32(done.Value);

            hide = (State & (1 << missionIndex)) != 0;

            if (hide)
            {
                return "background-color:#2dd700;color:#ffffff;font-weight:bold";
            }
            else
            {
                return "background-color:#3887c5;color:#ffffff";
            }
        }
    }

    public String ButtonUrl
    {
        get
        {
            Boolean hide = true;

            Int32 State = Convert.ToInt32(done.Value);

            hide = (State & (1 << missionIndex)) != 0;

            if (hide)
            {
                return "./Mission.aspx";
            }
            else
            {
                return "./Mission.aspx?time=" + DateTime.Now.ToShortDateString() + "&done=1&missionindex=" + missionIndex;
            }
        }
    }

    public String ButtonText
    {
        get
        {
            Boolean hide = true;

            Int32 State = Convert.ToInt32(done.Value);

            hide = (State & (1 << missionIndex)) != 0;

            if (hide)
            {
                return "已经完成";
            }
            else
            {
                return "完成任务";
            }
        }
    }
}