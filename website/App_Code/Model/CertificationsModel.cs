using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class CertificationModel : BaseModel
{
    public CertificationModel(XmlNode node) : base(node)
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

public class CertificationDatabase
{
    private static CertificationDatabase m_pInstance = null;

    public static CertificationDatabase GetInstance()
    {
        if (CertificationDatabase.m_pInstance == null)
        {
            CertificationDatabase.m_pInstance = new CertificationDatabase();
        }

        return CertificationDatabase.m_pInstance;
    }

    private object m_csLock;
    private XmlDocument m_pDoc;
    private List<CertificationModel> CertificationList;

	private CertificationDatabase()
	{
        m_csLock = new object();
        m_pDoc = new XmlDocument();
        CertificationList = new List<CertificationModel>();
        m_pDoc.Load(System.Web.HttpContext.Current.Server.MapPath(ModelParam.CERIIFICATION_PATH));

        XmlNodeList list = m_pDoc.SelectNodes(@"certifications/certification");

        foreach (XmlNode tmp in list)
        {
            CertificationModel certification = new CertificationModel(tmp);
            CertificationList.Add(certification);
        }
	}

    public List<CertificationModel> GetAllCertificats()
    {
        return CertificationList;
    }
}