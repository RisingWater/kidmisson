using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PocketItemController
{
    private String m_pUserid;
    private PocketItemModule m_pModule;

    public PocketItemController(PocketItemModule module)
    {
        m_pModule = module;
    }

    //public String Description
    //{
    //    get
    //    {
    //        ItemModel model = 
    //    }
    //}

    //public String ItemImageFileName
    //{
    //    get
    //    {
    //        return getAttributesValue(@"image");
    //    }
    //}

    public Int32 Number
    {
        get
        {
            return m_pModule.Number;
        }
    }

    public void UpdateNumber(Int32 delta)
    {
        m_pModule.UpdateNumber(delta);

        return;
    }
}

public class PocketController
{
    private PocketModule m_pModel;

    public PocketController(String userid)
    {
        MainDatabase db = MainDatabaseManager.GetDatabase(userid);
        m_pModel = db.GetPocketModel();
    }

    public Int32 Point
    {
        get
        {
            return m_pModel.Point.Point;
        }
    }

    public void UpdatePoint(Int32 delta)
    {
        m_pModel.Point.UpdatePoint(delta);
        return;
    }

    public List<PocketItemController> ItemList
    {
        get
        {
            List<PocketItemController> ret = new List<PocketItemController>();
            foreach (PocketItemModule tmp in m_pModel.ItemList)
            {
                PocketItemController item = new PocketItemController(tmp);
                ret.Add(item);
            }

            return ret;
        }
    }
}