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
            _skills[entry.category] = new ActiveSkill(entry.category, entry.level, entry.dataStrcut); 
        }
    }
    public void UpgradeSkill(SkillCategory category)
    {
        if(!_skills.ContainsKey(category)) return;
        ActiveSkill skill = _skills[category]; 
        int MaxLevel = 0;
        foreach(var entry in skill.dataStrcut.dataScripteable)
        {
            MaxLevel = Math.Max(entry.GetMaxLevel(), MaxLevel);
        }
        if(skill.level < MaxLevel)
        {
            skill.level++;
            _skills[category] = skill;
        }
    }
    public float GetValueSkill(SkillCategory category, SkillStatType specificType)
    {
        if (!_skills.ContainsKey(category)) return 0;
        ActiveSkill skill = _skills[category];
        int level = skill.level;
        foreach(var entry in skill.dataStrcut.dataScripteable)
        {
            if(entry.skillType == specificType)
            {
                return entry.GetValue(level);
            }
        }
        return 0f;
    }
}
[Serializable]
public struct ActiveSkill
{
    public SkillCategory category;
    public int level;
    public SkillCategoryData dataStrcut;

    public ActiveSkill(SkillCategory category, int level, SkillCategoryData dataStrcut)
    {
        this.category = category;
        this.level = level;
        this.dataStrcut = dataStrcut;
    }
}

