using System.Collections.Generic;
using UnityEngine;
public class ChestRandomSkill : MonoBehaviour
{
    private List<SkillCategory> _availableCategories;
    private float _valueChest;
    private void Update()
    {
        if (ManagerUI.instance.unlockSkill)
        {
            ChoseRandomCategory();
            ManagerUI.instance.unlockSkill = false;
        }
    }
    private void Awake()
    {
        _availableCategories = new List<SkillCategory>();
        _valueChest = 3000f;
    }
    private void Start()
    {
        _availableCategories.Add(SkillCategory.dashCategory);
        _availableCategories.Add(SkillCategory.shieldCategory);
        _availableCategories.Add(SkillCategory.dopplegangerCategory);
        _availableCategories.Add(SkillCategory.survivorCategory);
    }
    private void ChoseRandomCategory()
    {
        if (_availableCategories.Count <= 0)
        {
            return;
        }
        if (PointManager.instance.SpendPoints(_valueChest))
        {
            int randomIndex = Random.Range(0, _availableCategories.Count);
            SkillCategory random = _availableCategories[randomIndex];
            ManagerSkills.instance.UnlockSkillCategory(random);
            _availableCategories.Remove(random);
            Debug.Log("Desbloqueaste: " + random);
        }
    }
}
