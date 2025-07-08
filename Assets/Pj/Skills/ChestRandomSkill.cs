using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestRandomSkill : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ManagerSkills.instance.UnlockSkillCategory(SkillCategory.dashCategory);
        }
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory,SkillStatType.dashCooldown));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed));
    }
}
