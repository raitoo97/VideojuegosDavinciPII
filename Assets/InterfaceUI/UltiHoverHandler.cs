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

        ultiDescription = new Dictionary<SkillCategory, string>() 
        {
            {SkillCategory.turretCategory, "Fires a powerful shot with a large blast radius, damaging multiple enemies at once." },
            {SkillCategory.shieldCategory, "Slows down time itself, giving you a brief moment of clarity and control." },
            {SkillCategory.dashCategory, "Your dash tears through the ground, leaving a trail of energy that damages enemies, turrets are unaffected." },
            {SkillCategory.survivorCategory, "Gain pasive health regeneration." },
            {SkillCategory.dopplegangerCategory, "Your doppelgangers explode upon death, dealing damage to nearby enemies." }
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
