using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownFeedback : MonoBehaviour
{
    [SerializeField] Image _shield; 
    [SerializeField] Image _dash; 
    [SerializeField] Image _dopple; 
    public static CooldownFeedback instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        _shield.gameObject.SetActive(false);
        _dash.gameObject.SetActive(false);
        _dopple.gameObject.SetActive(false);
    }

    public void ShowIfActive(SkillCategory category)
    {
        switch (category)
        {
            case SkillCategory.turretCategory:
                break;

            case SkillCategory.dashCategory:
                _dash.gameObject.SetActive(true);
                break;

            case SkillCategory.shieldCategory:
                    _shield.gameObject.SetActive(true);
                break;

            case SkillCategory.dopplegangerCategory:
                _dopple.gameObject.SetActive(true);
                break;

            case SkillCategory.survivorCategory:
                break;
            default:
                break;
        }
    }


    public void Cooldown (SkillCategory category, SkillStatType statType)
    {
        float cooldownTimer = ManagerSkills.instance.GetValueSkill(category,statType);

        switch (category)
        {
            case SkillCategory.turretCategory:
                break;
            case SkillCategory.dashCategory:
                StartCoroutine(CooldownRoutine(_dash, cooldownTimer));
                break;
            case SkillCategory.shieldCategory:
                StartCoroutine(CooldownRoutine(_shield, cooldownTimer));
                break;
            case SkillCategory.dopplegangerCategory:
                StartCoroutine(CooldownRoutine(_dopple, cooldownTimer));
                break;
            case SkillCategory.survivorCategory:
                break;
            default:
                break;
        }
    }

    private IEnumerator CooldownRoutine(Image img, float cooldown)
    {
        img.fillAmount = 0;
        float timer = 0;


        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            img.fillAmount = timer / cooldown;
            yield return null;
        }
        img.fillAmount = 1;
    }
}
