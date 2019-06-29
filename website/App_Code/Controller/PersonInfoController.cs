using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

public class PersonInfoController
{
    private PersonInfoModel m_pModel;

	public PersonInfoController(String userid)
	{
        MainDatabase db = MainDatabaseManager.GetDatabase(userid);
        m_pModel = db.GetUserInfo();
	}

    public String Name
    {
        get
        {
            return m_pModel.Name;
        }
    }

    public String School
    {
        get
        {
            return m_pModel.School;
        }
    }

    public String Grade
    {
        get
        {
            return m_pModel.Grade;
        }
    }

    public String HeadImageFileName
    {
        get
        {
            return m_pModel.HeadImageFileName;
        }
    }

    public DateTime Birthday
    {
        get
        {
            DateTime time = new DateTime(2000, 1, 1);
            String tmp = m_pModel.Birthday;
            if (tmp != null)
            {
                try
                {
                    DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();

                    dtFormat.ShortDatePattern = "yyyy/MM/dd";

                    time = Convert.ToDateTime(tmp, dtFormat);
                }
                catch
                {

                }
            }
            return time;
        }
    }

    public Int32 Age
    {
        get
        {
            DateTime tmp = Birthday;
            Int32 age = 0;
            while (true)
            {
                tmp = tmp.AddYears(1);

                if (DateTime.Compare(tmp, DateTime.Now) > 0)
                {
                    break;
                }

                age++;
            }

            return age;
        }
    }
}