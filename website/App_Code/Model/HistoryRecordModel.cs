using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class HistoryRecordModel : BaseModel
{
    public HistoryRecordModel(XmlNode node, Database db)
        : base(node, db)
    {

    }

    public String Time
    {
        get
        {
            return getAttributesValue(@"time");
        }
    }

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
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
                Id = 0;
            }

            return Id;
        }
    }
}