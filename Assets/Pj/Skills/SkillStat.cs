using System;
public enum SkillStatType
{
    turretVisionRange,
    turretShotSpeed,
}
[Serializable]
public class SkillStat
{
    public SkillStatType skillType;
    public float[] valuesByLevel;
    public float GetValue(int level)
    {
        return valuesByLevel[Math.Clamp(level, 0, valuesByLevel.Length - 1)];
    }
    public int GetMaxLevel()
    {
        return valuesByLevel.Length - 1;
    }
}
