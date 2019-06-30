using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

public class WeekController
{
    private WeeksModel m_pModel;
    private MainDatabase m_pDb;

    public WeekController(String userid)
    {
        m_pDb = MainDatabaseManager.GetDatabase(userid);

        DateTime time = DateTime.Now;
        GregorianCalendar calendar = new GregorianCalendar();
        int weekOfYears = calendar.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        String weekKey = time.Year + "-" + weekOfYears;

        m_pModel = m_pDb.GetWeekyState(weekKey);
    }

    public Int32 Progress
    {
        get
        {
            return m_pModel.Progress;
        }
    }

    public Int32 Target
    {
        get
        {
            List<WeekMissionModel> week_mission_list = m_pDb.GetWeekyMissionList();
            WeekMissionModel model = null;
            foreach (WeekMissionModel tmp in week_mission_list)
            {
                model = tmp;
                if (!m_pModel.IsMissionCompleted(model.Id))
                {
                    break;
                }
            }

            return model.Target;
        }
    }

    public Int32 WeekMissionId
    {
        get
        {
            List<WeekMissionModel> week_mission_list = m_pDb.GetWeekyMissionList();
            WeekMissionModel model = null;
            foreach (WeekMissionModel tmp in week_mission_list)
            {
                model = tmp;
                if (!m_pModel.IsMissionCompleted(model.Id))
                {
                    break;
                }
            }

            return model.Id;
        }
    }

    public Boolean Done
    {
        get
        {
            List<WeekMissionModel> week_mission_list = m_pDb.GetWeekyMissionList();
            WeekMissionModel model = null;
            foreach (WeekMissionModel tmp in week_mission_list)
            {
                model = tmp;
                if (!m_pModel.IsMissionCompleted(model.Id))
                {
                    break;
                }
            }

            return m_pModel.IsMissionCompleted(model.Id);
        }
    }

    public Boolean GetAward(ref String ItemName)
    {
        Int32 WeekId = WeekMissionId;
        if (m_pModel.MissionComplete(WeekId))
        {
            WeekMissionModel mission = m_pDb.GetWeekMissionModel(WeekId);
            ;
            ItemModel item = m_pDb.GetItemModel(mission.Award.ItemId);
            ItemName = item.Description + mission.Award.Number + "个";

            PocketModule pocket = m_pDb.GetPocketModel();
            List<PocketItemModule> itemlist = pocket.ItemList;
            foreach (PocketItemModule tmp in itemlist)
            {
                if (tmp.Id == mission.Award.ItemId)
                {
                    tmp.UpdateNumber(mission.Award.Number);
                }
            }

            return true;
        }

        return false;
    }
}