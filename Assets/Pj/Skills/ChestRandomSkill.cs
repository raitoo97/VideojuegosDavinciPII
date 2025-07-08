using System.Collections.Generic;
using UnityEngine;
public class ChestRandomSkill : MonoBehaviour
{
    private List<SkillCategory> _availableCategories;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChoseRandomCategory();
        }
    }
    private void Awake()
    {
        _availableCategories = new List<SkillCategory>();
    }
    private void Start()
    {
        _availableCategories.Add(SkillCategory.dashCategory);
        _availableCategories.Add(SkillCategory.shieldCategory);
        _availableCategories.Add(SkillCategory.dopplegangerCategory);
    }
    private void ChoseRandomCategory()
    {
        if (_availableCategories.Count <= 0)
        {
            return;
        }
        int randomIndex = Random.Range(0, _availableCategories.Count);
        SkillCategory random = _availableCategories[randomIndex];
        ManagerSkills.instance.UnlockSkillCategory(random);
        _availableCategories.Remove(random);
        Debug.Log("Desbloqueaste: " + random);
    }
}
