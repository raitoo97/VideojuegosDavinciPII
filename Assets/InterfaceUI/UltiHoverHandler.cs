using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltiHoverHandler : MonoBehaviour
{
    public static UltiHoverHandler instance;

    public GameObject panel;
    public Text ultiText;

    Dictionary<SkillCategory, string> ultiDescription;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        panel.SetActive(false);

        
    }

    private void Update()
    {
        ultiDescription = new Dictionary<SkillCategory, string>()
        {
            {SkillCategory.turretCategory, "Fires a powerful shot with a large blast radius, damaging multiple enemies at once. COST: " + ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.turretCategory)},
            {SkillCategory.shieldCategory, "Slows down time itself, giving you a brief moment of clarity and control. COST: " + ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.shieldCategory)},
            {SkillCategory.dashCategory, "Your dash leaves a trail of energy that damages enemies, turrets are unaffected. COST: " + ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.dashCategory)},
            {SkillCategory.survivorCategory, "Gain pasive health regeneration. COST: " + ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.survivorCategory)},
            {SkillCategory.dopplegangerCategory, "Your doppelgangers explode upon death, dealing damage to nearby enemies. COST: " + ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.dopplegangerCategory)}
        };
    }
    public void ShowPanel(SkillCategory category)
    {
        if (ultiDescription.TryGetValue(category, out string description))
        {
            ultiText.text = description;
        }
        else
        {
            ultiText.text = "No description available";
        }
            panel.SetActive(true);
    }

    public void HidePanel()
    {
        panel.SetActive(false); 
    }
}
