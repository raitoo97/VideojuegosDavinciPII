using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PjSkillsUpgradeUI
{
    private Text pointsText;
    //private Text cantUpgradeText;
    [Header("Torreta")]
    private Text rateFireText;
    private Text distanceText;
    private Button rateFireButton;
    private Button distanceButton;
    private Button UltimateTurret;
    [Header("Shield")]
    private Button UnlockShield;
    private Button CantUnlockShield;
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

    public static bool alreadyClickedUnlock = false;
    public static bool alreadyClickedUpgrade = false;
    AudioManager audioManager = AudioManager.instance;
    private enum UIElementName
    {
        Number,//puntos
        CadenciaText,//Cadencia de torreta nivel
        DistanceText,// distancia de la torreta
        UpgradeCadencia,//Subir cadencia de torretya
        UpgradeDistance,//Subir distancia de torreta
        UnlockShield,//Desbloquear escudo
       // CantUnlockShield, //NO PUEDE DESBLOQUEAR
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
        UltimateTurret,
        //CantUpgradeSkill
    }
    public PjSkillsUpgradeUI(List<Text> textos,List<Button>buttons)
    {
        this.pointsText = textos.Find(x => x.gameObject.name == UIElementName.Number.ToString());
        //this.cantUpgradeText = textos.Find(x => x.gameObject.name == UIElementName.CantUpgradeSkill.ToString());
        //Torreta
        this.rateFireText = textos.Find(x => x.gameObject.name == UIElementName.CadenciaText.ToString());
        this.distanceText = textos.Find(x => x.gameObject.name == UIElementName.DistanceText.ToString());
        this.rateFireButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeCadencia.ToString());
        this.distanceButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDistance.ToString());
        this.UltimateTurret = buttons.Find(x => x.gameObject.name == UIElementName.UltimateTurret.ToString());
        //Shield
        //this.CantUnlockShield = buttons.Find(x => x.gameObject.name == UIElementName.CantUnlockShield.ToString());
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
        
        UnlockShield.onClick.AddListener(CantUnlockSkill);
        UpgradeShieldRatioButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeShieldColdownButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeShieldDurationButton.onClick.AddListener(CantPurchaseUpgrade);

        //dash
        UnlockDash.onClick.AddListener(CantUnlockSkill);
        UpgradeDashSpeedButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeDashSCooldownButton.onClick.AddListener(CantPurchaseUpgrade);
    }
    public void OnUpdate()
    {
        pointsText.text = PointManager.instance.CurrentPoints.ToString();
        #region TURRET

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


        #endregion

        #region SHIELD
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory))
        {
            UnlockShield.onClick.RemoveAllListeners();
            UpgradeShieldRatioButton.onClick.RemoveAllListeners();
            UpgradeShieldColdownButton.onClick.RemoveAllListeners();
            UpgradeShieldDurationButton.onClick.RemoveAllListeners();

            UpgradeShieldRatioButton.onClick.AddListener(UpgradeShieldRatio);
            UpgradeShieldColdownButton.onClick.AddListener(UpgradeShieldColdown);
            UpgradeShieldDurationButton.onClick.AddListener(UpgradeShieldDuration);

            UnlockShield.targetGraphic.color = Color.white;
            UpgradeShieldRatioButton.targetGraphic.color = Color.white;
            UpgradeShieldColdownButton.targetGraphic.color = Color.white;
            UpgradeShieldDurationButton.targetGraphic.color = Color.white;
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
            UnlockShield.targetGraphic.color = Color.white;
            UnlockShield.onClick.RemoveAllListeners();
            UnlockShield.onClick.AddListener(UnlockShieldFunction);

        } else
        {
            UnlockShield.onClick.RemoveAllListeners();
            UpgradeShieldRatioButton.onClick.RemoveAllListeners();
            UpgradeShieldColdownButton.onClick.RemoveAllListeners();
            UpgradeShieldDurationButton.onClick.RemoveAllListeners();

            UnlockShield.onClick.AddListener(CantUnlockSkill);
            UpgradeShieldRatioButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeShieldColdownButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeShieldDurationButton.onClick.AddListener(CantPurchaseUpgrade);

            UnlockShield.targetGraphic.color = Color.red;
            UpgradeShieldRatioButton.targetGraphic.color = Color.red;
            UpgradeShieldColdownButton.targetGraphic.color = Color.red;
            UpgradeShieldDurationButton.targetGraphic.color = Color.red;
        }

        RatioShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldRadius).ToString();
        CooldownShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldCooldown).ToString();
        DurationShieldText.text = ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldDuration).ToString();
        #endregion

        #region DASH
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory))
        {
            UnlockDash.onClick.RemoveAllListeners();
            UpgradeDashSpeedButton.onClick.RemoveAllListeners();
            UpgradeDashSCooldownButton.onClick.RemoveAllListeners();

            UpgradeDashSpeedButton.onClick.AddListener(UpgradeDashSpeed);
            UpgradeDashSCooldownButton.onClick.AddListener(UpgradeDashCooldown);

            UpgradeDashSpeedButton.targetGraphic.color = Color.white;
            UpgradeDashSCooldownButton.targetGraphic.color = Color.white;

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
            UnlockDash.targetGraphic.color = Color.white;
            UnlockDash.onClick.RemoveAllListeners();
            UnlockDash.onClick.AddListener(UnlockDashFunction);
        }
        else
        {
            UnlockDash.onClick.RemoveAllListeners();
            UpgradeDashSpeedButton.onClick.RemoveAllListeners();
            UpgradeDashSCooldownButton.onClick.RemoveAllListeners();

            UnlockDash.onClick.AddListener(CantUnlockSkill);
            UpgradeDashSpeedButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeDashSCooldownButton.onClick.AddListener(CantPurchaseUpgrade);

            UnlockDash.targetGraphic.color = Color.red;
            UpgradeDashSpeedButton.targetGraphic.color = Color.red;
            UpgradeDashSCooldownButton.targetGraphic.color = Color.red;
        }


        //Ulti
        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
        {
           
            UnlockDash.interactable = false;
            UpgradeDashSpeedButton.interactable = false;
            UpgradeDashSCooldownButton.interactable = false;
        }
        CooldownDashText.text = ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashCooldown).ToString();
        SpeedDashText.text = ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashSpeed).ToString();
        #endregion
    }
    private void UltimateTurretFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.turretCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);
    }
    private void UpgradeCadencia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
    }
    private void UpgradeDistancia()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }
    private void UnlockShieldFunction()
    {
        ManagerSkills.instance.UnlockSkillCategory(SkillCategory.shieldCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);

    }
    private void UpgradeShieldRatio() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldRadius);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }
    private void UpgradeShieldColdown()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldCooldown);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }
    private void UpgradeShieldDuration()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.shieldCategory, SkillStatType.shieldDuration);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }
    private void UnlockDashFunction()
    {
        ManagerSkills.instance.UnlockSkillCategory(SkillCategory.dashCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);

    }
    private void UpgradeDashSpeed() 
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }
    private void UpgradeDashCooldown()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dashCategory, SkillStatType.dashCooldown);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);

    }

    private void CantUnlockSkill()
    {
        audioManager.PlaySfx(audioManager.CantUnlockSkill);
        if (!alreadyClickedUnlock)
        {
            alreadyClickedUnlock = true;
            PointManager.instance.StartCoroutine(PointManager.instance.CantUnlockRoutine());
        }
    }

    private void CantPurchaseUpgrade()
    {
        audioManager.PlaySfx(audioManager.CantUnlockSkill);
        if (!alreadyClickedUpgrade)
        {
            alreadyClickedUpgrade = true;
            PointManager.instance.StartCoroutine(PointManager.instance.CantUpgradeRoutine());
        }
        Debug.Log("Deberias desbloquear la habilidad primero");
    }
}

