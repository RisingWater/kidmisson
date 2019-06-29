using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;

public class Database
{
    protected XmlDocument m_pDoc;
    protected String m_szDbFilePath;
    private object m_csLock;


	public Database()
	{
        m_csLock = new object();
        m_pDoc = new XmlDocument();
        m_szDbFilePath = null;
    }

    protected void Lock()
    {
        Monitor.Enter(m_csLock);
    }

    protected void Unlock()
    {
        Monitor.Exit(m_csLock);
    }

    public void SaveDbToFile()
    {
        Lock();
        if (m_szDbFilePath != null)
        {
            m_pDoc.Save(m_szDbFilePath);
        }
        Unlock();
    }
}