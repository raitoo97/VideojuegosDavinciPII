using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PjSkillsUpgradeUI
{
    private Text pointsText;
    [Header("Torreta")]
    private Text rateFireText;
    private Text distanceText;
    private Button rateFireButton;
    private Button distanceButton;
    private Button UltimateTurret;
    [Header("Shield")]
    private Button UnlockShield;
    private Button UpgradeShieldRatioButton;
    private Button UpgradeShieldColdownButton;
    private Button UpgradeShieldDurationButton;
    private Text RatioShieldText;
    private Text CooldownShieldText;
    private Text DurationShieldText;
    [Header("Dash")]
    private Button UnlockDash;
    private Button UpgradeDashSpeedButton;
    private Button UpgradeDashSCooldownButton;
    private Text CooldownDashText;
    private Text SpeedDashText;
    private enum UIElementName
    {
        Number,//puntos
        CadenciaText,//Cadencia de torreta nivel
        DistanceText,// distancia de la torreta
        UpgradeCadencia,//Subir cadencia de torretya
        UpgradeDistance,//Subir distancia de torreta
        UnlockShield,//Desbloquear escudo
        UpgradeShieldRatio,//Subir Ratio del escudo
        UpgradeShieldColdown,//Subir coldown del escudo
        UpgradeShieldDuration,//Subir duracion del escudo
        UnlockDash,//DesbloqueDash
        UpgradeDashSpeed,//Subir Speed Del Dash
        UpgradeDashCooldown,//Subir Coldown del dash
        CooldownDashText,//cooldown del dash nivel
        CooldownShieldText,// cooldown del shield nivel
        SpeedDashText,//speed del dash nivel
        RatioShieldText,// ratio del shield nivel
        DurationShieldText,//duracion del shield nivel
        UltimateTurret
    }
    public PjSkillsUpgradeUI(List<Text> textos,List<Button>buttons)
    {
        this.pointsText = textos.Find(x => x.gameObject.name == UIElementName.Number.ToString());
        //Torreta
        this.rateFireText = textos.Find(x => x.gameObject.name == UIElementName.CadenciaText.ToString());
        this.distanceText = textos.Find(x => x.gameObject.name == UIElementName.DistanceText.ToString());
        this.rateFireButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeCadencia.ToString());
        this.distanceButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDistance.ToString());
        this.UltimateTurret = buttons.Find(x => x.gameObject.name == UIElementName.UltimateTurret.ToString());
        //Shield
        this.UnlockShield = buttons.Find(x => x.gameObject.name == UIElementName.UnlockShield.ToString());
        this.UpgradeShieldRatioButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldRatio.ToString());
        this.UpgradeShieldColdownButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldColdown.ToString());
        this.UpgradeShieldDurationButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldDuration.ToString());
        this.RatioShieldText = textos.Find(x => x.gameObject.name == UIElementName.RatioShieldText.ToString());
        this.CooldownShieldText = textos.Find(x => x.gameObject.name == UIElementName.CooldownShieldText.ToString());
        this.DurationShieldText = textos.Find(x => x.gameObject.name == UIElementName.DurationShieldText.ToString());
        //Dash
        this.UnlockDash = buttons.Find(x => x.gameObject.name == UIElementName.UnlockDash.ToString());
        this.UpgradeDashSpeedButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDashSpeed.ToString());
        this.UpgradeDashSCooldownButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDashCooldown.ToString());
        this.CooldownDashText = textos.Find(x => x.gameObject.name == UIElementName.CooldownDashText.ToString());
        this.SpeedDashText = textos.Find(x => x.gameObject.name == UIElementName.SpeedDashText.ToString());
    }
    public void OnStart()
    {
        //turret
        rateFireButton.onClick.AddListener(UpgradeCadencia);
        distanceButton.onClick.AddListener(UpgradeDistancia);
        UltimateTurret.onClick.AddListener(UltimateTurretFunctionUnlock);
        //shield
        UnlockShield.onClick.AddListener(UnlockShieldFunction);
        UpgradeShieldRatioButton.onClick.AddListener(UpgradeShieldRatio);
        UpgradeShieldColdownButton.onClick.AddListener(UpgradeShieldColdown);
        UpgradeShieldDurationButton.onClick.AddListener(UpgradeShieldDuration);
        //dash
        UnlockDash.onClick.AddListener(UnlockDashFunction);
        UpgradeDashSpeedButton.onClick.AddListener(UpgradeDashSpeed);
        UpgradeDashSCooldownButton.onClick.AddListener(UpgradeDashCooldown);
    }
    public void OnUpdate()
    {
        pointsText.text = PointManager.instance.CurrentPoints.ToString();
        //turret
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.turretCategory))
        {
            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory, SkillStatType.turretShotSpeed) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory))
            {
                rateFireButton.interactable = true;

            }
            else
            {
                rateFireButton.interactable = false;
            }

            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory, SkillStatType.turretVisionRange) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory))
            {
                distanceButton.interactable = true;
            }
            else
            {
                distanceButton.interactable = false;
            }
        }
        

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.turretCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.turretCategory))
        {
            UltimateTurret.interactable = true;
        }
        else
        {
           UltimateTurret.interactable = false;
        }
        rateFireText.text = ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretShotSpeed).ToString();
        distanceText.text = ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretVisionRange).ToString();
        //shield
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory))
        {
            ColorBlock cb = UnlockShield.colors;
            cb.disabledColor = Color.white;
            UnlockShield.colors = cb;
            UnlockShield.interactable = false;

            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldDuration) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldDurationButton.interactable = true;
                
            }
            else
            {
                UpgradeShieldDurationButton.interactable = false;
            }
            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldRadius) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldRatioButton.interactable = true;
            }
            else
            {
                UpgradeShieldRatioButton.interactable = false;
            }
            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldColdownButton.interactable = true;
            }
            else
            {
                UpgradeShieldColdownButton.interactable = false;
            }
        }
        else if (ManagerSkills.instance.CanUnlockSkillCategory(SkillCategory.shieldCategory) && !ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory))
        {
            UnlockShield.interactable = true;
            
          
        } else
        {
            UnlockShield.interactable = false;
            UpgradeShieldDurationButton.interactable = false;
            UpgradeShieldRatioButton.interactable = false;
            UpgradeShieldColdownButton.interactable = false;
            /*ColorBlock cb = UnlockShield.colors;
            Color newNormal = cb.normalColor;
            newNormal.b = 1;
            newNormal.a = 0.2f;
            cb.normalColor = newNormal;
            UnlockShield.colors = cb;*/
        }
      
        RatioShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldRadius).ToString();
        CooldownShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldCooldown).ToString();
        DurationShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldDuration).ToString();

        //Dash
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory))
        {
            ColorBlock cb = UnlockDash.colors;
            cb.disabledColor = Color.white;
            UnlockDash.colors = cb;
            UnlockDash.interactable = false;

            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory, SkillStatType.dashSpeed) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
            {
                UpgradeDashSpeedButton.interactable = true;

            }
            else
            {
                UpgradeDashSpeedButton.interactable = false;
            }

            if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory, SkillStatType.dashCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
            {
                UpgradeDashSCooldownButton.interactable = true;

            }
            else
            {
                UpgradeDashSCooldownButton.interactable = false;
            }
        }
        else if (ManagerSkills.instance.CanUnlockSkillCategory(SkillCategory.dashCategory) && !ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory))
        {
            UnlockDash.interactable = true;
            
        }
        else
        {
            UnlockDash.interactable = false;
            UpgradeDashSpeedButton.interactable = false;
            UpgradeDashSCooldownButton.interactable = false;
        }
        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
        {
            UnlockDash.interactable = false;
            UpgradeDashSpeedButton.interactable = false;
            UpgradeDashSCooldownButton.interactable = false;
        }
        CooldownDashText.text = ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashCooldown).ToString();
        SpeedDashText.text = ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashSpeed).ToString();
    }
    private void UltimateTurretFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.turretCategory);
    }
    private void UpgradeCadencia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed);
    }
    private void UpgradeDistancia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
    }
    private void UnlockShieldFunction()
    {
        Debug.Log("Desbloquear escudo");
        ManagerSkills.instance.UnlockSkillCategory(SkillCategory.shieldCategory);
    }
    private void UpgradeShieldRatio() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldRadius);
    }
    private void UpgradeShieldColdown()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldCooldown);
    }
    private void UpgradeShieldDuration()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldDuration);
    }
    private void UnlockDashFunction()
    {
        ManagerSkills.instance.UnlockSkillCategory(SkillCategory.dashCategory);
    }
    private void UpgradeDashSpeed() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed);
    }
    private void UpgradeDashCooldown()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashCooldown);
    }
}

