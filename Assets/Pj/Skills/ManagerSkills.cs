using System;
using System.Collections.Generic;
using UnityEngine;
public enum SkillCategory
{
    turretCategory,
}
public class ManagerSkills : MonoBehaviour
{
    public List<ActiveSkill> skillEntries = new List<ActiveSkill>();
    private Dictionary<SkillCategory, ActiveSkill> _skills = new Dictionary<SkillCategory, ActiveSkill>();
    public static ManagerSkills instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        foreach(var entry in skillEntries)
        {
            _skills[entry.category] = new ActiveSkill(entry.category,entry.dataStrcut);
        }
    }
    public void UpgradeSkill(SkillCategory category, SkillStatType specificType)
    {
        if(!_skills.ContainsKey(category)) return;
        ActiveSkill skill = _skills[category];
        SkillStat targetStat = null;
        foreach (SkillStat entry in skill.dataStrcut.dataScripteable)
        {
            if(entry.skillType == specificType)
            {
                targetStat = entry;
                break;
            }
        }
        for(int i  = 0; i < skill.progressPerStat.Count; i++)
        {
            if(skill.progressPerStat[i].type == specificType)
            {
                int currentLevel = skill.progressPerStat[i].level;
                int maxLevel = targetStat.GetMaxLevel();
                if (currentLevel < maxLevel)
                {
                    skill.progressPerStat[i].level++;
                }
                break;
            }
        }
    }
    public float GetValueSkill(SkillCategory category, SkillStatType specificType)
    {
        if (!_skills.ContainsKey(category)) return 0;
        ActiveSkill skill = _skills[category];
        for (int i = 0; i < skill.progressPerStat.Count; i++)
        {
            if (skill.progressPerStat[i].type == specificType)
            {
                int currentLevel = skill.progressPerStat[i].level;
                foreach(var entry in skill.dataStrcut.dataScripteable)
                {
                    if(entry.skillType == specificType)
                    {
                        return entry.GetValue(currentLevel);
                    }
                }
            }
        }
            return 0f;
    }
}
public class StatProgress
{
    public SkillStatType type;
    public int level;
    public StatProgress(SkillStatType type, int level)
    {
        this.type = type;
        this.level = level; 
    }
}
[Serializable]
public class ActiveSkill
{
    public SkillCategory category;
    public List<StatProgress> progressPerStat;
    public SkillCategoryData dataStrcut;
    public ActiveSkill(SkillCategory category,SkillCategoryData dataStrcut)
    {
        this.category = category;
        this.progressPerStat = new List<StatProgress>();
        this.dataStrcut = dataStrcut;
        foreach (var data in dataStrcut.dataScripteable)
        {
            progressPerStat.Add(new StatProgress (data.skillType,0));
        }
    }
}

