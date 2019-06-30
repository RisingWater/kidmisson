using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

public class AchievementModel : BaseModel
{
    public AchievementModel(XmlNode node, Database db)
        : base(node, db)
    {

    }

    public Int32 Id
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

    public String Description
    {
        get
        {
            return getAttributesValue(@"description");
        }
    }

    public Boolean Done
    {
        get
        {
            String tmp = getAttributesValue(@"done");
            if (tmp != null && String.Compare(tmp, "0") != 0)
            {
                return true;
            }

            return false;
        }
    }

    public void SetDone()
    {
        setAttributesValue(@"done", "1");
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
}

public class AchievementGroupModel : BaseModel, IComparable<AchievementGroupModel>
{
    private List<AchievementModel> m_pAchievementList;

    public AchievementGroupModel(XmlNode node, Database db)
        : base(node, db)
	{
        m_pAchievementList = new List<AchievementModel>();
        XmlNodeList nodelist = node.SelectNodes(@"achievement");
        foreach (XmlNode tmp in nodelist)
        {
            AchievementModel m = new AchievementModel(tmp, db);
            m_pAchievementList.Add(m);
        }
	}

    public int CompareTo(AchievementGroupModel other)
    {
        if (other == null)
        {
            return 1;
        }

        return DisplayIndex.CompareTo(other.DisplayIndex);
    }

    public Int32 Id
    {
        get
        {
            Int32 Id = -1;
            String tmp = getAttributesValue(@"achievement_group_id");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
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

    public Int32 Progress
    {
        get
        {
            Int32 Id = 0;
            String tmp = getAttributesValue(@"progress");
            if (tmp != null)
            {
                Id = Convert.ToInt32(tmp);
            }

            return Id;
        }
    }

    public String ImageFileName
    {
        get
        {
            return getAttributesValue(@"image");
        }
    }

    public void SetProgress(Int32 delta)
    {
        Lock();

        Int32 NewValue = Progress + delta;
        setAttributesValue(@"progress", NewValue.ToString());

        Unlock();
    }

    public AchievementModel GetLastAchievement
    {
        get
        {
            AchievementModel ret = null;
            foreach (AchievementModel tmp in m_pAchievementList)
            {
                ret = tmp;
                if (!ret.Done)
                {
                    break;
                }
            }

            return ret;
        }
    }
}

public class AchievementsModel
{
    private List<AchievementGroupModel> m_pGroupList;

    public AchievementsModel(XmlNode node, Database db)
	{
        m_pGroupList = new List<AchievementGroupModel>();

        XmlNodeList nodelist = node.SelectNodes(@"achievement_group");
        foreach (XmlNode tmp in nodelist)
        {
            AchievementGroupModel m = new AchievementGroupModel(tmp, db);
            m_pGroupList.Add(m);
            m_pGroupList.Sort();
        }
	}

    public List<AchievementGroupModel> AchievementGroupList
    {
        get
        {
            return m_pGroupList;
        }
    }

    public AchievementGroupModel GetAchievementGroup(Int32 GroupId)
    {
        AchievementGroupModel ret = null;
        foreach (AchievementGroupModel tmp in AchievementGroupList)
        {
            if (tmp.Id == GroupId)
            {
                ret = tmp;
                break;
            }
        }

        return ret;
    }

}