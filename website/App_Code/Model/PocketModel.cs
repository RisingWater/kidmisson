using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class PocketPointModel : BaseModel
{
	public PocketPointModel(XmlNode node) : base(node)
	{
		
	}

    public Int32 Point
    {
        get
        {
            Int32 Id;
            try
            {
                Id = Convert.ToInt32(getAttributesValue(@"point"));
            }
            catch
            {
                Id = -1;
            }

            return Id;
        }
    }

    public void UpdatePoint(Int32 delta)
    {
        Lock();

        Int32 NewPoint = Point + delta;

        setAttributesValue(@"point", NewPoint.ToString());

        Unlock();
    }
}

public class PocketItemModule : BaseModel
{
    public PocketItemModule(XmlNode node)
        : base(node)
	{
		
	}

    public Int32 Id
    {
        get
        {
            Int32 Id;
            try
            {
                Id = Convert.ToInt32(getAttributesValue(@"item_id"));
            }
            catch
            {
                Id = -1;
            }

            return Id;
        }
    }

    public Int32 Number
    {
        get
        {
            Int32 Id;
            try
            {
                Id = Convert.ToInt32(getAttributesValue(@"number"));
            }
            catch
            {
                Id = -1;
            }

            return Id;
        }
    }

    public void UpdateNumber(Int32 delta)
    {
        Lock();

        Int32 NewPoint = Number + delta;

        setAttributesValue(@"point", NewPoint.ToString());

        Unlock();
    }
}

public class PocketModule
{
    private PocketPointModel m_pWalletPointModel;
    private List<PocketItemModule> m_pWalletItemModule;

    public PocketModule(XmlNode node)
	{
        XmlNode wallet = null;

        wallet = node.SelectSingleNode("wallet");
        m_pWalletPointModel = new PocketPointModel(wallet);

        XmlNodeList list = node.SelectNodes("pocket_item");

        foreach (XmlNode tmp in list)
        {
            PocketItemModule item = new PocketItemModule(tmp);
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