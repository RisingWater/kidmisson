using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;

public class MainDatabase
{
    private object m_csLock;
    private XmlDocument m_pDoc;
    private PersonInfoModel m_pUser;
    private Dictionary<Int32, ItemModel> m_pItemList;
    private PocketModule m_pPocket;

	public MainDatabase(String userid)
	{
        String filename = userid + ".xml";
        m_csLock = new object();
        m_pDoc = new XmlDocument();
        m_pDoc.Load(System.Web.HttpContext.Current.Server.MapPath(filename));

        InitPersonInfo();
        InitItemList();
	}

    protected void Lock()
    {
        Monitor.Enter(m_csLock);
    }

    protected void Unlock()
    {
        Monitor.Exit(m_csLock);
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

    private void InitPersonInfo()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/personal_info");
        m_pUser = new PersonInfoModel(node);
    }

    private void InitItemList()
    {
        m_pItemList = new Dictionary<Int32, ItemModel>();

        XmlNodeList list = m_pDoc.SelectNodes(@"root/items/item");

        foreach (XmlNode tmp in list)
        {
            ItemModel item = new ItemModel(tmp);
            m_pItemList.Add(item.Id, item);
        }
    }

    private void InitPocket()
    {
        XmlNode node = m_pDoc.SelectSingleNode(@"root/pocket");
        m_pPocket = new PocketModule(node);
    }
}