using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class DailyMissionDoneModel : BaseModel
{
    public DailyMissionDoneModel(XmlNode node, Database db)
        : base(node, db)
	{

    }

    public Int32 MissionId
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"mission_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}

public class DailyModel : BaseModel
{
    private List<DailyMissionDoneModel> m_pCompleteList;

	public DailyModel(XmlNode node, Database db) : base(node, db)
	{
        m_pCompleteList = new List<DailyMissionDoneModel>();
        XmlNodeList list = node.SelectNodes(@"daily_mission_done");
        foreach (XmlNode tmp in list)
        {
            DailyMissionDoneModel m = new DailyMissionDoneModel(tmp, db);
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

    public List<DailyMissionDoneModel> GetCompleteMissionList
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
        foreach (DailyMissionDoneModel tmp in GetCompleteMissionList)
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

        XmlElement element = doc.CreateElement(@"daily_mission_done");
        XmlAttribute attr = doc.CreateAttribute(@"mission_id");
        attr.Value = MissionId.ToString();
        element.Attributes.Append(attr);

        m_pNode.AppendChild(element);

        DailyMissionDoneModel m = new DailyMissionDoneModel(element, m_pDB);
        m_pCompleteList.Add(m);

        m_pDB.SaveDbToFile();

        Unlock();

        return true;
    }
}