using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManagerUI : MonoBehaviour
{
    public Image lifeBar;
    public List<LifeController> _statusPjImageEntries = new List<LifeController>();
    public static ManagerUI instance;
    [SerializeField]private Player _player;
    private PjStatesLifeBar PjLifeStates;
    private void Awake()
    {
        if (instance == null) { instance = this; }
            else { Destroy(this.gameObject); }
    }
    void Start()
    {
        _player = GameManager.instance.player.GetComponent<Player>();
        PjLifeStates = new PjStatesLifeBar(_player, lifeBar, _statusPjImageEntries);
        PjLifeStates.OnStart();
    }
    private void Update()
    {
        PjLifeStates.OnUpdate();
    }
    public PjStatesLifeBar getLifeBar { get => PjLifeStates; }
}

