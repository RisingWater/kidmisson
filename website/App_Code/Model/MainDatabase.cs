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
}