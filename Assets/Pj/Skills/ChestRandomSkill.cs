using UnityEngine;
public class ChestRandomSkill : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ManagerSkills.instance.UnlockSkillCategory(SkillCategory.dopplegangerCategory);
        }
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger));
    }
}
