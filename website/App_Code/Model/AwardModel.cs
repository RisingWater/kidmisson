using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class AwardItemModel : BaseModel
{
    public AwardItemModel(XmlNode node, Database db)
        : base(node, db)
    {

    }

    public Int32 ItemId
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

    public String ItemName
    {
        get
        {
            return getAttributesValue("item_name");
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

    public Int32 Weight
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"weight");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}

public class AwardBoxModel : BaseModel
{
    private List<AwardItemModel> m_pItemList;

    public AwardBoxModel(XmlNode node, Database db)
        : base(node, db)
    {
        m_pItemList = new List<AwardItemModel>();

        XmlNodeList nodelist = node.SelectNodes(@"award_item");
        foreach (XmlNode tmp in nodelist)
        {
            AwardItemModel model = new AwardItemModel(tmp, db);
            m_pItemList.Add(model);
        }
    }

    public Int32 Id
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"award_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Int32 Cost
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"cost");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public String AwardImageFileName
    {
        get
        {
            return getAttributesValue(@"image");
        }
    }

    public List<AwardItemModel> ItemList
    {
        get
        {
            return m_pItemList;
        }
    }
}

public class AwardModel : BaseModel
{
    private List<AwardBoxModel> m_pAwardList;

	public AwardModel(XmlNode node, Database db) : base(node, db)
	{
        m_pAwardList = new List<AwardBoxModel>();
        XmlNodeList nodelist = node.SelectNodes(@"award_box");
        foreach (XmlNode tmp in nodelist)
        {
            AwardBoxModel model = new AwardBoxModel(tmp, db);
            m_pAwardList.Add(model);
        }
	}

    public List<AwardBoxModel> AwardList
    {
        get
        {
            return m_pAwardList;
        }
    }
}