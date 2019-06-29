using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CertificationController
{
    private CertificationDatabase m_DbRef = null;

	public CertificationController()
	{
        m_DbRef = CertificationDatabase.GetInstance();
	}

    public Boolean IsUserIdValid(String userid)
    {
        List<CertificationModel> list = m_DbRef.GetAllCertificats();

        foreach (CertificationModel tmp in list)
        {
            if (String.Compare(userid, tmp.UserId, true) == 0)
            {
                return true;
            }
        }

        return false;
    }

    public String GetUserId(String username, String password)
    {
        String userid = null;
        List<CertificationModel> list = m_DbRef.GetAllCertificats();

        foreach (CertificationModel tmp in list)
        {
            if (tmp.Username == null || tmp.EncodedPassword == null)
            {
                continue;
            }

            if (String.Compare(username, tmp.Username, true) == 0
                && String.Compare(password, tmp.EncodedPassword) == 0)
            {
                userid = tmp.UserId;
                break;
            }
        }

        return userid;
    }
}