using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
public class PjSkillsUpgradeUI
{
    private Text pointsText;
    private Text rateFireText;
    private Text distanceText;
    private Text shieldText;
    private Text dashText;
    private Button rateFireButton;
    private Button distanceButton;
    private Button shieldButton;
    private Button dashButton;
    private enum UIElementName
    {
        Number,
        CadenciaText,
        DistanceText,
        UpgradeCadencia,
        UpgradeDistance,
        ButtonShield,
        ShieldNumber,
        ButtonDash,
        DashNumber
    }
    public PjSkillsUpgradeUI(List<Text> textos,List<Button>buttons)
    {
        this.pointsText = textos.Find(x => x.gameObject.name == UIElementName.Number.ToString());
        this.rateFireText = textos.Find(x => x.gameObject.name == UIElementName.CadenciaText.ToString());
        this.distanceText = textos.Find(x => x.gameObject.name == UIElementName.DistanceText.ToString());
        this.rateFireButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeCadencia.ToString());
        this.distanceButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDistance.ToString());
        this.shieldButton = buttons.Find(x => x.gameObject.name == UIElementName.ButtonShield.ToString());
        this.shieldText = textos.Find(x => x.gameObject.name == UIElementName.ShieldNumber.ToString());
        this.dashButton = buttons.Find(x => x.gameObject.name == UIElementName.ButtonDash.ToString());
        this.dashText = textos.Find(x => x.gameObject.name == UIElementName.DashNumber.ToString());
    }
    public void OnStart()
    {
        if (this.pointsText == null || this.rateFireText == null || this.distanceText == null || this.rateFireButton == null || this.distanceButton == null || this.shieldButton == null || this.dashButton == null) return;
        rateFireButton.onClick.AddListener(UpgradeCadencia);
        distanceButton.onClick.AddListener(UpgradeDistancia);
        shieldButton.onClick.AddListener(UpgradeShield);
        dashButton.onClick.AddListener(UpgradeDash);
    }
    public void OnUpdate()
    {
        pointsText.text = PointManager.instance.CurrentPoints.ToString();
        rateFireText.text = ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretShotSpeed).ToString();
        distanceText.text = ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretVisionRange).ToString();
        shieldText.text = ((ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldCooldown) + 1) + " / 3");
        dashText.text = ((ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashCooldown) + 1) + " / 3");
        CanLevelUp();
    }
    private void CanLevelUp () 
    {

        if (shieldButton == null || dashButton == null || ManagerSkills.instance == null || PointManager.instance == null)
        {
            Debug.Print("CanLevelUp error: uno de los objetos es null");
            return;
        }
        if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory,SkillStatType.shieldCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory)) //Solo lo hago con el shield y esa abilidad porque todos comparten mismo valor
        {
            shieldButton.gameObject.SetActive(true); 
        }
        else
        {
            shieldButton.gameObject.SetActive(false); ;
        }
        if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory, SkillStatType.dashCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
        {
            dashButton.gameObject.SetActive(true);
        }
        else
        {
            dashButton.gameObject.SetActive(false);
        }
    }
    private void UpgradeCadencia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed);
    }
    private void UpgradeDistancia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
    }
    private void UpgradeShield() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldRadius);
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldCooldown);
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldDuration);
    }
    private void UpgradeDash() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed);
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashCooldown);
    }
}

