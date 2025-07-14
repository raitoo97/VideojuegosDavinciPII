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
            {SkillCategory.turretCategory, "Powerful shot, aplies a grand blast radius affecting multiple enemies at once." }
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
