using UnityEngine;
public class ChestRandomSkill : MonoBehaviour
{
    private SkillCategory _skillCategory;
    private void Start()
    {
        _skillCategory = SkillCategory.None;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChoseRandomCategory();
        }
    }
    private void ChoseRandomCategory()
    {
        int randomNum = Random.Range(0,3);
        switch (randomNum)
        {
            case 0:
                _skillCategory = SkillCategory.dashCategory;
                print("Desbloqueste dash");
                break;
            case 1:
                _skillCategory = SkillCategory.shieldCategory;
                print("Desbloqueste ssss");
                break;
            case 2:
                _skillCategory = SkillCategory.dopplegangerCategory;
                print("Desbloqueste dddd");
                break;
        }
        UnlockCategory(_skillCategory);
    }
    private void UnlockCategory(SkillCategory skillCategory)
    {
        if (ManagerSkills.instance.IsUnlocked(skillCategory))
        {
            ChoseRandomCategory();
            return;
        }
        ManagerSkills.instance.UnlockSkillCategory(skillCategory);
    }
}
