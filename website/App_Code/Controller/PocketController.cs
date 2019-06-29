using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PocketItemController
{
    private PocketItemModule m_pModule;
    private MainDatabase m_pDB;

    public PocketItemController(PocketItemModule module, MainDatabase db)
    {
        m_pModule = module;
        m_pDB = db;
    }

    public Int32 Id
    {
        get
        {
            return m_pModule.Id;
        }
    }

    public String Description
    {
        get
        {
            ItemModel model = m_pDB.GetItemModel(m_pModule.Id);
            if (model != null)
            {
                return model.Description;
            }
            else
            {
                return "";
            }
        }
    }

    public String ItemImageFileName
    {
        get
        {
            ItemModel model = m_pDB.GetItemModel(m_pModule.Id);
            if (model != null)
            {
                return model.ItemImageFileName;
            }
            else
            {
                return "";
            }
        }
    }

    public Int32 Number
    {
        get
        {
            return m_pModule.Number;
        }
    }

    public Int32 ExchangeUnit
    {
        get
        {
            ItemModel model = m_pDB.GetItemModel(m_pModule.Id);
            if (model != null)
            {
                return model.ExchangeUnit;
            }
            else
            {
                return 0;
            }
        }
    }

    public Boolean Exchange()
    {
        Int32 delta = -1 * ExchangeUnit;
        return m_pModule.UpdateNumber(delta);
    }
}

public class PocketController
{
    private PocketModule m_pModel;
    private MainDatabase m_pDB;
    
    public PocketController(String userid)
    {
        m_pDB = MainDatabaseManager.GetDatabase(userid);
        m_pModel = m_pDB.GetPocketModel();
    }

    public Int32 Point
    {
        get
        {
            return m_pModel.Point.Point;
        }
    }

    public Boolean UpdatePoint(Int32 delta)
    {
        return m_pModel.Point.UpdatePoint(delta);
    }

    public List<PocketItemController> ItemList
    {
        get
        {
            List<PocketItemController> ret = new List<PocketItemController>();
            foreach (PocketItemModule tmp in m_pModel.ItemList)
            {
                PocketItemController item = new PocketItemController(tmp, m_pDB);
                ret.Add(item);
            }

            return ret;
        }
    }

    static public List<PocketItemController> GetItemList(String userid)
    {
        PocketController Controller = new PocketController(userid);

        return Controller.ItemList;
    }

    static public PocketItemController GetItem(String userid, Int32 item_id)
    {
        List<PocketItemController> list = PocketController.GetItemList(userid);
        foreach (PocketItemController tmp in list)
        {
            if (tmp.Id == item_id)
            {
                return tmp;
            }
        }

        return null;
    }
}