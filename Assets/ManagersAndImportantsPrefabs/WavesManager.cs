using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class WavesManager : MonoBehaviour
{
    private int index;
    private int numberOfWave = 0;
    private int currentEnemies = 0;
    private bool _isInitialized = false;

    private List<IEnemies> enemies;
    private List<RespawnZombie> _zombieListRespawns;
    private List<RespawnZombie> _tempZombieListRespawns;
    private List<TurretBehaviour> _turrets;

    public Action _currentWave;
    public Action _cleanZombieTempList;
    public static WavesManager instance;
    public bool waveStarted = false;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this.gameObject); }

        enemies = new List<IEnemies>();
        _tempZombieListRespawns = new List<RespawnZombie>();
        index = 0;
    }
    private void Start()
    {
        _zombieListRespawns = new List<RespawnZombie>(GameObject.FindObjectsOfType<RespawnZombie>());
        _turrets = new List<TurretBehaviour>(GameObject.FindObjectsOfType<TurretBehaviour>());
        _zombieListRespawns = _zombieListRespawns.OrderBy(x => UnityEngine.Random.value).ToList();
        _turrets = _turrets.OrderBy(x => UnityEngine.Random.value).ToList();
        _isInitialized = true;

        SetWave(index);
        StartCoroutine(GetWaveUIButton());
    }

    private void OnEnable()
    {
        _cleanZombieTempList = CleanZombieTemp;
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
            case 3:
                _currentWave = Wave4;
                break;
            case 4:
                _currentWave = Wave5;
                break;
            case 5:
                _currentWave = Finish;
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
        int zombies = RandomWaveValue<RespawnZombie>(_zombieListRespawns,1,2);
        ConfigWave(zombies,0,0);
        numberOfWave = 0;
    }
    private void Wave2()
    {
        int zombies = RandomWaveValue<RespawnZombie>(_zombieListRespawns,2,3);
        ConfigWave(zombies,0,2);
        numberOfWave = 1;
    }
    private void Wave3()
    {
        int zombies = RandomWaveValue<RespawnZombie>(_zombieListRespawns,3,5);
        ConfigWave(zombies,0,2);
        numberOfWave = 2;
    }
    private void Wave4()
    {
        int zombies = RandomWaveValue<RespawnZombie>(_zombieListRespawns,4,6);
        ConfigWave(zombies,0,3);
        numberOfWave = 3;
    }
    private void Wave5()
    {
        //UltimaWaveActivoTodo
        ConfigWave(9,0,5);
        numberOfWave = 4;
        var RefWaveUI = ManagerUI.instance.WaveUI;
        RefWaveUI._isLastWave = true;
    }
    private void Finish()
    {
        numberOfWave = 5;
    }
    public int RandomWaveValue<T>(List<T>list,int minValue,int maxValue)
    {
        if (list == null || list.Count == 0) return 0;
        int MaxValue = Math.Clamp(maxValue, minValue, list.Count);
        return UnityEngine.Random.Range(minValue, MaxValue + 1);
    }
    private void ConfigWave(int RangeZombies, int RangeATurret, int RangeBTurret) //Arranca Wave
    {
        if (_zombieListRespawns != null && _zombieListRespawns.Count > 0)
        {
            _tempZombieListRespawns = _zombieListRespawns.Take(RangeZombies).ToList();
            if (_tempZombieListRespawns != null && _tempZombieListRespawns.Count > 0)
            {
                foreach (var waveZombie in _tempZombieListRespawns)
                {
                    waveZombie.gameObject.SetActive(true);
                    waveZombie.StartWave();
                    currentEnemies += waveZombie.returnNumberOfEnemies();
                    waveStarted = true;
                }
            }
        }
        if(_turrets != null && _turrets.Count > 0 && RangeATurret >= 0 && RangeATurret < _turrets.Count)
        {
            int amountToTake = Mathf.Min(RangeBTurret, _turrets.Count - RangeATurret);
            var turretList = _turrets.GetRange(RangeATurret, amountToTake);
            _turrets.RemoveRange(RangeATurret, amountToTake);
            if (turretList.Count > 0)
            {
                foreach (var waveTurret in turretList)
                {
                    waveTurret.gameObject.SetActive(true);
                    currentEnemies += waveTurret.ReturnThisTorret();
                }
            }
        }
    }
    private void CleanZombieTemp() //Termino Wave
    {
        if (currentEnemies <= 0 && _tempZombieListRespawns.Count > 0)
        {
            foreach (var waveZombies in _tempZombieListRespawns)
            {
                waveZombies.gameObject.SetActive(false);
                if (!_zombieListRespawns.Contains(waveZombies))
                {
                    _zombieListRespawns.Add(waveZombies);
                }
            }
            _tempZombieListRespawns.Clear();
            waveStarted = false;
        }
    }
    private void OnDisable()
    {
        _currentWave = null;
        _cleanZombieTempList = null;
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
    public IEnumerator GetWaveUIButton()
    {
        yield return new WaitForSeconds(2);
        var RefWaveUI = ManagerUI.instance.WaveUI;
        RefWaveUI._waveButton.interactable = true;
    }
    public int GetCurrentEnemies { get => currentEnemies; }
    public int GetNumberWave { get => numberOfWave; }
    public bool GetInitialized { get => _isInitialized; }
}
