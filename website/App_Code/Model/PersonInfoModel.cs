using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class PersonInfoModel : BaseModel
{
    public PersonInfoModel(XmlNode node, Database db) : base(node, db)
    {

    }

    public String Name
    {
        get
        {
            return getAttributesValue(@"name");
        }
    }

    public String Birthday
    {
        get
        {
            return getAttributesValue(@"birthday");
        }
    }

    public String School
    {
        get
        {
            return getAttributesValue(@"school");
        }
    }

    public String Grade
    {
        get
        {
            return getAttributesValue(@"grade");
        }
    }

    public String HeadImageFileName
    {
        get
        {
            return getAttributesValue(@"image");
        }
    }
}