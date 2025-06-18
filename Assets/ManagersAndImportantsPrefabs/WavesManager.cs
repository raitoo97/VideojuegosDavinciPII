using System;
using System.Collections.Generic;
using UnityEngine;
public class WavesManager : MonoBehaviour
{
    private List<IEnemies> enemies;
    public List<RespawnZombie> zombieListRespawns;
    [SerializeField]private List<TurretBehaviour> turrets;
    public static WavesManager instance;
    public Action _currentWave;
    private int index;
    private int currentEnemies = 0;
    private int numberOfWave = 0;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }
        index = 0;
        SetWave(index);
        enemies = new List<IEnemies>();
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
            case 2:
                _currentWave = Wave3;
                break;
            default:
                _currentWave = null;
                break;
        }
    }
    public void AdvanceWave()
    {
        index++;
        SetWave(index);
    }
    private void Wave1()
    {
        ConfigWave(0,2,0,2);
        numberOfWave = 0;
    }
    private void Wave2()
    {
        ConfigWave(0, 2, 0, 2);
        numberOfWave = 1;
    }
    private void Wave3()
    {
        print("Llegaste a la 3 pa");
        numberOfWave = 2;
    }
    private void ConfigWave(int RangeAZombies,int RangeBZombies, int RangeATurret, int RangeBTurret)
    {
        if (zombieListRespawns == null || zombieListRespawns.Count < 0) return;
        var zombieList = zombieListRespawns.GetRange(RangeAZombies, RangeBZombies);
        zombieListRespawns.RemoveRange(RangeAZombies, RangeBZombies);
        if (zombieList == null || zombieList.Count <= 0) return;
        foreach (var waveZombie in zombieList)
        {
            waveZombie.StartWave();
            currentEnemies += waveZombie.returnNumberOfEnemies();
        }
        var turretList = turrets.GetRange(RangeATurret, RangeBTurret);
        turrets.RemoveRange(RangeATurret, RangeBTurret);
        if (turretList == null || turretList.Count <= 0) return;
        foreach (var waveTurret in turretList)
        {
            waveTurret.gameObject.SetActive(true);
            currentEnemies += waveTurret.ReturnThisTorret();
        }
    }
    public void EnemySuscribeEventToWaveSubstract(IEnemies enemy)
    {
        if (enemies.Contains(enemy)) return;
        enemies.Add(enemy);
        enemy._substractEnemyFromWave += HandleEnemyDeathWaves;
    }
    public void EnemyDesuscribeEventToWaveSubstract(IEnemies enemy)
    {
        if (!enemies.Contains(enemy)) return;
        enemies.Remove(enemy);
        enemy._substractEnemyFromWave -= HandleEnemyDeathWaves;
    }
    private void HandleEnemyDeathWaves(IEnemies enemy)
    {
        int substract = enemy.SubstractFromWave();
        currentEnemies -= substract;
    }
    public int GetCurrentEnemies { get => currentEnemies; }
    public int GetNumberWave { get => numberOfWave; }
}
