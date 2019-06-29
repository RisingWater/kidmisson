using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class PocketPointModel : BaseModel
{
	public PocketPointModel(XmlNode node, Database db) : base(node, db)
	{
		
	}

    public Int32 Point
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"point");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Boolean UpdatePoint(Int32 delta)
    {
        Boolean ret = false;
        Lock();

        Int32 NewPoint = Point + delta;

        if (NewPoint >= 0)
        {
            setAttributesValue(@"point", NewPoint.ToString());
            ret = true;
        }

        Unlock();

        return ret;
    }
}

public class PocketItemModule : BaseModel
{
    public PocketItemModule(XmlNode node, Database db) : base(node, db)
	{
		
	}

    public Int32 Id
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"item_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Int32 Number
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"number");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Boolean UpdateNumber(Int32 delta)
    {
        Boolean ret = true;
        Lock();

        Int32 NewPoint = Number + delta;

        if (NewPoint >= 0)
        {
            setAttributesValue(@"number", NewPoint.ToString());
            ret = true;
        }
        else
        {
            ret = false;
        }

        Unlock();

        return ret;
    }
}

public class PocketModule
{
    private PocketPointModel m_pWalletPointModel;
    private List<PocketItemModule> m_pWalletItemModule;

    public PocketModule(XmlNode node, Database db)
	{
        XmlNode wallet = null;

        wallet = node.SelectSingleNode("wallet");
        m_pWalletPointModel = new PocketPointModel(wallet, db);

        XmlNodeList list = node.SelectNodes("pocket_item");
        m_pWalletItemModule = new List<PocketItemModule>();
        foreach (XmlNode tmp in list)
        {
            PocketItemModule item = new PocketItemModule(tmp, db);
            m_pWalletItemModule.Add(item);
        }
	}

    public PocketPointModel Point
    {
        get
        {
            return m_pWalletPointModel;
        }
    }

    public List<PocketItemModule> ItemList
    {
        get
        {
            return m_pWalletItemModule;
        } 
    }
}