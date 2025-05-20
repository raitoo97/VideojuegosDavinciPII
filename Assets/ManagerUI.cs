using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public List<Button> buttons;
    public Image lifeBar;
    void Start()
    {
        buttons[0].onClick.AddListener(UpdateTurretVision);
    }
    public void UpdateTurretVision()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
    }
}
