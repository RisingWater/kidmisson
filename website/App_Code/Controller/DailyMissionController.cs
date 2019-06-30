using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

public class DailyMissionController
{
    private MainDatabase m_pDb;
    private DailyMissionModel m_pModel;

    public DailyMissionController(DailyMissionModel model, MainDatabase db)
    {
        m_pModel = model;
        m_pDb = db;
    }

    public String Description
    {
        get
        {
            return m_pModel.Description;
        }
    }

    public String Name
    {
        get
        {
            return m_pModel.Name;
        }
    }

    public Int32 Award
    {
        get
        {
            return m_pModel.Award;
        }
    }

    public Int32 Id
    {
        get
        {
            return m_pModel.Id;
        }
    }

    public Boolean Done
    {
        get
        {
            String key = DateTime.Now.ToShortDateString();
            DailyModel model = m_pDb.GetDailyState(key);
            return model.IsMissionCompleted(Id);
        }
    }

    public void SetDone()
    {
        String key = DateTime.Now.ToShortDateString();
        DailyModel model = m_pDb.GetDailyState(key);
        Boolean ret = model.MissionComplete(Id);

        if (ret)
        {
            PocketModule pocket = m_pDb.GetPocketModel();
            pocket.Point.UpdatePoint(Award);

            List<DailyMissionAssociatedAchievementModel> assolist = m_pModel.AssociatedAchievementModelList;
            foreach (DailyMissionAssociatedAchievementModel tmp in assolist)
            {
                AchievementGroupModel achi_model = m_pDb.GetAchievementsModel().GetAchievementGroup(tmp.AchievementId);
                if (achi_model != null)
                {
                    achi_model.SetProgress(tmp.Progress);
                }
            }

            DateTime time = DateTime.Now;
            GregorianCalendar calendar = new GregorianCalendar();
            int weekOfYears = calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            String weekKey = time.Year + "-" + weekOfYears;

            WeeksModel weekModel = m_pDb.GetWeekyState(weekKey);
            weekModel.UpdateProgress(Award);
        }
    }


    public String ImageFileName
    {
        get
        {
            if (Done)
            {
                return m_pModel.ImageDoneFileName;
            }
            else
            {
                return m_pModel.ImageFileName;
            }
        }
    }
}

public class DailyMissionsController
{
    private MainDatabase m_pDb;

    public DailyMissionsController(String userid)
	{
        m_pDb = MainDatabaseManager.GetDatabase(userid);
	}

    public static List<DailyMissionController> GetMissionList(String userid)
    {
        List<DailyMissionController> control_list = new List<DailyMissionController>();
        DailyMissionsController Controller = new DailyMissionsController(userid);
        List<DailyMissionModel> model_list = Controller.m_pDb.GetDailyMissionList();
        foreach (DailyMissionModel model in model_list)
        {
            DailyMissionController c = new DailyMissionController(model, Controller.m_pDb);
            control_list.Add(c);
        }

        return control_list;
    }

    public static DailyMissionController GetMission(String userid, Int32 MissionId)
    {
        List<DailyMissionController> list = DailyMissionsController.GetMissionList(userid);

        foreach (DailyMissionController tmp in list)
        {
            if (tmp.Id == MissionId)
            {
                return tmp;
            }
        }

        return null;
    }
}