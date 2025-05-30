using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum LifeStatus
{
    NormalStatus,
    Injured,
    MoreInjured,
    MoreMoreInjured,
    AlmostDead
}
public class PjStatesLifeBar
{
    private Image image;
    private List<LifeController> _statusPjImageEntries = new List<LifeController>();
    private Player _player;
    private Dictionary<LifeStatus, LifeController> _dictionaryLife = new Dictionary<LifeStatus, LifeController>();
    public PjStatesLifeBar(Player _player, Image image, List<LifeController> _statusPjImageEntries)
    {
        this._player = _player;
        this.image = image;
        this._statusPjImageEntries = _statusPjImageEntries;
    }
    public void OnStart()
    {
        foreach (var entry in _statusPjImageEntries)
        {
            _dictionaryLife[entry.lifeStatusType] = new LifeController(entry.lifeStatusType, entry.statusImage,entry.EdgeStatus);
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
        if (_player.GetLife > 90)
            currentStatus = LifeStatus.NormalStatus;
        else if (_player.GetLife > 80)
            currentStatus = LifeStatus.Injured;
        else if (_player.GetLife > 60)
            currentStatus = LifeStatus.MoreInjured;
        else if (_player.GetLife > 20)
            currentStatus = LifeStatus.MoreMoreInjured;
        else
            currentStatus = LifeStatus.AlmostDead;
        UpdateLifeBar();
        UpdateStatusImage(currentStatus);
    }
    private void UpdateLifeBar()
    {
        float lifeToAmount = (_player.GetLife) / 100f;
        image.fillAmount = Mathf.Clamp(lifeToAmount, 0f, 1f);
    }
    private void UpdateStatusImage(LifeStatus status)
    {
        if (!_dictionaryLife.ContainsKey(status)) return;
        foreach (var entry in _dictionaryLife)
        {
            entry.Value.statusImage.gameObject.SetActive(entry.Key == status);
            entry.Value.EdgeStatus.gameObject.SetActive(entry.Key == status);
        }
    }
}
[Serializable]
public class LifeController
{
    public LifeStatus lifeStatusType;
    public Image statusImage;
    public Image EdgeStatus;
    public LifeController(LifeStatus lifeStatusType, Image statusImage,Image EdgeStatus)
    {
        this.lifeStatusType = lifeStatusType;
        this.statusImage = statusImage;
        this.EdgeStatus = EdgeStatus;
    }
}
