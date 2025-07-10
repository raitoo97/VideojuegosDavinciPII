using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class ManagerUI : MonoBehaviour
{
    public List<Image> imagesList = new List<Image>();
    public List<Button> buttonList = new List<Button>();
    public List <Text> textList = new List<Text>();
    public List<LifeController> _statusPjImageEntries = new List<LifeController>();
    public static ManagerUI instance;
    private PjStatesLifeBar PjLifeStates;
    private PjSkillsUpgradeUI _pjSkillsUpgradeUI;
    private WavesUI _wavesUI;
    public GameObject SkillsPanel;
    [Header("Obstacles UI")]//
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
    }
    private void Update()
    {
        PjLifeStates.OnUpdate();
        _pjSkillsUpgradeUI.OnUpdate();
        _wavesUI.OnUpdate();
        _obstaclesDetectedUI.OnUpdate();
    }
    public PjStatesLifeBar getLifeBar { get => PjLifeStates; }
    public WavesUI WaveUI { get => _wavesUI; }
    public ObstaclesDetectedUI ObstaclesDetectedUI { get => _obstaclesDetectedUI;}
}

