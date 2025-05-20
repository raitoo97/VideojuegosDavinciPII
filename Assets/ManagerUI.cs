using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerUI : MonoBehaviour
{
    public List<Button> buttons;
    public Image lifeBar;
    public TestLife lifeProv;
    public static ManagerUI instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
    }
    void Start()
    {
        buttons[0].onClick.AddListener(UpdateTurretVision);
    }
    public void UpdateTurretVision()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
    }
    public void TakeDamageTest(int amount)
    {
        lifeProv.life -= amount;
        print(lifeProv.life);
    }
    public void UpdateLifeBar()
    {
        float lifeToAmount = (lifeProv.life) / 100;
        lifeBar.fillAmount = Math.Clamp(lifeToAmount, 0, 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamageTest(10);
            UpdateLifeBar();
        }
    }
}
[Serializable]
public class TestLife
{
    public float life;
}
