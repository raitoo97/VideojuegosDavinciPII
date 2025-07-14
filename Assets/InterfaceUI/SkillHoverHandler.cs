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

        skillDescription = new Dictionary<(SkillCategory, SkillStatType), string>
        {
            {
                (SkillCategory.turretCategory, SkillStatType.turretVisionRange), 
                "Increase the turret's vision range, allowing it to detect enemies from farther away."
            },
            {
                (SkillCategory.turretCategory, SkillStatType.turretShotSpeed), 
                "Increase the turret's fire rate, resulting in more shots fired per minute."
            },
            {
                (SkillCategory.dashCategory, SkillStatType.dashCooldown),
                "Reduces the cooldown time of the dash ability, allowing you to dash more often."
            },
            {
                (SkillCategory.dashCategory, SkillStatType.dashSpeed), 
                "Increase the dash overall speed, allowing you to travel farther."
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldRadius), 
                "Increase the size of the shield."
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldDuration),
                "Increases the duration of the protective shield."
            },
            {
                (SkillCategory.shieldCategory, SkillStatType.shieldCooldown),
                "Reduces the cooldown time of the shield ability, allowing you to use the ability more often."
            },
            {
                (SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife),
                "Increase the life of the doppleganger, allowing to endure more damage before diying."
            },
            {
                (SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger),
                "Reduces the cooldown time of the doppleganger ability, allowing you to use the ability more often."
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.lifeSurvivor),
                "Boosts your maximum health, increasing your survivability."
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.ratioPickUp),
                "Increases the magnetic field, allowing you to collect items from a greater distance."
            },
            {
                (SkillCategory.survivorCategory, SkillStatType.healingPickup),
                "Enhances the healing properties of medical items."
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
