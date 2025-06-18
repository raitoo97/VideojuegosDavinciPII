using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WavesUI
{
    public Button _waveButton;
    public Text _waveText;
    public Text _numberOfEnemies;
    private enum WaveAtributes
    {
        WavesButton,
        WaveText,
        NumberOfRemaningEnemies
    }
    public WavesUI(List<Button> button,List<Text>texts)
    {
        _waveButton = button.Find(x => x.name == WaveAtributes.WavesButton.ToString());
        _waveText = texts.Find(x => x.name == WaveAtributes.WaveText.ToString());
        _numberOfEnemies = texts.Find(x => x.name == WaveAtributes.NumberOfRemaningEnemies.ToString());
    }
    public void OnStart()
    {
        if(_waveButton == null || _waveText == null || _numberOfEnemies == null) return;
        _waveButton.onClick.AddListener(ActivateWave);
    }
    public void OnUpdate()
    {
        SetActivateWaveButton();
        _numberOfEnemies.text = WavesManager.instance.GetCurrentEnemies.ToString();
        Debug.Log("Enemy in scene :" + WavesManager.instance.GetCurrentEnemies);
        Debug.Log("Number Of Wave: " + WavesManager.instance.GetNumberWave);
    }
    private void ActivateWave()
    {
        WavesManager.instance._currentWave?.Invoke();
        WavesManager.instance.AdvanceWave();
    }
    private void SetActivateWaveButton()
    {
        if (WavesManager.instance.GetNumberWave < 2 && WavesManager.instance.GetCurrentEnemies <= 0)
        {
            _waveButton.gameObject.SetActive(true);
            _waveText.gameObject.SetActive(false);
            _numberOfEnemies.gameObject.SetActive(false);
        }
        else
        {
            _waveButton.gameObject.SetActive(false);
            _waveText.gameObject.SetActive(true);
            _numberOfEnemies.gameObject.SetActive(true);
        }
    }
}
