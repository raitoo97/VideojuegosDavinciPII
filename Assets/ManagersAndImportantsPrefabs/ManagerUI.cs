using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ManagerUI : MonoBehaviour
{
    public List<Image> imagesList = new List<Image>();
    public Image UnlockedShield;
    public Image ShieldText;
    public Image UnlockedDash;
    public Image DashText;
    public Image UnlockedSurvivor;
    public Image SurvivorText;
    public Image UnlockedDoppleganger;
    public Image DopplegangerText;

    public List<Button> buttonList = new List<Button>();
    public List <Text> textList = new List<Text>();
    public List<LifeController> _statusPjImageEntries = new List<LifeController>();
    public static ManagerUI instance;
    private PjStatesLifeBar PjLifeStates;
    private PjSkillsUpgradeUI _pjSkillsUpgradeUI;
    private WavesUI _wavesUI;
    [SerializeField] CooldownFeedback _cooldownFeedback;

    [Header("Skills UI")]
    public GameObject SkillsPanel;
    private bool _isCanvasEnable;
    private Button _skillsCanvasButton;
    

    [Header("Obstacles UI")]
    public GameObject obstacleWarning;
    public GameObject obstacleWarningArrow;
    private ObstaclesDetectedUI _obstaclesDetectedUI;

    private void Awake()
    {
        if (instance == null) { instance = this; }
            else { Destroy(this.gameObject); }
    }
    void Start()
    {
        imagesList = imagesList.OrderBy(x => x.name).ToList();

        _pjSkillsUpgradeUI = new PjSkillsUpgradeUI(textList, buttonList);
        PjLifeStates = new PjStatesLifeBar(GameManager.instance.player.GetComponent<Player>(),imagesList.Find(x => x.gameObject.name == "LifeBar"), _statusPjImageEntries, imagesList.Find(x => x.gameObject.name == "LifeBarEdge"));
        _wavesUI = new WavesUI(buttonList, textList);
        _obstaclesDetectedUI = new ObstaclesDetectedUI(obstacleWarning, obstacleWarningArrow);
        PjLifeStates.OnStart();
        _pjSkillsUpgradeUI.OnStart();
        _wavesUI.OnStart();
        _obstaclesDetectedUI.OnStart();
        
        SkillsPanel.SetActive(false);
        _isCanvasEnable = false;
        this._skillsCanvasButton = buttonList.Find(x => x.gameObject.name == "ButtonSkill");
        _skillsCanvasButton.onClick.AddListener(ButtonSkillClicked);


        

        //Images of Skills unlock half alpha
        Color c = UnlockedShield.color;
        c.a = 0.2f;
        UnlockedShield.color = c;
        ShieldText.gameObject.SetActive(false);

        Color d = UnlockedDash.color;
        d.a = 0.2f;
        UnlockedDash.color = d;
        DashText.gameObject.SetActive(false);

        Color s = UnlockedSurvivor.color;
        s.a = 0.2f;
        UnlockedSurvivor.color = s;
        SurvivorText.gameObject.SetActive(false);

        Color g = UnlockedDoppleganger.color;
        g.a = 0.2f;
        UnlockedDoppleganger.color = g;
        DopplegangerText.gameObject.SetActive(false);

    }
    private void Update()
    {
        PjLifeStates.OnUpdate();
        _pjSkillsUpgradeUI.OnUpdate();
        _wavesUI.OnUpdate();
        _obstaclesDetectedUI.OnUpdate();

        
    }
    public void CanvasState()
    {
        if (!_isCanvasEnable)
        {
            ActiveSkillsMenu();
        }
        else
        {
            DeactivateSkillsMenu();
        }
    }
    public void ActiveSkillsMenu()
    {
        _isCanvasEnable = true;
        SkillsPanel.SetActive(true);
    }
    public void DeactivateSkillsMenu()
    {
        _isCanvasEnable = false;
        SkillsPanel.SetActive(false);

    }

    private void ButtonSkillClicked()
    {
        CanvasState();
        _skillsCanvasButton.onClick.RemoveAllListeners();
        _skillsCanvasButton.onClick.AddListener(ButtonSkillDeclicked);
    }

    private void ButtonSkillDeclicked()
    {
        CanvasState();
        _skillsCanvasButton.onClick.RemoveAllListeners();
        _skillsCanvasButton.onClick.AddListener(ButtonSkillClicked);
    }

    


    public PjStatesLifeBar getLifeBar { get => PjLifeStates; }
    public WavesUI WaveUI { get => _wavesUI; }
    public ObstaclesDetectedUI ObstaclesDetectedUI { get => _obstaclesDetectedUI;}
}

