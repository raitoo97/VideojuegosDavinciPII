using System;
using System.Collections.Generic;
using UnityEngine;
public class WavesManager : MonoBehaviour
{
    public List<RespawnZombie> zombieListRespawn;
    public Action _currentWave;
    public static WavesManager instance;
    private int waveIndex = 0;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
    }
    private void Start()
    {
        SetWave(waveIndex);
    }
    private void SetWave(int index)
    {
        switch (index)
        {
            case 0:
                _currentWave = Wave1;
                break;
            case 1:
                _currentWave = Wave2;
                break;
            default:
                _currentWave = null;
                break;
        }
    }
    public void AdvanceWave()
    {
        waveIndex++;
        SetWave(waveIndex);
    }
    private void Wave1()
    {
        print("Holaaaaaaa");
    }
    private void Wave2()
    {
        print("asdasdasdsadas");
    }
    private void Wave3()
    {
        print("TU viejaaa");
    }
}
