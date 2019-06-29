using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class HistoryRecordsModel : BaseModel
{
    private List<HistoryRecordModel> m_pDetails;

    public HistoryRecordsModel(XmlNode node, Database db)
        : base(node, db)
    {
        m_pDetails = new List<HistoryRecordModel>();

        XmlNodeList list = m_pNode.SelectNodes(@"record");

        foreach (XmlNode tmp in list)
        {
            HistoryRecordModel item = new HistoryRecordModel(tmp, m_pDB);
            m_pDetails.Add(item);
        }
    }

    public Int32 Id
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"record_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public List<HistoryRecordModel> Details
    {
        get
        {
            return m_pDetails;
        }
    }

    public void AddDetail(String time, String description, Int32 point)
    {
        XmlDocument doc = m_pNode.OwnerDocument;
        XmlElement node = doc.CreateElement("record");
        XmlAttribute attr;
        attr = doc.CreateAttribute("time");
        attr.Value = time;
        node.Attributes.Append(attr);

        attr = doc.CreateAttribute("description");
        attr.Value = description;
        node.Attributes.Append(attr);

        if (point != 0)
        {
            attr = doc.CreateAttribute("point");
            attr.Value = description;
            node.Attributes.Append(attr);
        }

        Lock();
        HistoryRecordModel item = new HistoryRecordModel(node, m_pDB);
        m_pDetails.Add(item);
        m_pNode.AppendChild(node);
        m_pDB.SaveDbToFile();
        Unlock();
    }
}

public class HistoryModel
{
    private XmlNode m_pNode;
    private List<HistoryRecordsModel> m_pRecords;
    public HistoryModel(XmlNode node, Database db)
    {
        m_pNode = node;
        m_pRecords = new List<HistoryRecordsModel>();

        XmlNodeList list = m_pNode.SelectNodes(@"records");

        foreach (XmlNode tmp in list)
        {
            HistoryRecordsModel item = new HistoryRecordsModel(tmp, db);
            m_pRecords.Add(item);
        }
    }

    public List<HistoryRecordsModel> GetRecords
    {
        get
        {
            return m_pRecords;
        }
    }
}