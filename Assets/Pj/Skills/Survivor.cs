using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField] itemHealthBehavior _healthItem;
    [SerializeField] itemXPBehavior _xpItem;
    public float currentPickupDistance = 6f;
    public float currentHealingPickup;
    public static Survivor instance;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UpgradeLife()
    {
        if ( Player.instance != null )
        {
            var result = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor);
            Player.instance.maxLife = result;
        }
    }

    public void UpgradePickupDistance()
    {
        if ( Player.instance != null )
        {
            currentPickupDistance = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.ratioPickUp);
        }
    }

    public void UpgradePickupHealing()
    {
        if ( Player.instance != null )
        {
            currentHealingPickup = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.healingPickup);
        }
    }

    public float GetCurrentPickupDistance() => currentPickupDistance;
    public float GetCurrentHealingPickup() => currentHealingPickup;
}
