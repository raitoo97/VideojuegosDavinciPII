using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField] itemHealthBehavior _healthItem;
    [SerializeField] itemXPBehavior _xpItem;
    public float currentPickupDistance = 6f;
    public static Survivor instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Debug.Log(ManagerSkills.instance.GetLevel(SkillCategory.survivorCategory, SkillStatType.ratioPickUp));
    }

    public void UpgradeLife()
    {
        if ( Player.instance != null )
        {
            var result = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor);
            Player.instance.maxLife = result;
        }
    }

    public void UpgradePickup()
    {
        if ( Player.instance != null )
        {
            currentPickupDistance = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.ratioPickUp);
        }
    }

    public float GetCurrentPickupDistance() => currentPickupDistance;
}
