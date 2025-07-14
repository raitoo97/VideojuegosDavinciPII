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
    //private Button UnlockShield;
    private Button UpgradeShieldRatioButton;
    private Button UpgradeShieldColdownButton;
    private Button UpgradeShieldDurationButton;
    private Button UltimateShield;
    private Text RatioShieldText;
    private Text CooldownShieldText;
    private Text DurationShieldText;
    [Header("Dash")]
    //private Button UnlockDash;
    private Button UpgradeDashSpeedButton;
    private Button UpgradeDashSCooldownButton;
    private Text CooldownDashText;
    private Text SpeedDashText;
    private Button UltimateDash;
    [Header("Survivor")]
    //private Image UnlockSurvivor;
    private Button UpgradeLife;
    private Button UpgradeRadioPickup;
    private Button UpgradePickupHealing;
    private Button UltiSurvivor;
    private Text UpgradeLifeText;
    private Text UpgradeRadioText;
    private Text UpgradePickupHealingText;

    [Header("Doppleganger")]
    private Button UpgradeDoppleLife;
    private Button UpgradeDoppleCooldwn;
    private Button UltimateDopple;
    
    private Text UpgradeDoppleLifeText;
    private Text UpgradeDoppleCooldwnText;
        
        
        
        

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
        //UnlockShield,//Desbloquear escudo
        UpgradeShieldRatio,//Subir Ratio del escudo
        UpgradeShieldColdown,//Subir coldown del escudo
        UpgradeShieldDuration,//Subir duracion del escudo
        UltimateShield,
        //UnlockDash,//DesbloqueDash
        UpgradeDashSpeed,//Subir Speed Del Dash
        UpgradeDashCooldown,//Subir Coldown del dash
        CooldownDashText,//_cooldown del dash nivel
        CooldownShieldText,// _cooldown del shield nivel
        SpeedDashText,//speed del dash nivel
        RatioShieldText,// ratio del shield nivel
        DurationShieldText,//duracion del shield nivel
        UltimateTurret,
        UltimateDash,
         //SURVIVOR
        UpgradeLife,
        UpgradeRadioPickup,
        UpgradePickupHealing,
        UpgradeLifeText,
        UpgradeRadioText,
        UpgradePickupHealingText,
        UltiSurvivor,
        //Doppleganger
        UpgradeDoppleLife,
        UpgradeDoppleLifeText,
        UpgradeDoppleCooldwn,
        UpgradeDoppleCooldwnText,
        UltimateDopple
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
        this.UpgradeShieldRatioButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldRatio.ToString());
        this.UpgradeShieldColdownButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldColdown.ToString());
        this.UpgradeShieldDurationButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeShieldDuration.ToString());
        this.UltimateShield = buttons.Find(x => x.gameObject.name == UIElementName.UltimateShield.ToString());
        this.RatioShieldText = textos.Find(x => x.gameObject.name == UIElementName.RatioShieldText.ToString());
        this.CooldownShieldText = textos.Find(x => x.gameObject.name == UIElementName.CooldownShieldText.ToString());
        this.DurationShieldText = textos.Find(x => x.gameObject.name == UIElementName.DurationShieldText.ToString());
        //Dash
        this.UpgradeDashSpeedButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDashSpeed.ToString());
        this.UpgradeDashSCooldownButton = buttons.Find(x => x.gameObject.name == UIElementName.UpgradeDashCooldown.ToString());
        this.CooldownDashText = textos.Find(x => x.gameObject.name == UIElementName.CooldownDashText.ToString());
        this.SpeedDashText = textos.Find(x => x.gameObject.name == UIElementName.SpeedDashText.ToString());
        this.UltimateDash = buttons.Find(x => x.gameObject.name == UIElementName.UltimateDash.ToString());
        //SURVIVOR
        this.UpgradeLife = buttons.Find(x=> x.gameObject.name == UIElementName.UpgradeLife.ToString());
        this.UpgradeRadioPickup = buttons.Find(x=> x.gameObject.name == UIElementName.UpgradeRadioPickup.ToString());
        this.UpgradePickupHealing = buttons.Find(x=> x.gameObject.name == UIElementName.UpgradePickupHealing.ToString());
        this.UltiSurvivor = buttons.Find(x=> x.gameObject.name == UIElementName.UltiSurvivor.ToString());

        this.UpgradeLifeText = textos.Find(x => x.gameObject.name == UIElementName.UpgradeLifeText.ToString());
        this.UpgradeRadioText = textos.Find(x => x.gameObject.name == UIElementName.UpgradeRadioText.ToString());
        this.UpgradePickupHealingText = textos.Find(x => x.gameObject.name == UIElementName.UpgradePickupHealingText.ToString());

        //Doppleganger
        this.UpgradeDoppleLife = buttons.Find(x=> x.gameObject.name == UIElementName.UpgradeDoppleLife.ToString());
        this.UpgradeDoppleLifeText = textos.Find(x => x.gameObject.name == UIElementName.UpgradeDoppleLifeText.ToString());

        this.UpgradeDoppleCooldwn = buttons.Find(x=> x.gameObject.name == UIElementName.UpgradeDoppleCooldwn.ToString());
        this.UpgradeDoppleCooldwnText = textos.Find(x => x.gameObject.name == UIElementName.UpgradeDoppleCooldwnText.ToString());

        this.UltimateDopple = buttons.Find(x=> x.gameObject.name == UIElementName.UltimateDopple.ToString());
    }
    public void OnStart()
    {
        
        //turret
        rateFireButton.onClick.AddListener(UpgradeCadencia);
        distanceButton.onClick.AddListener(UpgradeDistancia);
        UltimateTurret.onClick.AddListener(UltimateTurretFunctionUnlock);

        //shield
        UpgradeShieldRatioButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeShieldColdownButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeShieldDurationButton.onClick.AddListener(CantPurchaseUpgrade);
        UltimateShield.onClick.AddListener(UltimateShieldFunctionUnlock);

        //dash
        UpgradeDashSpeedButton.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeDashSCooldownButton.onClick.AddListener(CantPurchaseUpgrade);
        UltimateDash.onClick.AddListener(UltimateDashFunctionUnlock);

        //survivor
        UpgradeLife.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeRadioPickup.onClick.AddListener(CantPurchaseUpgrade);
        UpgradePickupHealing.onClick.AddListener(CantPurchaseUpgrade);
        UltiSurvivor.onClick.AddListener(UltimateSurvivorFunctionUnlock);

        //dopple
        UpgradeDoppleLife.onClick.AddListener(CantPurchaseUpgrade);
        UpgradeDoppleCooldwn.onClick.AddListener(CantPurchaseUpgrade);
        UltimateDopple.onClick.AddListener(UltimateDoppleFunctionUnlock);
    }
    public void OnUpdate()
    {
        
        pointsText.text = PointManager.instance.CurrentPoints.ToString();
        #region TURRET
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.turretCategory))
        {
            if (ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretShotSpeed) >= 2)
            {
                rateFireButton.interactable = false;
                rateFireButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory, SkillStatType.turretShotSpeed) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory))
            {
                rateFireButton.targetGraphic.color = Color.white;
                rateFireButton.onClick.RemoveAllListeners();
                rateFireButton.onClick.AddListener(UpgradeCadencia);
            }
            else
            {
                rateFireButton.targetGraphic.color = Color.gray;
                rateFireButton.onClick.RemoveAllListeners();
                rateFireButton.onClick.AddListener(NotEnoughPoints);
            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.turretCategory, SkillStatType.turretVisionRange) >= 2)
            {
                distanceButton.interactable = false;
                distanceButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.turretCategory, SkillStatType.turretVisionRange) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory))
            {
                distanceButton.targetGraphic.color = Color.white;
                distanceButton.onClick.RemoveAllListeners();
                distanceButton.onClick.AddListener(UpgradeDistancia);
            }
            else
            {
                distanceButton.targetGraphic.color = Color.gray;
                distanceButton.onClick.RemoveAllListeners();
                distanceButton.onClick.AddListener(NotEnoughPoints);
            }
        }
        else
        {
            distanceButton.onClick.RemoveAllListeners();
            rateFireButton.onClick.RemoveAllListeners();
            distanceButton.onClick.AddListener(NotEnoughPoints);
            rateFireButton.onClick.AddListener(NotEnoughPoints);

            distanceButton.targetGraphic.color = Color.gray;
            rateFireButton.targetGraphic.color = Color.gray;

        }



        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory))
        {
            rateFireButton.interactable = false;
            distanceButton.interactable = false;
            rateFireButton.targetGraphic.color = Color.gray;
            distanceButton.targetGraphic.color = Color.gray;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.turretCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.turretCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.turretCategory))
        {
            UltimateTurret.interactable = true;
            UltimateTurret.targetGraphic.color = Color.white;
        }
        else
        {
            UltimateTurret.interactable = false;
            UltimateTurret.targetGraphic.color = Color.gray;
        }
        
        rateFireText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed).ToString();
        distanceText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange).ToString();


        #endregion

        #region SHIELD
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory))
        {

            Color c = ManagerUI.instance.UnlockedShield.color;
            c.a = 1;
            ManagerUI.instance.UnlockedShield.color = c;
            ManagerUI.instance.ShieldText.gameObject.SetActive(true);
            CooldownFeedback.instance.ShowIfActive(SkillCategory.shieldCategory);

            if (ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldDuration) >= 2)
            {
                UpgradeShieldDurationButton.interactable = false;
                UpgradeShieldDurationButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldDuration) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldDurationButton.onClick.RemoveAllListeners();
                UpgradeShieldDurationButton.onClick.AddListener(UpgradeShieldDuration);
                UpgradeShieldDurationButton.targetGraphic.color = Color.white;
                UpgradeShieldDurationButton.interactable = true;
            }
            else
            {
                UpgradeShieldDurationButton.onClick.RemoveAllListeners();
                UpgradeShieldDurationButton.onClick.AddListener(NotEnoughPoints);
                UpgradeShieldDurationButton.targetGraphic.color = Color.gray;

            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldRadius) >= 2)
            {
                UpgradeShieldRatioButton.interactable = false;
                UpgradeShieldRatioButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldRadius) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldRatioButton.onClick.RemoveAllListeners();
                UpgradeShieldRatioButton.onClick.AddListener(UpgradeShieldRatio);
                UpgradeShieldRatioButton.targetGraphic.color = Color.white;
                UpgradeShieldRatioButton.interactable = true;
            }
            else
            {
                UpgradeShieldRatioButton.onClick.RemoveAllListeners();
                UpgradeShieldRatioButton.onClick.AddListener(NotEnoughPoints);
                UpgradeShieldRatioButton.targetGraphic.color = Color.gray;
            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.shieldCategory, SkillStatType.shieldCooldown) >= 2)
            {
                UpgradeShieldColdownButton.interactable = false;
                UpgradeShieldColdownButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.shieldCategory, SkillStatType.shieldCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
            {
                UpgradeShieldColdownButton.onClick.RemoveAllListeners();
                UpgradeShieldColdownButton.onClick.AddListener(UpgradeShieldColdown);
                UpgradeShieldColdownButton.targetGraphic.color = Color.white;
                UpgradeShieldColdownButton.interactable = true;

            }
            else
            {
                UpgradeShieldColdownButton.onClick.RemoveAllListeners();
                UpgradeShieldColdownButton.onClick.AddListener(NotEnoughPoints);
                UpgradeShieldColdownButton.targetGraphic.color = Color.gray;
            }

        }
        else if (ManagerSkills.instance.CanUnlockSkillCategory(SkillCategory.shieldCategory) && !ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory))
        {
            //UnlockShield.interactable = true;
            //UnlockShield.targetGraphic.color = Color.white;
            //UnlockShield.onClick.RemoveAllListeners();
            //UnlockShield.onClick.AddListener(UnlockShieldFunction);

        } else
        {
            //UnlockShield.onClick.RemoveAllListeners();
            UpgradeShieldRatioButton.onClick.RemoveAllListeners();
            UpgradeShieldColdownButton.onClick.RemoveAllListeners();
            UpgradeShieldDurationButton.onClick.RemoveAllListeners();

            //UnlockShield.onClick.AddListener(NotEnoughPoints);
            UpgradeShieldRatioButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeShieldColdownButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeShieldDurationButton.onClick.AddListener(CantPurchaseUpgrade);

            //UnlockShield.targetGraphic.color = Color.gray;
            UpgradeShieldRatioButton.targetGraphic.color = Color.gray;
            UpgradeShieldColdownButton.targetGraphic.color = Color.gray;
            UpgradeShieldDurationButton.targetGraphic.color = Color.gray;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory))
        {
            UpgradeShieldRatioButton.interactable = false;
            UpgradeShieldColdownButton.interactable = false;
            UpgradeShieldDurationButton.interactable = false;
            rateFireButton.targetGraphic.color = Color.white;
            distanceButton.targetGraphic.color = Color.white;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.shieldCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.shieldCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.shieldCategory))
        {
            UltimateShield.interactable = true;
            UltimateShield.targetGraphic.color = Color.white;
        }
        else
        {
            UltimateShield.interactable = false;
            UltimateShield.targetGraphic.color = Color.gray;
        }

        RatioShieldText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldRadius).ToString();
        CooldownShieldText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldCooldown).ToString();
        DurationShieldText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldDuration).ToString();
        #endregion

        #region DASH
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory))
        {
            Color d = ManagerUI.instance.UnlockedDash.color;
            d.a = 1;
            ManagerUI.instance.UnlockedDash.color = d;
            ManagerUI.instance.DashText.gameObject.SetActive(true);
            CooldownFeedback.instance.ShowIfActive(SkillCategory.dashCategory);

            if (ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashSpeed) >= 2)
            {
                UpgradeDashSpeedButton.onClick.RemoveAllListeners();
                UpgradeDashSpeedButton.interactable = false;
                UpgradeDashSpeedButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory, SkillStatType.dashSpeed) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
            {
                UpgradeDashSpeedButton.onClick.RemoveAllListeners();
                UpgradeDashSpeedButton.interactable = true;
                UpgradeDashSpeedButton.onClick.AddListener(UpgradeDashSpeed);
                UpgradeDashSpeedButton.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeDashSpeedButton.onClick.RemoveAllListeners();
                UpgradeDashSpeedButton.onClick.AddListener(NotEnoughPoints);
                UpgradeDashSpeedButton.targetGraphic.color = Color.gray;
            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.dashCategory, SkillStatType.dashCooldown) >= 2)
            {
                UpgradeDashSCooldownButton.interactable = false;
                UpgradeDashSCooldownButton.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dashCategory, SkillStatType.dashCooldown) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
            {
                UpgradeDashSCooldownButton.onClick.RemoveAllListeners();
                UpgradeDashSCooldownButton.onClick.AddListener(UpgradeDashCooldown);
                UpgradeDashSCooldownButton.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeDashSCooldownButton.onClick.RemoveAllListeners();
                UpgradeDashSCooldownButton.onClick.AddListener(NotEnoughPoints);
                UpgradeDashSCooldownButton.targetGraphic.color = Color.gray;
            }
        }
        else if (ManagerSkills.instance.CanUnlockSkillCategory(SkillCategory.dashCategory) && !ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory))
        {
            //UnlockDash.targetGraphic.color = Color.white;
            //UnlockDash.onClick.RemoveAllListeners();
            //UnlockDash.onClick.AddListener(UnlockDashFunction);
        }
        else
        {
            //UnlockDash.onClick.RemoveAllListeners();
            //UnlockDash.onClick.AddListener(NotEnoughPoints);
            //UnlockDash.targetGraphic.color = Color.gray;

            UpgradeDashSpeedButton.onClick.RemoveAllListeners();
            UpgradeDashSpeedButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeDashSpeedButton.targetGraphic.color = Color.gray;

            UpgradeDashSCooldownButton.onClick.RemoveAllListeners();
            UpgradeDashSCooldownButton.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeDashSCooldownButton.targetGraphic.color = Color.gray;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
        {
            UpgradeDashSCooldownButton.interactable = false;
            UpgradeDashSpeedButton.interactable = false;
            UpgradeDashSpeedButton.targetGraphic.color = Color.gray;
            UpgradeDashSCooldownButton.targetGraphic.color = Color.gray;
        }
        //Ulti
        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.dashCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.dashCategory))
        {
            UltimateDash.interactable = true;
            UltimateDash.targetGraphic.color = Color.white;
        }
        else
        {
            UltimateDash.interactable = false;
            UltimateDash.targetGraphic.color = Color.gray;
        }

        CooldownDashText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory, SkillStatType.dashCooldown).ToString();
        SpeedDashText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed).ToString();
        #endregion

        #region SURVIVOR
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.survivorCategory))
        {
            Color s = ManagerUI.instance.UnlockedSurvivor.color;
            s.a = 1;
            ManagerUI.instance.UnlockedSurvivor.color = s;
            ManagerUI.instance.SurvivorText.gameObject.SetActive(true);

            //UnlockSurvivor.onClick.RemoveAllListeners();
            //UnlockSurvivor.interactable = false;
            if (ManagerSkills.instance.GetLevel(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor) >= 2)
            {
                UpgradeLife.interactable = false;
                UpgradeLife.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.survivorCategory))
            {
                 UpgradeLife.onClick.RemoveAllListeners();
                 UpgradeLife.onClick.AddListener(UpgradeMaxLife);
                 UpgradeLife.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeLife.onClick.RemoveAllListeners();
                UpgradeLife.onClick.AddListener(NotEnoughPoints);
                UpgradeLife.targetGraphic.color = Color.gray;
            }  
            
            if (ManagerSkills.instance.GetLevel(SkillCategory.survivorCategory, SkillStatType.ratioPickUp) >= 2)
            {
                UpgradeRadioPickup.interactable = false;
                UpgradeRadioPickup.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory, SkillStatType.ratioPickUp) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.survivorCategory))
            {
                UpgradeRadioPickup.onClick.RemoveAllListeners();
                UpgradeRadioPickup.onClick.AddListener(UpgradeRadio);
                UpgradeRadioPickup.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeRadioPickup.onClick.RemoveAllListeners();
                UpgradeRadioPickup.onClick.AddListener(NotEnoughPoints);
                UpgradeRadioPickup.targetGraphic.color = Color.gray;
            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.survivorCategory, SkillStatType.healingPickup) >= 2)
            {
                UpgradePickupHealing.interactable = false;
                UpgradePickupHealing.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.survivorCategory, SkillStatType.healingPickup) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.survivorCategory))
            {
                UpgradePickupHealing.onClick.RemoveAllListeners();
                UpgradePickupHealing.onClick.AddListener(UpgradePickupHealingPoints);
                UpgradePickupHealing.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradePickupHealing.onClick.RemoveAllListeners();
                UpgradePickupHealing.onClick.AddListener(NotEnoughPoints);
                UpgradePickupHealing.targetGraphic.color = Color.gray;
            }
        }
        else if (ManagerSkills.instance.CanUnlockSkillCategory(SkillCategory.survivorCategory) && !ManagerSkills.instance.IsUnlocked(SkillCategory.survivorCategory))
        {
            //UnlockSurvivor.targetGraphic.color = Color.white;
            //UnlockSurvivor.onClick.RemoveAllListeners();
            //UnlockSurvivor.onClick.AddListener(UnlockSurvivorFunc);
        }
        else
        {
            //UnlockSurvivor.onClick.RemoveAllListeners();
            UpgradeLife.onClick.RemoveAllListeners();
            UpgradeRadioPickup.onClick.RemoveAllListeners();
            UpgradePickupHealing.onClick.RemoveAllListeners();

            //UnlockSurvivor.onClick.AddListener(NotEnoughPoints);
            UpgradeLife.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeRadioPickup.onClick.AddListener(CantPurchaseUpgrade);
            UpgradePickupHealing.onClick.AddListener(CantPurchaseUpgrade);

            //UnlockSurvivor.targetGraphic.color = Color.gray;
            UpgradeLife.targetGraphic.color = Color.gray;
            UpgradeRadioPickup.targetGraphic.color = Color.gray;
            UpgradePickupHealing.targetGraphic.color = Color.gray;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dashCategory))
        {
            UpgradeLife.interactable = false;
            UpgradeLife.targetGraphic.color = Color.gray;
        }
        //Ulti
        
        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.survivorCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.survivorCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.survivorCategory))
        {
            UltiSurvivor.interactable = true;
            UltiSurvivor.targetGraphic.color = Color.white;
        }
        else
        {
            UltiSurvivor.interactable = false;
            UltiSurvivor.targetGraphic.color = Color.gray;
        }
        
        UpgradeLifeText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor).ToString();
        UpgradeRadioText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory,SkillStatType.ratioPickUp).ToString();
        UpgradePickupHealingText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.survivorCategory,SkillStatType.healingPickup).ToString();
        #endregion

        #region DOPPLE
        if (ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
        {
            Color d = ManagerUI.instance.UnlockedDoppleganger.color;
            d.a = 1;
            ManagerUI.instance.UnlockedDoppleganger.color = d;
            ManagerUI.instance.DopplegangerText.gameObject.SetActive(true);
            CooldownFeedback.instance.ShowIfActive(SkillCategory.dopplegangerCategory);

            if (ManagerSkills.instance.GetLevel(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife) >= 2)
            {
                UpgradeDoppleLife.onClick.RemoveAllListeners();
                UpgradeDoppleLife.interactable = false;
                UpgradeDoppleLife.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dopplegangerCategory))
            {
                UpgradeDoppleLife.onClick.RemoveAllListeners();
                UpgradeDoppleLife.interactable = true;
                UpgradeDoppleLife.onClick.AddListener(UpgradeDoppleLifeSkill);
                UpgradeDoppleLife.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeDoppleLife.onClick.RemoveAllListeners();
                UpgradeDoppleLife.onClick.AddListener(NotEnoughPoints);
                UpgradeDoppleLife.targetGraphic.color = Color.gray;
            }

            if (ManagerSkills.instance.GetLevel(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger) >= 2)
            {
                UpgradeDoppleCooldwn.interactable = false;
                UpgradeDoppleCooldwn.targetGraphic.color = Color.gray;
            }
            else if (ManagerSkills.instance.GetValueSkillCost(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dopplegangerCategory))
            {
                UpgradeDoppleCooldwn.onClick.RemoveAllListeners();
                UpgradeDoppleCooldwn.onClick.AddListener(UpgradeDoppleCooldownSkill);
                UpgradeDoppleCooldwn.targetGraphic.color = Color.white;
            }
            else
            {
                UpgradeDoppleCooldwn.onClick.RemoveAllListeners();
                UpgradeDoppleCooldwn.onClick.AddListener(NotEnoughPoints);
                UpgradeDoppleCooldwn.targetGraphic.color = Color.gray;
            }
        }
        else
        {

            UpgradeDoppleLife.onClick.RemoveAllListeners();
            UpgradeDoppleLife.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeDoppleLife.targetGraphic.color = Color.gray;

            UpgradeDoppleCooldwn.onClick.RemoveAllListeners();
            UpgradeDoppleCooldwn.onClick.AddListener(CantPurchaseUpgrade);
            UpgradeDoppleCooldwn.targetGraphic.color = Color.gray;
        }

        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dopplegangerCategory))
        {
            UpgradeDoppleLife.interactable = false;
            UpgradeDoppleCooldwn.interactable = false;
            UpgradeDoppleLife.targetGraphic.color = Color.gray;
            UpgradeDoppleCooldwn.targetGraphic.color = Color.gray;
        }
        //Ulti
        if (ManagerSkills.instance.AreAllSkillsMaxed(SkillCategory.dopplegangerCategory) && ManagerSkills.instance.GetUltimateUnlockCost(SkillCategory.dopplegangerCategory) <= PointManager.instance.CurrentPoints && !ManagerSkills.instance.IsUnlockUltimate(SkillCategory.dopplegangerCategory))
        {
            UltimateDopple.interactable = true;
            UltimateDopple.targetGraphic.color = Color.white;
        }
        else
        {
            UltimateDopple.interactable = false;
            UltimateDopple.targetGraphic.color = Color.gray;
        }

        UpgradeDoppleLifeText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife).ToString();
        UpgradeDoppleCooldwnText.text = ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger).ToString();
        #endregion
    }
    private void UltimateTurretFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.turretCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);
    }
    private void UltimateDashFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.dashCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);
    }
    private void UltimateShieldFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.shieldCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);
    }
    private void UltimateSurvivorFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.survivorCategory);
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
    
    private void UpgradeMaxLife()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.survivorCategory, SkillStatType.lifeSurvivor);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
        Survivor.instance.UpgradeLife();
    }private void UpgradeRadio()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.survivorCategory, SkillStatType.ratioPickUp);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
        Survivor.instance.UpgradePickupDistance();
    }
    private void UpgradePickupHealingPoints()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.survivorCategory, SkillStatType.healingPickup);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
        Survivor.instance.UpgradePickupHealing();
    }
    private void UpgradeDoppleLifeSkill()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
    }
    private void UpgradeDoppleCooldownSkill()
    {
        ManagerSkills.instance.UpgradeSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger);
        AudioManager.instance.PlaySfxRandomPitch(audioManager.UpgradeSkill);
    }

    private void UltimateDoppleFunctionUnlock()
    {
        ManagerSkills.instance.TryUnlockUltimate(SkillCategory.dopplegangerCategory);
        AudioManager.instance.PlaySfx(audioManager.UnlockSkill);
    }
    private void NotEnoughPoints()
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
    }
}

