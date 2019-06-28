using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Threading;

public class MainDatabaseManager
{
    private static MainDatabaseManager m_pInstance = null;

    public static MainDatabase GetDatabase(String userid)
    {
        MainDatabaseManager manager = MainDatabaseManager.GetInstance();
        return manager.GetDatabaseByUserId(userid);
    }

    private static MainDatabaseManager GetInstance()
    {
        if (MainDatabaseManager.m_pInstance == null)
        {
            MainDatabaseManager.m_pInstance = new MainDatabaseManager();
        }

        return MainDatabaseManager.m_pInstance;
    }

    private object m_csLock;
    private Dictionary<String, MainDatabase> m_pDatabaseMap;

    private MainDatabaseManager()
	{
        m_csLock = new object();
        m_pDatabaseMap = new Dictionary<String, MainDatabase>();
	}

    public void Lock()
    {
        Monitor.Enter(m_csLock);
    }

    public void Unlock()
    {
        Monitor.Exit(m_csLock);
    }

    private MainDatabase GetDatabaseByUserId(String userid)
    {
        MainDatabase db = null;

        Lock();
        if (m_pDatabaseMap.ContainsKey(userid))
        {
            db = m_pDatabaseMap[userid];
        }
        else
        {
            db = new MainDatabase(userid);
            m_pDatabaseMap.Add(userid, db);
        }
        Unlock();

        return db;
    }
}