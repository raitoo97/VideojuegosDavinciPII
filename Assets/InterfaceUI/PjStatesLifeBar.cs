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
public class PjStatesLifeBar
{
    private Image lifeBar;
    public List<LifeController> _statusPjImageEntries = new List<LifeController>();
    private Player _player;
    private Dictionary<LifeStatus, LifeController> _dictionaryLife = new Dictionary<LifeStatus, LifeController>();
    public PjStatesLifeBar(Player _player, Image lifeBar, List<LifeController> _statusPjImageEntries)
    {
        this._player = _player;
        this.lifeBar = lifeBar;
        this._statusPjImageEntries = _statusPjImageEntries;
    }
    public void OnStart()
    {
        foreach (var entry in _statusPjImageEntries)
        {
            _dictionaryLife[entry.lifeStatusType] = new LifeController(entry.lifeStatusType, entry.statusImage);
        }
    }
    public void OnUpdate()
    {
        CheckLife();
    }
    public void CheckLife()
    {

        if (!_player.gameObject.activeSelf) return;
        LifeStatus currentStatus;
        if (_player.GetLife > 70)
            currentStatus = LifeStatus.NormalStatus;
        else if (_player.GetLife > 40)
            currentStatus = LifeStatus.Injured;
        else if (_player.GetLife > 15)
            currentStatus = LifeStatus.MoreInjured;
        else
            currentStatus = LifeStatus.AlmostDead;
        UpdateLifeBar();
        UpdateStatusImage(currentStatus);
    }
    public void UpdateLifeBar()
    {
        float lifeToAmount = (_player.GetLife) / 100f;
        lifeBar.fillAmount = Mathf.Clamp(lifeToAmount, 0f, 1f);
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
    public LifeController(LifeStatus lifeStatusType, Image statusImage)
    {
        this.lifeStatusType = lifeStatusType;
        this.statusImage = statusImage;
    }
}
