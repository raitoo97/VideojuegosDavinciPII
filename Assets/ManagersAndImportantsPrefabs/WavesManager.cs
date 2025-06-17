using System;
using System.Collections.Generic;
using UnityEngine;
public class WavesManager : MonoBehaviour
{
    public List<RespawnZombie> zombieListRespawns;  
    public static WavesManager instance;
    public Action _currentWave;
    private int index;
    private int indexWaveRespawn;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
        index = 0;
        SetWave(index);
        zombieListRespawns = new List<RespawnZombie>();
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
    private void Update()
    {
        print(indexWaveRespawn);
    }
    public void AdvanceWave()
    {
        index++;
        SetWave(index);
    }
    private void Wave1()
    {
        for (int indexWaveRespawn = 0; indexWaveRespawn < 2; indexWaveRespawn++)
        {
            print("sdsadsadasdasdasdasdsadasd444444sa");
        }
    }
    private void Wave2()
    {
        print("sdsadsadasdasdasdasdsadasdsa");
    }
}
