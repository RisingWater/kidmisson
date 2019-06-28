using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class BaseModel
{
    protected XmlNode m_pNode;

    public BaseModel(XmlNode node)
	{
        m_pNode = node;
	}

    protected String getAttributesValue(String key)
    {
        String value = null;
        XmlAttribute attr = m_pNode.Attributes[key];
        if (attr != null)
        {
            value = attr.Value;
        }

        return value;
    }
}