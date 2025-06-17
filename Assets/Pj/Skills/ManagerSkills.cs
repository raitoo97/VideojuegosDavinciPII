using System;
using System.Collections.Generic;
using UnityEngine;
public enum SkillCategory
{
    turretCategory,
    dashCategory
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
            _skills[entry.category] = new ActiveSkill(entry.category,entry.dataStrcut,entry.isUnlocked,entry.costToUnlock, entry.costToUnlockUltimate);
        }
    }
    public void UpgradeSkill(SkillCategory category, SkillStatType specificType)
    {
        if(!_skills.ContainsKey(category)) return;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked) return;
        SkillStat targetStat = null;
        foreach (SkillStat entry in skill.dataStrcut.dataScripteable)
        {
            if(entry.skillType == specificType)
            {
                targetStat = entry;
                break;
            }
        }
        for(int i = 0; i < skill.progressPerStat.Count; i++)
        {
            if(skill.progressPerStat[i].type == specificType)
            {
                int currentLevel = skill.progressPerStat[i].level;
                int maxLevel = targetStat.GetMaxLevel();
                float costLevel = GetValueSkillCost(category, specificType);
                if (currentLevel < maxLevel && PointManager.instance.SpendPoints(costLevel))
                {
                    skill.progressPerStat[i].level++;
                }
                break;
            }
        }
    }
    #region//GetValues
    public float GetValueSkill(SkillCategory category, SkillStatType specificType)
    {
        if (!_skills.ContainsKey(category)) return 0;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked) return 0f;
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
    public float GetLevel(SkillCategory category, SkillStatType specificType)
    {
        if (!_skills.ContainsKey(category)) return 0;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked) return 0f;
        for (int i = 0; i < skill.progressPerStat.Count; i++)
        {
            if (skill.progressPerStat[i].type == specificType)
            {
                int currentLevel = skill.progressPerStat[i].level;
                return currentLevel;
            }
        }
        return 0f;
    }
    private float GetValueSkillCost(SkillCategory category, SkillStatType specificType)
    {
        if (!_skills.ContainsKey(category)) return 0f;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked) return 0f;
        for (int i = 0;i < skill.progressPerStat.Count; i++)
        {
            if (skill.progressPerStat[i].type == specificType)
            {
                int currentlevel = skill.progressPerStat[i].level;
                foreach(var entry in skill.dataStrcut.dataScripteable)
                {
                    if(entry.skillType == specificType)
                    {
                        return entry.GetCost(currentlevel + 1);
                    }
                }
            }
        }
        return 0f;
    }
    public bool IsUnlocked(SkillCategory category)
    {
        if (!_skills.ContainsKey(category)) return false;
        ActiveSkill skill = _skills[category];
        return skill.isUnlocked;
    } 
    #endregion
    public void CanUnlockSkillCategory(SkillCategory category)
    {
        if (!_skills.ContainsKey(category)) return;
        ActiveSkill skill = _skills[category];
        if (skill.isUnlocked) return;
        if (PointManager.instance.SpendPoints(skill.costToUnlock))
        {
            skill.isUnlocked = true;
            var entry = skillEntries.Find(x => x.category == category);
            if (entry != null) entry.isUnlocked = true;
        }
    }
    public bool AreAllSkillsMaxed(SkillCategory category)
    {
        if (!_skills.ContainsKey(category)) return false;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked) return false;
        foreach (var progress in skill.progressPerStat)
        {
            SkillStat stat = skill.dataStrcut.dataScripteable.Find(x => x.skillType == progress.type);
            int maxLevel = stat.GetMaxLevel();
            if (progress.level < maxLevel)
            {
                print("No estan todas al maximo");
                return false;
            }
        }
        print("SIII estan todas al maximo");
        return true;
    }
    public void TryUnlockUltimate(SkillCategory category)
    {
        if (!_skills.ContainsKey(category)) return;
        ActiveSkill skill = _skills[category];
        if (!skill.isUnlocked || skill.ultimateUnlocked) return;
        if (!AreAllSkillsMaxed(category))
        {
            Debug.Log("No se puede desbloquear aún, faltan habilidades al máximo.");
            return;
        }
        if (PointManager.instance.SpendPoints(skill.costToUnlockUltimate))
        {
            skill.ultimateUnlocked = true;
            var entry = skillEntries.Find(x => x.category == category);
            if (entry != null) entry.ultimateUnlocked = true;
            Debug.Log("¡Mejora definitiva desbloqueada!");
        }
    }
    public bool IsUnlockUltimate(SkillCategory category)
    {
        if (!_skills.ContainsKey(category)) return false;
        ActiveSkill skill = _skills[category];
        return skill.ultimateUnlocked;
    }
}
[HideInInspector]
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
    public bool isUnlocked;
    public float costToUnlock;
    public bool ultimateUnlocked = false;
    public float costToUnlockUltimate;
    public ActiveSkill(SkillCategory category,SkillCategoryData dataStrcut,bool isUnlocked,float costToUnlock,float costToUnlockUltimate)
    {
        this.category = category;
        this.progressPerStat = new List<StatProgress>();
        this.dataStrcut = dataStrcut;
        this.isUnlocked = isUnlocked;
        this.costToUnlock = costToUnlock;
        this.costToUnlockUltimate= costToUnlockUltimate;
        foreach (var data in dataStrcut.dataScripteable)
        {
            progressPerStat.Add(new StatProgress (data.skillType,0));
        }
    }
}

