using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillHoverHandler : MonoBehaviour
{
    public static SkillHoverHandler instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text text;

    private Dictionary<(SkillCategory, SkillStatType), string> skillDescription;

    private void Awake()
    {
        instance = this;
        panel.SetActive(false);

        skillDescription = new Dictionary<(SkillCategory, SkillStatType), string>
        {
            {
                (SkillCategory.turretCategory, SkillStatType.turretVisionRange), 
                "Increase the turret's vision range, allowing it to detect enemies from farther away."
            },
        };
    }

    public void ShowPanel(SkillCategory category, SkillStatType stat)
    {
        if (skillDescription.TryGetValue((category,stat), out string description))
        {
            text.text = description;
        }
        else
        {
            text.text = "No description available";
        }

        panel.SetActive(true);
        //text.gameObject.SetActive(true);

    }

    public void HidePanel()
    {
        panel.SetActive(false);
        //text.gameObject.SetActive(false);
    }
}
