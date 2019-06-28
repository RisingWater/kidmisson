using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class ItemModel : BaseModel
{
	public ItemModel(XmlNode node) : base(node)
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
}