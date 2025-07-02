using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WavesUI
{
    public Button _waveButton;
    public Text _waveText;
    public Text _numberOfEnemies;
    public Text _winningText;
    private enum WaveAtributes
    {
        WavesButton,
        WaveText,
        NumberOfRemaningEnemies,
        WinningText
    }
    public WavesUI(List<Button> button,List<Text>texts)
    {
        _waveButton = button.Find(x => x.name == WaveAtributes.WavesButton.ToString());
        _waveText = texts.Find(x => x.name == WaveAtributes.WaveText.ToString());
        _numberOfEnemies = texts.Find(x => x.name == WaveAtributes.NumberOfRemaningEnemies.ToString());
        _winningText = texts.Find(x => x.name == WaveAtributes.WinningText.ToString());
    }
    public void OnStart()
    {
        if(_waveButton == null || _waveText == null || _numberOfEnemies == null) return;
        _waveButton.onClick.AddListener(ActivateWave);
        _waveButton.interactable = false;
    }
    public void OnUpdate()
    {
        SetActivateWaveButton();
        _numberOfEnemies.text = WavesManager.instance.GetCurrentEnemies.ToString();
        Debug.Log("Number Of Wave: " + WavesManager.instance.GetNumberWave);
    }
    private void ActivateWave()
    {
        if (!WavesManager.instance.GetInitialized) return;
        _waveButton.interactable = false;
        WavesManager.instance._currentWave?.Invoke();
        WavesManager.instance.AdvanceWave();
        WavesManager.instance.StartCoroutine(WavesManager.instance.GetWaveUIButton());
    }
    private void SetActivateWaveButton()
    {
        if (WavesManager.instance.GetNumberWave < 5 && WavesManager.instance.GetCurrentEnemies <= 0)
        {
            _waveButton.gameObject.SetActive(true);
            ManagerUI.instance.SkillsPanel.SetActive(true);
            WavesManager.instance._cleanZombieTempList?.Invoke();
            _waveText.gameObject.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
            _winningText.gameObject.SetActive(false);
        }
        else
        {
            _waveButton.gameObject.SetActive(false);
            ManagerUI.instance.SkillsPanel.SetActive(false);
            _waveText.gameObject.SetActive(true);
            _numberOfEnemies.gameObject.SetActive(true);
            _winningText.gameObject.SetActive(false);
        }
        if(WavesManager.instance.GetNumberWave >= 5)
        {
            _waveButton.gameObject.SetActive(false);
            _waveText.gameObject.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
            _winningText.gameObject.SetActive(true);
        }
    }
}
