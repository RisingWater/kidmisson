using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class WeekMissionAwardModel : BaseModel
{
    public WeekMissionAwardModel(XmlNode node, Database db)
        : base(node, db)
	{
	    
	}

    public Int32 ItemId
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"item_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Int32 Number
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"number");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}

public class WeekMissionModel : BaseModel, IComparable<WeekMissionModel>
{
    private WeekMissionAwardModel m_pAward;

	public WeekMissionModel(XmlNode node, Database db) : base(node, db)
	{
        XmlNode award_node = node.SelectSingleNode(@"award_item");
        m_pAward = new WeekMissionAwardModel(award_node, db);
	}

    public int CompareTo(WeekMissionModel other)
    {
        if (other == null)
        {
            return 1;
        }

        return Id.CompareTo(other.Id);
    }

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public Int32 Id
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

    public Int32 Target
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"target");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public WeekMissionAwardModel Award
    {
        get
        {
            return m_pAward;
        }
    }
}

public class DailyMissionAssociatedAchievementModel : BaseModel
{
    public DailyMissionAssociatedAchievementModel(XmlNode node, Database db)
        : base(node, db)
    {

    }

    public Int32 AchievementId
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"achievement_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Int32 Progress
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"number");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }
}

public class DailyMissionModel : BaseModel, IComparable<DailyMissionModel>
{
    List<DailyMissionAssociatedAchievementModel> m_pAssociateAchievementList;

    public DailyMissionModel(XmlNode node, Database db)
        : base(node, db)
    {
        m_pAssociateAchievementList = new List<DailyMissionAssociatedAchievementModel>();
        XmlNodeList list = node.SelectNodes(@"associate_achevement");

        foreach (XmlNode tmp in list)
        {
            DailyMissionAssociatedAchievementModel model = new DailyMissionAssociatedAchievementModel(tmp, db);
            m_pAssociateAchievementList.Add(model);
        }
    }

    public int CompareTo(DailyMissionModel other)
    {
        if (other == null)
        {
            return 1;
        }

        return DisplayIndex.CompareTo(other.DisplayIndex);
    }

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public String Name
    {
        get
        {
            return getAttributesValue(@"name");
        }
    }

    public String ImageFileName
    {
        get
        {
            return getAttributesValue(@"image");
        }
    }

    public String ImageDoneFileName
    {
        get
        {
            return getAttributesValue(@"image_done");
        }
    }


    public Int32 DisplayIndex
    {
        get
        {
            Int32 Id = 10000;
            String tmp = getAttributesValue(@"display_index");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public Int32 Id
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

    public Int32 Award
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"award");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public List<DailyMissionAssociatedAchievementModel> AssociatedAchievementModelList
    {
        get
        {
            return m_pAssociateAchievementList;
        }
    }
}

