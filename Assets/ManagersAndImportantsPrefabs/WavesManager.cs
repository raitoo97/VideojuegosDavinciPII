using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class WavesManager : MonoBehaviour
{
    private List<IEnemies> enemies;
    [SerializeField]private List<RespawnZombie> _zombieListRespawns;
    [SerializeField]private List<RespawnZombie> _tempZombieListRespawns;
    [SerializeField]private List <Obstacles> _obstaclesList;
    [SerializeField]private List<Obstacles> _tempobstaclesList;
    [SerializeField]private List<TurretBehaviour> _turrets;
    public static WavesManager instance;
    public Action _currentWave;
    public Action _cleanObstaclesList;
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
        _tempZombieListRespawns = new List<RespawnZombie>();
        _tempobstaclesList = new List<Obstacles>();
    }
    private void OnEnable()
    {
        _cleanObstaclesList = CleanListObstacles;
    }
    private void Start()
    {
        _zombieListRespawns = new List<RespawnZombie>(GameObject.FindObjectsOfType<RespawnZombie>());
        _obstaclesList = new List<Obstacles>(GameObject.FindObjectsOfType<Obstacles>());
        _turrets = new List<TurretBehaviour>(GameObject.FindObjectsOfType<TurretBehaviour>());
        _zombieListRespawns = _zombieListRespawns.OrderBy(x => x.name).ToList();
        _obstaclesList = _obstaclesList.OrderBy(x => x.name).ToList();
        _turrets = _turrets.OrderBy(x => x.name).ToList();
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
        ConfigWave(0,2,0,1,0,2);
        numberOfWave = 0;
    }
    private void Wave2()
    {
        ConfigWave(0,2,0,1,0,2);
        numberOfWave = 1;
    }
    private void Wave3()
    {
        //ConfigWave(0, 2, 0, 1);
        numberOfWave = 2;
    }
    private void Wave4()
    {
        //ConfigWave(0, 2, 0, 1);
        numberOfWave = 2;
    }
    private void Wave5()
    {
        //ConfigWave(0, 2, 0, 1);
        numberOfWave = 2;
    }
    private void Finish()
    {
        numberOfWave = 6;
        Debug.Log("Ganaste");
    }
    private void ConfigWave(int RangeAZombies,int RangeBZombies, int RangeATurret, int RangeBTurret,int RangeAObstacles, int RangeBObstacles )
    {
        if (_zombieListRespawns != null && _zombieListRespawns.Count > 0)
        {
            _tempZombieListRespawns = _zombieListRespawns.GetRange(RangeAZombies, RangeBZombies);
            if (_tempZombieListRespawns != null && _tempZombieListRespawns.Count > 0)
            {
                foreach (var waveZombie in _tempZombieListRespawns)
                {
                    waveZombie.StartWave();
                    currentEnemies += waveZombie.returnNumberOfEnemies();
                }
            }
        }
        if(_turrets != null && _turrets.Count > 0)
        {
            var turretList = _turrets.GetRange(RangeATurret, RangeBTurret);
            _turrets.RemoveRange(RangeATurret, RangeBTurret);
            if (turretList != null && turretList.Count > 0)
            {
                foreach (var waveTurret in turretList)
                {
                    waveTurret.gameObject.SetActive(true);
                    currentEnemies += waveTurret.ReturnThisTorret();
                }
            }
        }
        if(_obstaclesList != null && _obstaclesList.Count > 0)
        {
            _tempobstaclesList = _obstaclesList.GetRange(RangeAObstacles, RangeBObstacles);
            if(_tempobstaclesList != null && _tempobstaclesList.Count > 0)
            {
                foreach (var waveObstacles in _tempobstaclesList)
                {
                    waveObstacles.gameObject.SetActive(true);
                }
            }
        }
    }
    private void CleanListObstacles()
    {
        if (currentEnemies <= 0 && _tempobstaclesList.Count > 0)
        {
            foreach (var waveObstacles in _tempobstaclesList)
            {
                waveObstacles.gameObject.SetActive(false);
            }
            _tempobstaclesList.Clear();
        }
        if (currentEnemies <= 0 && _tempZombieListRespawns.Count > 0)
        {
            foreach (var waveZombies in _tempZombieListRespawns)
            {
                waveZombies.gameObject.SetActive(false);
            }
            _tempZombieListRespawns.Clear();
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
