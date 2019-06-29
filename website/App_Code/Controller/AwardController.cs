using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AwardBoxController
{
    private AwardBoxModel m_pModel;
    private MainDatabase m_pDb;

    public AwardBoxController(AwardBoxModel model, MainDatabase db)
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

    public Int32 Cost
    {
        get
        {
            return m_pModel.Cost;
        }
    }

    public String Description
    {
        get
        {
            return m_pModel.Description;
        }
    }

    public String AwardImageFileName
    {
        get
        {
            return m_pModel.AwardImageFileName;
        }
    }

    public Boolean OpenBox(ref String ItemName)
    {
        PocketModule pocket = m_pDb.GetPocketModel();
        Int32 delta = -1 * Cost;

        if (pocket.Point.UpdatePoint(delta))
        {
            Int32 TotalWeight = 0;
            List<AwardItemModel> list = m_pModel.ItemList;
            foreach (AwardItemModel tmp in list)
            {
                TotalWeight += tmp.Weight;
            }

            Random r = new Random();
            Int32 result = r.Next(TotalWeight);

            AwardItemModel SelectItem = null;
            foreach (AwardItemModel tmp in list)
            {
                result -= tmp.Weight;
                if (result <= 0)
                {
                    SelectItem = tmp;
                    break;
                }
            }

            Int32 Item_id = SelectItem.ItemId;
            Int32 Count = SelectItem.Number;

            if (Item_id >= 0)
            {
                ItemModel item = m_pDb.GetItemModel(Item_id);
                ItemName = item.Description + Count + "个";

                List<PocketItemModule> itemlist = pocket.ItemList;
                foreach (PocketItemModule tmp in itemlist)
                {
                    if (tmp.Id == Item_id)
                    {
                        tmp.UpdateNumber(Count);
                    }
                }
            }
            else
            {
                ItemName = SelectItem.ItemName;
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}

public class AwardController
{
    private MainDatabase m_pDb;
    private AwardModel m_pAwardModel;

    public AwardController(String userid)
	{
        m_pDb = MainDatabaseManager.GetDatabase(userid);
        m_pAwardModel = m_pDb.GetAwardModule();
	}

    static public List<AwardBoxController> GetAwardBoxs(String userid)
    {
        AwardController Control = new AwardController(userid);
        List<AwardBoxController> list = new List<AwardBoxController>();

        List<AwardBoxModel> Modellist = Control.m_pAwardModel.AwardList;
        foreach (AwardBoxModel tmp in Modellist)
        {
            AwardBoxController c = new AwardBoxController(tmp, Control.m_pDb);
            list.Add(c);
        }

        return list;
    }

    static public AwardBoxController GetAwardBox(String userid, Int32 award_id)
    {
        List<AwardBoxController> list = AwardController.GetAwardBoxs(userid);
        foreach (AwardBoxController tmp in list)
        {
            if (tmp.Id == award_id)
            {
                return tmp;
            }
        }

        return null;
    }
}