using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;

public class BaseModel
{
    protected XmlNode m_pNode;
    protected Database m_pDB;
    private object m_csLock;

    public BaseModel(XmlNode node, Database db)
	{
        m_csLock = new object();
        m_pNode = node;
        m_pDB = db;
	}

    protected String getAttributesValue(String key)
    {
        String value = null;
        Lock();

        XmlAttribute attr = m_pNode.Attributes[key];
        if (attr != null)
        {
            value = attr.Value;
        }

        Unlock();

        return value;
    }

    protected void setAttributesValue(String key, String value)
    {
        Lock();

        XmlAttribute attr = m_pNode.Attributes[key];
        if (attr != null)
        {
            attr.Value = value;
        }

        m_pDB.SaveDbToFile();

        Unlock();

        return;
    }

    protected void Lock()
    {
        Monitor.Enter(m_csLock);
    }

    protected void Unlock()
    {
        Monitor.Exit(m_csLock);
    }
}