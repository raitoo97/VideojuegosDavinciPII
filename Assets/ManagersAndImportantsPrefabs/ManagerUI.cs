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
    public Button WaveButton;
    private void Awake()
    {
        if (instance == null) { instance = this; }
            else { Destroy(this.gameObject); }
    }
    void Start()
    {
        imagesList = imagesList.OrderBy(x => x.name).ToList();
        _pjSkillsUpgradeUI = new PjSkillsUpgradeUI(textList, buttonList);
        PjLifeStates = new PjStatesLifeBar(GameManager.instance.player.GetComponent<Player>(),imagesList.Find(x => x.gameObject.name == "LifeBar"), _statusPjImageEntries);
        PjLifeStates.OnStart();
        _pjSkillsUpgradeUI.OnStart();
        WaveButton.onClick.AddListener(ActivateWave);
    }
    private void Update()
    {
        PjLifeStates.OnUpdate();
        _pjSkillsUpgradeUI.OnUpdate();
    }
    private void ActivateWave()
    {
        WavesManager.instance._currentWave?.Invoke();
        WavesManager.instance.AdvanceWave();
    }
    public PjStatesLifeBar getLifeBar { get => PjLifeStates; }
}

