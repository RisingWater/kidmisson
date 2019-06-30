using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;

public class MainDatabase : Database
{
    private PersonInfoModel m_pUser;
    private Dictionary<Int32, ItemModel> m_pItemList;
    private Dictionary<Int32, WeekMissionModel> m_pWeekMissionList;
    private Dictionary<Int32, DailyMissionModel> m_pDailyMissionList;
    private Dictionary<String, DailyModel> m_pDailyState;
    private Dictionary<String, WeeksModel> m_pWeekState;
    private PocketModule m_pPocket;
    private HistoryModel m_pHistory;
    private AwardModel m_pAward;
    private AchievementsModel m_pAchievements;

	public MainDatabase(String userid)
	{
        String filename = userid + ".xml";
        m_szDbFilePath = System.Web.HttpContext.Current.Server.MapPath(ModelParam.DATA_PATH + filename);
        m_pDoc.Load(m_szDbFilePath);

        InitPersonInfo();
        InitItemList();
        InitPocket();
        InitHistory();
        InitAward();
        InitAchievements();
        InitWeekMissionList();
        InitDailyMissionList();
        InitDailyState();
        InitWeekyState();
	}

    public PersonInfoModel GetUserInfo()
    {
        return m_pUser;
    }

    public ItemModel GetItemModel(Int32 Id)
    {
        ItemModel model = null;

        Lock();

        if (m_pItemList.ContainsKey(Id))
        {
            model = m_pItemList[Id];
        }

        Unlock();

        return model;
    }

    public WeekMissionModel GetWeekMissionModel(Int32 Id)
    {
        WeekMissionModel model = null;

        Lock();

        if (m_pWeekMissionList.ContainsKey(Id))
        {
            model = m_pWeekMissionList[Id];
        }

        Unlock();

        return model;
    }

    public DailyMissionModel GetDailyMissionModel(Int32 Id)
    {
        DailyMissionModel model = null;

        Lock();

        if (m_pDailyMissionList.ContainsKey(Id))
        {
            model = m_pDailyMissionList[Id];
        }

        Unlock();

        return model;
    }

    public DailyModel GetDailyState(String Id)
    {
        DailyModel model = null;

        Lock();

        if (m_pDailyState.ContainsKey(Id))
        {
            model = m_pDailyState[Id];
        }
        else
        {
            XmlElement e = m_pDoc.CreateElement(@"daily");
            XmlAttribute attr = m_pDoc.CreateAttribute(@"id");
            attr.Value = Id;
            e.Attributes.Append(attr);

            XmlNode dailys = m_pDoc.SelectSingleNode(@"root/dailys");
            dailys.AppendChild(e);

            model = new DailyModel(e, this);

            m_pDailyState.Add(Id, model);

            SaveDbToFile();
        }

        Unlock();

        return model;
    }

    public WeeksModel GetWeekyState(String Id)
    {
        WeeksModel model = null;

        Lock();

        if (m_pWeekState.ContainsKey(Id))
        {
            model = m_pWeekState[Id];
        }
        else
        {
            XmlElement e = m_pDoc.CreateElement(@"week");
            XmlAttribute attr = m_pDoc.CreateAttribute(@"id");
            attr.Value = Id;
            e.Attributes.Append(attr);

            attr = m_pDoc.CreateAttribute(@"number");
            attr.Value = "0";
            e.Attributes.Append(attr);


            XmlNode dailys = m_pDoc.SelectSingleNode(@"root/weeks");
            dailys.AppendChild(e);

            model = new WeeksModel(e, this);

            m_pWeekState.Add(Id, model);

            SaveDbToFile();
        }

        Unlock();

        return model;
    }

    public List<DailyMissionModel> GetDailyMissionList()
    {
        List<DailyMissionModel> list = new List<DailyMissionModel>();
        foreach (KeyValuePair<Int32, DailyMissionModel> tmp in m_pDailyMissionList)
        {
            list.Add(tmp.Value);
            list.Sort();
        }

        return list;
    }

    public List<WeekMissionModel> GetWeekyMissionList()
    {
        List<WeekMissionModel> list = new List<WeekMissionModel>();
        foreach (KeyValuePair<Int32, WeekMissionModel> tmp in m_pWeekMissionList)
        {
            list.Add(tmp.Value);
            list.Sort();
        }

        return list;
    }

    public PocketModule GetPocketModel()
    {
        return m_pPocket;
    }

    public HistoryModel GetHistoryModule()
    {
        return m_pHistory;
    }

    public AwardModel GetAwardModule()
    {
        return m_pAward;
    }

    public AchievementsModel GetAchievementsModel()
    {
        return m_pAchievements;
    }

    private void InitPersonInfo()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/personal_info");
        m_pUser = new PersonInfoModel(node, this);
    }

    private void InitItemList()
    {
        m_pItemList = new Dictionary<Int32, ItemModel>();

        XmlNodeList list = m_pDoc.SelectNodes(@"root/items/item");

        foreach (XmlNode tmp in list)
        {
            ItemModel item = new ItemModel(tmp, this);
            m_pItemList.Add(item.Id, item);
        }
    }

    private void InitWeekMissionList()
    {
        m_pWeekMissionList = new Dictionary<Int32, WeekMissionModel>();

        XmlNodeList node_list = m_pDoc.SelectNodes(@"root/week_missions/week_mission");
        foreach (XmlNode tmp in node_list)
        {
            WeekMissionModel mission = new WeekMissionModel(tmp, this);
            m_pWeekMissionList.Add(mission.Id, mission);
        }
    }

    private void InitDailyMissionList()
    {
        m_pDailyMissionList = new Dictionary<Int32, DailyMissionModel>();

        XmlNodeList node_list = m_pDoc.SelectNodes(@"root/missions/mission");
        foreach (XmlNode tmp in node_list)
        {
            DailyMissionModel mission = new DailyMissionModel(tmp, this);
            m_pDailyMissionList.Add(mission.Id, mission);
        }
    }

    private void InitPocket()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/pocket");
        m_pPocket = new PocketModule(node, this);
    }

    private void InitHistory()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/historys");
        m_pHistory = new HistoryModel(node, this);
    }

    private void InitAward()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/award");
        m_pAward = new AwardModel(node, this);
    }

    private void InitAchievements()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/achievement");
        m_pAchievements = new AchievementsModel(node, this);
    }

    private void InitDailyState()
    {
        m_pDailyState = new Dictionary<String, DailyModel>();

        XmlNodeList list = m_pDoc.SelectNodes(@"root/dailys/daily");

        foreach (XmlNode tmp in list)
        {
            DailyModel d = new DailyModel(tmp, this);
            m_pDailyState.Add(d.Id, d);
        }
    }

    private void InitWeekyState()
    {
        m_pWeekState = new Dictionary<String, WeeksModel>();

        XmlNodeList list = m_pDoc.SelectNodes(@"root/weeks/week");

        foreach (XmlNode tmp in list)
        {
            WeeksModel d = new WeeksModel(tmp, this);
            m_pWeekState.Add(d.Id, d);
        }
    }
}