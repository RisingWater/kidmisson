using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class DetailController
{
    private HistoryRecordModel m_pModel;

    public DetailController(HistoryRecordModel model)
    {
        m_pModel = model;
    }

    public String Time
    {
        get
        {
            return m_pModel.Time;
        }
    }

    public String Description
    {
        get
        {
            return m_pModel.Description;
        }
    }

    public Int32 Point
    {
        get
        {
            return m_pModel.Point;
        }
    }
}

public class RecordController
{
    private HistoryRecordsModel m_pModel;
    private MainDatabase m_pDb;

    public RecordController(HistoryRecordsModel model, MainDatabase db)
    {
        m_pModel = model;
        m_pDb = db;
    }

    public Int32 Id
    {
        get
        {
            return m_pModel.Id;
        }
    }

    public String Description
    {
        get
        {
            return m_pModel.Description;
        }
    }

    static public List<DetailController> GetDetailsList(String userid, Int32 record_id)
    {
        List<DetailController> detaillist = new List<DetailController>();
        RecordController selectController = null;
        List<RecordController> recordlist = HistoryController.GetHistoryRecords(userid);
        foreach (RecordController tmp in recordlist)
        {
            if (tmp.Id == record_id)
            {
                selectController = tmp;
                break;
            }
        }

        if (selectController != null)
        {
            List<HistoryRecordModel> detailmodels = selectController.m_pModel.Details;
            foreach (HistoryRecordModel tmp in detailmodels)
            {
                DetailController c = new DetailController(tmp);
                detaillist.Add(c);
            }
        }

        return detaillist;
    }

    static public void AddDetail(String userid, Int32 record_id, String description, Int32 point)
    {
        List<DetailController> detaillist = new List<DetailController>();
        RecordController selectController = null;
        List<RecordController> recordlist = HistoryController.GetHistoryRecords(userid);
        foreach (RecordController tmp in recordlist)
        {
            if (tmp.Id == record_id)
            {
                selectController = tmp;
                break;
            }
        }

        if (selectController != null)
        {
            selectController.m_pModel.AddDetail(DateTime.Now.ToString(), description, point);
        }
    }
}

public class HistoryController
{
    private MainDatabase m_pDb;
    private HistoryModel m_pHistoryModel;
	public HistoryController(String userid)
	{
        m_pDb = MainDatabaseManager.GetDatabase(userid);
        m_pHistoryModel = m_pDb.GetHistoryModule();
	}

    static public List<RecordController> GetHistoryRecords(String userid)
    {
        HistoryController Control = new HistoryController(userid);
        List<RecordController> list = new List<RecordController>();

        List<HistoryRecordsModel> Modellist = Control.m_pHistoryModel.GetRecords;
        foreach (HistoryRecordsModel tmp in Modellist)
        {
            RecordController c = new RecordController(tmp, Control.m_pDb);
            list.Add(c);
        }

        return list;
    }
}