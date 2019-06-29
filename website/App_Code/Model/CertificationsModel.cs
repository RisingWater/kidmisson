using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class CertificationModel : BaseModel
{
    public CertificationModel(XmlNode node, Database db) : base(node, db)
    {   

    }

    public String Username
    {
        get
        {
            return getAttributesValue(@"username");
        }
    }

    public String EncodedPassword
    {
        get
        {
            return getAttributesValue(@"password");
        }
    }

    public String UserId
    {
        get
        {
            return getAttributesValue(@"userid");
        }
    }
}
