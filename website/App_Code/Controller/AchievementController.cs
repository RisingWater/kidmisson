using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AchievementGroupController
{
    private AchievementGroupModel m_pModel;
    private MainDatabase m_pDb;

    public AchievementGroupController(AchievementGroupModel model, MainDatabase db)
    {
        m_pModel = model;
        m_pDb = db;
    }

    public String Description
    {
        get
        {
            AchievementModel m = m_pModel.GetLastAchievement;
            if (m != null)
            {
                return m.Description;
            }

            return "";
        }
    }

    public Int32 Target
    {
        get
        {
            AchievementModel m = m_pModel.GetLastAchievement;
            if (m != null)
            {
                return m.Target;
            }

            return 0;
        }
    }

    public Int32 Award
    {
        get
        {
            AchievementModel m = m_pModel.GetLastAchievement;
            if (m != null)
            {
                return m.Award;
            }

            return 0;
        }
    }

    public Boolean Done
    {
        get
        {
            AchievementModel m = m_pModel.GetLastAchievement;
            if (m != null)
            {
                return m.Done;
            }

            return true;
        }
    }

    public Int32 Id
    {
        get
        {
            return m_pModel.Id;
        }
    }

    public Int32 Progress
    {
        get
        {
            return m_pModel.Progress;
        }
    }

    public String ImageFileName
    {
        get
        {
            return m_pModel.ImageFileName;
        }
    }

    public Boolean GetAward()
    {
        if (Progress < Target)
        {
            return false;
        }

        Int32 delta = Award;

        AchievementModel m = m_pModel.GetLastAchievement;
        m.SetDone();

        PocketModule pocket = m_pDb.GetPocketModel();

        pocket.Point.UpdatePoint(delta);

        return true;
    }
}

public class AchievementController
{
    private MainDatabase m_pDb;
    private AchievementsModel m_pModel;
    public AchievementController(String userid)
	{
        m_pDb = MainDatabaseManager.GetDatabase(userid);
        m_pModel = m_pDb.GetAchievementsModel();
	}

    static public List<AchievementGroupController> GetAchievementGroupList(String userid)
    {
        AchievementController Control = new AchievementController(userid);
        List<AchievementGroupController> list = new List<AchievementGroupController>();

        List<AchievementGroupModel> Modellist = Control.m_pModel.AchievementGroupList;
        foreach (AchievementGroupModel tmp in Modellist)
        {
            AchievementGroupController c = new AchievementGroupController(tmp, Control.m_pDb);
            list.Add(c);
        }

        return list;
    }

    static public AchievementGroupController GetAchievementGroup(String userid, Int32 award_group_id)
    {
        List<AchievementGroupController> list = AchievementController.GetAchievementGroupList(userid);
        foreach (AchievementGroupController tmp in list)
        {
            if (tmp.Id == award_group_id)
            {
                return tmp;
            }
        }

        return null;
    }
}