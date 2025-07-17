using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHoverHandler : MonoBehaviour
{
    public static SkillHoverHandler instance;
    [SerializeField] GameObject skillPanel;
    [SerializeField] Text skillText;

    private Dictionary<(SkillCategory, SkillStatType), string> skillDescription;
    private void Awake()
    {
        instance = this;
        skillPanel.SetActive(false);

       
    }

    private void Update()
    {
        skillDescription = new Dictionary<(SkillCategory, SkillStatType), string>
        {
            {
                (SkillCategory.turretCategory, SkillStatType.turretVisionRange),
                "Increase the turret's vision range, allowing it to detect enemies from farther away. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory,SkillStatType.turretVisionRange)
            },
            {
                (SkillCategory.turretCategory, SkillStatType.turretShotSpeed),
                "Increase the turret's fire rate, resulting in more shots fired per minute. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory,SkillStatType.turretShotSpeed)
            },
            {
                (SkillCategory.dashCategory, SkillStatType.dashCooldown),
                "Reduces the cooldown time of the dash ability, allowing you to dash more often. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory,SkillStatType.dashCooldown)
            },
            {
                (SkillCategory.dashCategory, SkillStatType.dashSpeed),
                "Increase the dash overall speed, allowing you to travel farther. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory,SkillStatType.dashSpeed)
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldRadius),
                "Increase the size of the shield. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory,SkillStatType.shieldRadius)
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldDuration),
                "Increases the duration of the protective shield. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory,SkillStatType.shieldDuration)
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldCooldown),
                "Reduces the cooldown time of the shield ability, allowing you to use the ability more often. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory,SkillStatType.shieldCooldown)
            },
            {
                (SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife),
                "Increase the life of the doppleganger, allowing to endure more damage before diying. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.dopplegangerCategory,SkillStatType.dopplegangerLife)
            },
            {
                (SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger),
                "Reduces the cooldown time, allowing you to use the ability more often. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.dopplegangerCategory,SkillStatType.coldowndoppleganger)
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.lifeSurvivor),
                "Boosts your maximum health, increasing your survivability. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory,SkillStatType.lifeSurvivor)
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.ratioPickUp),
                "Increases the magnetic field, allowing you to collect items from a greater distance. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory,SkillStatType.ratioPickUp)
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.healingPickup),
                "Enhances the healing properties of medical items. COST: " + ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory,SkillStatType.healingPickup)
            },
        };
    }
    public void ShowPanel(SkillCategory category, SkillStatType stat)
    {
        if (skillDescription.TryGetValue((category,stat), out string description))
        {
            skillText.text = description;
        }
        else
        {
            skillText.text = "No description available";
        }

        skillPanel.SetActive(true);
    }

    public void HidePanel()
    {
        skillPanel.SetActive(false);
    }
}
