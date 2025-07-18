using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
    [SerializeField] itemHealthBehavior _healthItem;
    [SerializeField] itemXPBehavior _xpItem;
    public float currentPickupDistance = 6f;
    public float currentHealingPickup = 10f;
    public static Survivor instance;
    private float regenRate = 5f;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentHealingPickup = 10f;



    }

    private void Update()
    {
        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.survivorCategory))
        {

            if (Player.instance._currentLife < Player.instance.maxLife)
            {
                Player.instance._currentLife += regenRate * Time.deltaTime;
                Player.instance._currentLife = Mathf.Min(Player.instance._currentLife, Player.instance.maxLife);
            }
        }
    }
    public void UpgradeLife()
    {
        if ( Player.instance != null )
        {
            var result = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor);
            Player.instance.maxLife = result;
            Player.instance._currentLife = result;
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
