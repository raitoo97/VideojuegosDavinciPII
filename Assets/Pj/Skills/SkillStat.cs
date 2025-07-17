using System;
using UnityEngine;
public enum SkillStatType
{
    turretVisionRange,
    turretShotSpeed,
    dashSpeed,
    dashCooldown,
    shieldDuration,
    shieldRadius,
    shieldCooldown,
    dopplegangerLife,
    coldowndoppleganger,
    lifeSurvivor,
    ratioPickUp,
    healingPickup
    
}
[Serializable]
public class SkillStat
{
    public SkillStatType skillType;
    public float[] valuesByLevel;
    public float[] costPerLevel;
    public float GetValue(int level)
    {
        return valuesByLevel[Mathf.Clamp(level, 0, valuesByLevel.Length - 1)];
    }
    public float GetCost(int level)
    {
        return costPerLevel[Mathf.Clamp(level, 0, costPerLevel.Length - 1)];
    }
    public int GetMaxLevel()
    {
        return valuesByLevel.Length - 1;
    }
}
