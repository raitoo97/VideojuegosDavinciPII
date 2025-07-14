using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WavesUI
{
    public Button _waveButton;
    public Text _waveText;
    public Text _numberOfEnemies;
    public Text _winningText;
    public Text _arrow;
    private bool _isFirstWave = true;
    public bool _isLastWave = false;
    private enum WaveAtributes
    {
        WavesButton,
        WaveText,
        NumberOfRemaningEnemies,
        WinningText,
        Arrow
    }
    public WavesUI(List<Button> button,List<Text>texts)
    {
        _waveButton = button.Find(x => x.name == WaveAtributes.WavesButton.ToString());
        _waveText = texts.Find(x => x.name == WaveAtributes.WaveText.ToString());
        _numberOfEnemies = texts.Find(x => x.name == WaveAtributes.NumberOfRemaningEnemies.ToString());
        _winningText = texts.Find(x => x.name == WaveAtributes.WinningText.ToString());
        _arrow = texts.Find(x => x.name == WaveAtributes.Arrow.ToString());
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
    }
    private void ActivateWave()
    {
        if (!WavesManager.instance.GetInitialized) return;
        _waveButton.interactable = false;
        WavesManager.instance._currentWave?.Invoke();
        WavesManager.instance.AdvanceWave();
        WavesManager.instance.StartCoroutine(WavesManager.instance.GetWaveUIButton());
        AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
        _isFirstWave = false;
        _arrow.gameObject.SetActive(false);
    }
    private void SetActivateWaveButton()
    {
        int NumberWave = WavesManager.instance.GetNumberWave;
        int currentEnemies = WavesManager.instance.GetCurrentEnemies;
        if (_isFirstWave)
        {
            _waveButton.gameObject.SetActive(true);
            _arrow.gameObject.SetActive(true);
            //ManagerUI.instance.SkillsPanel.SetActive(false);
            _waveText.gameObject.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
            _winningText.gameObject.SetActive(false);
            return;
        }
        if (_isLastWave && currentEnemies <= 0)
        {
            _waveButton.gameObject.SetActive(false);
            _waveText.gameObject.SetActive(false);
            //ManagerUI.instance.SkillsPanel.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
            _winningText.gameObject.SetActive(true);
            return;
        }
        if (NumberWave < 5 && currentEnemies <= 0 && !_isLastWave)
        {
            _waveButton.gameObject.SetActive(true);
            //ManagerUI.instance.SkillsPanel.SetActive(true);
            WavesManager.instance._cleanZombieTempList?.Invoke();
            _waveText.gameObject.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
            _winningText.gameObject.SetActive(false);
        }
        else
        {
            _waveButton.gameObject.SetActive(false);
            //ManagerUI.instance.SkillsPanel.SetActive(false);
            _waveText.gameObject.SetActive(true);
            _numberOfEnemies.gameObject.SetActive(true);
            _winningText.gameObject.SetActive(false);
        }
    }
}
