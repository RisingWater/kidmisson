using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;
using System.Text;
using System.Globalization;

/// <summary>
///XmlDatabase 的摘要说明
/// </summary>
public class XmlDatabase
{
    private static XmlDatabase instance = null;

    public static XmlDatabase GetInstance
    {
        get
        {
            if (XmlDatabase.instance == null)
            {
                XmlDatabase.instance = new XmlDatabase();
            }

            return XmlDatabase.instance;
        }
    }

    private XmlDocument doc;
    private object objectlock;
    private List<AchievementObject> AcheList;
    private List<DailyMissionState> DaliyList;
    private XmlNode TodatMissionState;
    private String DailyKey;
    private PersonInformation information;

	public XmlDatabase()
	{
        objectlock = new object();
        doc = new XmlDocument();
        doc.Load(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));
        information = new PersonInformation(doc);
        InitAchievementList();
        InitDailyList();
	}

    public PersonInformation Informaton
    {
        get { return information; }
    }
    
    public void Lock()
    {
        Monitor.Enter(objectlock);
    }

    public void Unlock()
    {
        Monitor.Exit(objectlock);
    }

    private void InitDailyList()
    {
        DaliyList = new List<DailyMissionState>();

        Lock();

        DailyKey = DateTime.Now.ToShortDateString();

        XmlNodeList list = doc.SelectNodes(@"root/daily_root/daily");
        TodatMissionState = null;

        foreach (XmlNode tmp in list)
        {
            XmlAttribute attr = tmp.Attributes["id"];

            if (String.Compare(DailyKey, attr.Value) == 0)
            {
                TodatMissionState = tmp;
                break;
            }
        }

        if (TodatMissionState == null)
        {
            XmlNode node = doc.SelectSingleNode(@"root/daily_root");
            XmlElement daily = doc.CreateElement("daily");
            daily.Attributes.Append(doc.CreateAttribute("id"));
            daily.Attributes["id"].Value = DailyKey;
            daily.Attributes.Append(doc.CreateAttribute("MissionState"));
            daily.Attributes["MissionState"].Value = "0";
            node.AppendChild(daily);

            TodatMissionState = daily;

            doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));
        }

        for (int i = 0; i < 12; i++)
        {
            DailyMissionState state = new DailyMissionState(i, TodatMissionState.Attributes["MissionState"]);
            DaliyList.Add(state);
        }

        Unlock();
    }

    private void InitAchievementList()
    {
        AcheList = new List<AchievementObject>();

        Lock();

        XmlNodeList list = doc.SelectNodes(@"root/achievement/achievement_group");

        foreach (XmlNode tmp in list)
        {
            XmlAttribute attr = tmp.Attributes["id"];
            Int32 GroupId = Convert.ToInt32(attr.Value);
            XmlNode node = tmp.SelectSingleNode("progress");
            XmlNode Current = node.FirstChild;

            Int32 Total = 0;
            String descrption = @"";
            Boolean Done = false;
            Int32 Point = 0;
            Int32 AcheId = -1;
            Boolean success = false;

            XmlNodeList achelist = tmp.SelectNodes("achievement");
            XmlNode selectnode = null;
            foreach (XmlNode ache in achelist)
            {
                selectnode = ache;
                if (Convert.ToInt32(ache.SelectSingleNode("done").FirstChild.Value) == 0)
                {
                    break;
                }
            }

            if (selectnode != null)
            {
                AcheId = Convert.ToInt32(selectnode.Attributes["id"].Value);
                Done = Convert.ToInt32(selectnode.SelectSingleNode("done").FirstChild.Value) != 0;
                Total = Convert.ToInt32(selectnode.SelectSingleNode("progress").FirstChild.Value);
                Point = Convert.ToInt32(selectnode.SelectSingleNode("point").FirstChild.Value);
                descrption = selectnode.SelectSingleNode("description").FirstChild.Value;

                success = true;
            }

            if (success)
            {
                AchievementObject ache_object = new AchievementObject(GroupId, AcheId, descrption, Total, Current, Done, Point, selectnode);
                AcheList.Add(ache_object);
            }
        }

        Unlock();
    }

    private AchievementObject FindAchievement(Int32 groupid)
    {
        foreach (AchievementObject tmp in AcheList)
        {
            if (tmp.GroupId == groupid)
            {
                return tmp;
            }
        }

        return null;
    }

    public List<AchievementObject> GetAllAchievement()
    {
        return AcheList;
    }

    public List<DailyMissionState> GetDailyMissionState()
    {
        if (String.Compare(DateTime.Now.ToShortDateString(), DailyKey) != 0)
        {
            InitDailyList();
        }
        return DaliyList;
    }

    public void SetAchievementDone(Int32 groupid, Int32 acheid)
    {
        Int32 point = 0;

        Lock();

        AchievementObject tmp = FindAchievement(groupid);

        if (tmp != null && tmp.CheckId(groupid, acheid))
        {
            XmlNode node = tmp.XmlNode;
            node.SelectSingleNode("done").FirstChild.Value = "1";
            point = tmp.Point;
        }

        if (point != 0)
        {
            information.UpdatePoint(point);

            tmp = FindAchievement(Static.GetPointAchievementId);
            tmp.UpdateCurrent(point);
        }

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        InitAchievementList();

        Unlock();
    }

    public void SetDailyMissionDone(Int32 MissoinId)
    {
        Lock();

        Int32 MissionState = Convert.ToInt32(TodatMissionState.Attributes["MissionState"].Value);

        if ((MissionState & (1 << MissoinId)) != 0)
        {
            Unlock();
            return;
        }

        MissionState |= (1 << MissoinId);
        TodatMissionState.Attributes["MissionState"].Value = MissionState.ToString();

        //积分加10
        information.UpdatePoint(10);

        //积分成就
        AchievementObject tmp = FindAchievement(Static.GetPointAchievementId);
        tmp.UpdateCurrent(10);

        switch (MissoinId)
        {
            case 0:
            case 1:
                 tmp = FindAchievement(Static.DoMathAchievementId);
                 tmp.UpdateCurrent(20);
                 break;
            case 2:
            case 3:
                 tmp = FindAchievement(Static.ReadBookAchievementId);
                 tmp.UpdateCurrent(1);
                 break;
            case 4:
                 tmp = FindAchievement(Static.ReadEnglishAchievementId);
                 tmp.UpdateCurrent(1);
                 break;
            case 5:
            case 6:
                 tmp = FindAchievement(Static.WriteCharAchievement);
                 tmp.UpdateCurrent(1);
                 break;
            case 8:
            case 9:
                 tmp = FindAchievement(Static.EatHimselfAchievemntId);
                 tmp.UpdateCurrent(1);
                 break;
            case 10:
                 tmp = FindAchievement(Static.SleepHimselfAchievement);
                 tmp.UpdateCurrent(1);
                 break;
            case 11:
                 tmp = FindAchievement(Static.WriteDailyAchievement);
                 tmp.UpdateCurrent(1);
                 break;
            default:
                 break;
        }

        DateTime time = DateTime.Now;
        GregorianCalendar calendar = new GregorianCalendar();
        int weekOfYears = calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        String weekKey = time.Year + "-" + weekOfYears;

        XmlDatabase database = XmlDatabase.GetInstance;
        WeekInfo week = database.GetWeekInfo(weekKey);

        week.UpdateCurrent(10);

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        InitAchievementList();

        Unlock();
    }

    public String OpenBronzeBox()
    {
        String text = "";
        Lock();

        if (information.Point < Static.BRONZE_PRICE)
        {
            Unlock();
            return null;
        }

        information.UpdatePoint(-1 * Static.BRONZE_PRICE);

        Random rd = new Random();
        Int32 roll = rd.Next() % 10;

        if (roll < 7)
        {
            information.UpdateAnimatePiece(1);
            text = "动画碎片 x 1";
        }
        else if (roll < 8)
        {
            information.UpdateToyPiece(1);
            text = "玩具碎片 x 1";
        }
        else if (roll < 9)
        {
            information.UpdateFoodPiece(1);
            text = "美食碎片 x 1";
        }
        else
        {
            information.UpdatePlayPiece(1);
            text = "陪玩碎片 x 1";
        }

        AchievementObject tmp = FindAchievement(Static.OpenBoxAchievementId);
        tmp.UpdateCurrent(1);

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        Unlock();

        return text;
    }

    public String OpenSliverBox()
    {
        String text = "";
        Lock();

        if (information.Point < Static.SLIVER_PRICE)
        {
            Unlock();
            return null;
        }

        information.UpdatePoint(-1 * Static.SLIVER_PRICE);

        Random rd = new Random();
        Int32 roll = rd.Next() % 10;

        if (roll < 4)
        {
            information.UpdateAnimatePiece(2);
            text = "动画碎片 x 2";
        }
        else if (roll < 6)
        {
            information.UpdateAnimatePiece(3);
            text = "动画碎片 x 3";
        }
        else if (roll < 8)
        {
            information.UpdateFoodPiece(2);
            text = "美食碎片 x 2";
        }
        else if (roll < 9)
        {
            information.UpdateToyPiece(1);
            text = "玩具碎片 x 1";
        }
        else
        {
            information.UpdatePlayPiece(1);
            text = "陪玩碎片 x 1";
        }

        AchievementObject tmp = FindAchievement(Static.OpenBoxAchievementId);
        tmp.UpdateCurrent(1);

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        Unlock();

        return text;
    }

    public String OpenGoldBox()
    {
        String text = "";
        Lock();

        if (information.Point < Static.GOLD_PRICE)
        {
            Unlock();
            return null;
        }

        information.UpdatePoint(-1 * Static.GOLD_PRICE);

        Random rd = new Random();
        Int32 roll = rd.Next() % 10;

        if (roll < 3)
        {
            information.UpdateAnimatePiece(4);
            text = "动画碎片 x 4";
        }
        else if (roll < 4)
        {
            information.UpdateAnimatePiece(5);
            text = "动画碎片 x 5";
        }
        else if (roll < 6)
        {
            information.UpdateFoodPiece(4);
            text = "美食碎片 x 4";
        }
        else if (roll < 9)
        {
            information.UpdateToyPiece(3);
            text = "玩具碎片 x 3";
        }
        else
        {
            information.UpdatePlayPiece(1);
            text = "陪玩碎片 x 1";
        }

        AchievementObject tmp = FindAchievement(Static.OpenBoxAchievementId);
        tmp.UpdateCurrent(1);

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        Unlock();

        return text;
    }

    public String OpenDiamondBox()
    {
        String text = "";
        Lock();

        if (information.Point < Static.DIAMOND_PRICE)
        {
            Unlock();
            return null;
        }

        information.UpdatePoint(-1 * Static.DIAMOND_PRICE);

        Random rd = new Random();
        Int32 roll = rd.Next() % 10;

        if (roll < 1)
        {
            text = "100元玩具";
        }
        else if (roll < 3)
        {
            text = "50元玩具";
        }
        else if (roll < 4)
        {
            text = "火锅美食";
        }
        else if (roll < 6)
        {
            text = "动画片5集";
        }
        else if (roll < 8)
        {
            text = "课外书";
        }
        else
        {
            text = "电影1场";
        }

        AchievementObject tmp = FindAchievement(Static.OpenBoxAchievementId);
        tmp.UpdateCurrent(1);

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        Unlock();

        return text;
    }

    public String OpenWeekyAward(Int32 id)
    {
        String text = "";
        Lock();

        DateTime time = DateTime.Now;
        GregorianCalendar calendar = new GregorianCalendar();
        int weekOfYears = calendar.GetWeekOfYear(time, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        String weekKey = time.Year + "-" + weekOfYears;

        WeekInfo week = GetWeekInfo(weekKey);

        if (week.CurrentPoint < (id + 1 * 100))
        {
            Unlock();
            return null;
        }

        if (id == 0)
        {
            information.UpdateToyPiece(2);
            text = "玩具碎片 x 2";
        }
        else if (id == 1)
        {
            information.UpdateAnimatePiece(2);
            text = "动画碎片 x 2";
        }
        else if (id == 2)
        {
            information.UpdateFoodPiece(2);
            text = "美食碎片 x 2";
        }
        else if (id == 3)
        {
            Random rd = new Random();
            Int32 roll = rd.Next() % 10;

            if (roll < 4)
            {
                information.UpdateAnimatePiece(2);
                text = "动画碎片 x 2";
            }
            else if (roll < 7)
            {
                information.UpdateAnimatePiece(3);
                text = "动画碎片 x 3";
            }
            else if (roll < 9)
            {
                information.UpdateFoodPiece(2);
                text = "美食碎片 x 2";
            }
            else
            {
                information.UpdateToyPiece(1);
                text = "玩具碎片 x 3";
            }
        }
        else if (id == 4)
        {
            Random rd = new Random();
            Int32 roll = rd.Next() % 10;

            if (roll < 3)
            {
                information.UpdateAnimatePiece(4);
                text = "动画碎片 x 4";
            }
            else if (roll < 4)
            {
                information.UpdateAnimatePiece(5);
                text = "动画碎片 x 5";
            }
            else if (roll < 6)
            {
                information.UpdateFoodPiece(4);
                text = "美食碎片 x 4";
            }
            else
            {
                information.UpdateToyPiece(3);
                text = "玩具碎片 x 3";
            }
        }

        if (id < 5)
        {
            week.SetAwardDone(id);
        }

        doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

        Unlock();

        return text;
    }

    public WeekInfo GetWeekInfo(String keyword)
    {
        WeekInfo ret = null;
        Lock();

        XmlNodeList list = doc.SelectNodes(@"root/week_root/week");
        foreach (XmlNode tmp in list)
        {
            XmlAttribute attr = tmp.Attributes["id"];
            if (String.Compare(keyword, attr.Value) == 0)
            {
                ret = new WeekInfo(tmp);
                break;
            }
        }

        if (ret == null)
        {
            XmlNode node = doc.SelectSingleNode(@"root/week_root");
            XmlElement week = doc.CreateElement("week");
            week.Attributes.Append(doc.CreateAttribute("id"));
            week.Attributes["id"].Value = keyword;
            week.Attributes.Append(doc.CreateAttribute("cur_point"));
            week.Attributes["cur_point"].Value = "0";
            week.Attributes.Append(doc.CreateAttribute("awarddone0"));
            week.Attributes["awarddone0"].Value = "0";
            week.Attributes.Append(doc.CreateAttribute("awarddone1"));
            week.Attributes["awarddone1"].Value = "0";
            week.Attributes.Append(doc.CreateAttribute("awarddone2"));
            week.Attributes["awarddone2"].Value = "0";
            week.Attributes.Append(doc.CreateAttribute("awarddone3"));
            week.Attributes["awarddone3"].Value = "0";
            week.Attributes.Append(doc.CreateAttribute("awarddone4"));
            week.Attributes["awarddone4"].Value = "0";
            node.AppendChild(week);

            doc.Save(System.Web.HttpContext.Current.Server.MapPath(Static.XML_WEB_PATH));

            ret = new WeekInfo(week);
        }

        Unlock();

        return ret;
    }
}