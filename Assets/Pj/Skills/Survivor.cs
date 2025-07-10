using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    public static Survivor instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        
    }
    public void UpgradeLife()
    {
        if ( Player.instance != null )
        {
            var result = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor);
            Player.instance.maxLife = result;
            Debug.Log("GET LEVEL: " + ManagerSkills.instance.GetLevel(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor));
        }
    }
}
