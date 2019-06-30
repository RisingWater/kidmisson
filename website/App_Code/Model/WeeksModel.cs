using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class WeekMissionDoneModel : BaseModel
{
    public WeekMissionDoneModel(XmlNode node, Database db)
        : base(node, db)
	{

    }

    public Int32 MissionId
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"week_mission_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}

public class WeeksModel : BaseModel
{
    private List<WeekMissionDoneModel> m_pCompleteList;

    public WeeksModel(XmlNode node, Database db)
        : base(node, db)
	{
        m_pCompleteList = new List<WeekMissionDoneModel>();
        XmlNodeList list = node.SelectNodes(@"week_mission_done");
        foreach (XmlNode tmp in list)
        {
            WeekMissionDoneModel m = new WeekMissionDoneModel(tmp, db);
            m_pCompleteList.Add(m);
        }
    }

    public String Id
    {
        get
        {
            return getAttributesValue(@"id");
        }
    }

    public Int32 Progress
    {
        get
        {
            Int32 Id = 10000;
            String tmp = getAttributesValue(@"number");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public void UpdateProgress(Int32 delta)
    {
        Lock();

        Int32 NewProgress = Progress + delta;
        setAttributesValue(@"number", NewProgress.ToString());

        Unlock();
    }


    public List<WeekMissionDoneModel> GetCompleteMissionList
    {
        get
        {
            return m_pCompleteList;
        }
    }

    public Boolean IsMissionCompleted(Int32 MissionId)
    {
        Boolean ret = false;
        Lock();
        foreach (WeekMissionDoneModel tmp in GetCompleteMissionList)
        {
            if (tmp.MissionId == MissionId)
            {
                ret = true;
                break;
            }
        }

        Unlock();

        return ret;
    }

    public Boolean MissionComplete(Int32 MissionId)
    {
        XmlDocument doc = m_pNode.OwnerDocument;
        Lock();
        if (IsMissionCompleted(MissionId))
        {
            Unlock();
            return false;
        }

        XmlElement element = doc.CreateElement(@"week_mission_done");
        XmlAttribute attr = doc.CreateAttribute(@"week_mission_id");
        attr.Value = MissionId.ToString();
        element.Attributes.Append(attr);

        m_pNode.AppendChild(element);

        WeekMissionDoneModel m = new WeekMissionDoneModel(element, m_pDB);
        m_pCompleteList.Add(m);

        m_pDB.SaveDbToFile();

        Unlock();

        return true;
    }
}