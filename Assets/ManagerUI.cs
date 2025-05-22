using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum LifeStatus
{
    NormalStatus,
    Injured,
    MoreInjured,
    AlmostDead
}
public class ManagerUI : MonoBehaviour
{
    public Image lifeBar;
    public List<LifeController> _statusPjImageEntries = new List<LifeController>();
    private Dictionary<LifeStatus, LifeController>  _dictionaryLife = new Dictionary<LifeStatus, LifeController>();
    public static ManagerUI instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
    }
    void Start()
    {
        foreach(var entry in _statusPjImageEntries)
        {
            _dictionaryLife[entry.lifeStatusType] = new LifeController(entry.lifeStatusType, entry.statusImage);
        }
    }
    public void UpdateLifeBar()
    {
        //float lifeToAmount = (lifeProv.life) / 100;
        //lifeBar.fillAmount = Math.Clamp(lifeToAmount, 0, 1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
        }
        UpdateLifeBar();
        CheckLife();
    }
    public void CheckLife()
    {
        //LifeStatus currentStatus;
        //if (lifeProv.life > 70)
        //    currentStatus = LifeStatus.NormalStatus;
        //else if (lifeProv.life > 40)
        //    currentStatus = LifeStatus.Injured;
        //else if (lifeProv.life > 15)
        //    currentStatus = LifeStatus.MoreInjured;
        //else
        //    currentStatus = LifeStatus.AlmostDead;
        //UpdateStatusImage(currentStatus);
    }
    private void UpdateStatusImage(LifeStatus status)
    {
        if (!_dictionaryLife.ContainsKey(status)) return;
        foreach (var entry in _dictionaryLife)
        {
            entry.Value.statusImage.gameObject.SetActive(entry.Key == status);
        }
    }
}
[Serializable]
public class LifeController
{
    public LifeStatus lifeStatusType;
    public Image statusImage;
    public LifeController (LifeStatus lifeStatusType, Image statusImage)
    {
        this.lifeStatusType = lifeStatusType;
        this.statusImage = statusImage;
    }
}
