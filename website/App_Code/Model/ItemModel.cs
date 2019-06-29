using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class ItemModel : BaseModel
{
	public ItemModel(XmlNode node, Database db) : base(node, db)
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

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public String ItemImageFileName
    {
        get
        {
            return getAttributesValue(@"image");
        }
    }

    public Int32 ExchangeUnit
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"exchange_unit");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}