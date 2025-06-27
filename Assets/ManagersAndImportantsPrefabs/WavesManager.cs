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
        int zombiesA = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int zombiesB = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int obstacleA = RandomWaveValue<Obstacles>(_obstaclesList);
        int obstaclesB = RandomWaveValue<Obstacles>(_obstaclesList);
        ConfigWave(zombiesA,zombiesB,0,0, obstacleA, obstaclesB);
        numberOfWave = 0;
    }
    private void Wave2()
    {
        int zombiesA = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int zombiesB = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int obstacleA = RandomWaveValue<Obstacles>(_obstaclesList);
        int obstaclesB = RandomWaveValue<Obstacles>(_obstaclesList);
        ConfigWave(zombiesA,zombiesB,0,2,obstacleA,obstaclesB);
        numberOfWave = 1;
    }
    private void Wave3()
    {
        int zombiesA = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int zombiesB = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int obstacleA = RandomWaveValue<Obstacles>(_obstaclesList);
        int obstaclesB = RandomWaveValue<Obstacles>(_obstaclesList);
        ConfigWave(zombiesA,zombiesB,0,2,obstacleA,obstaclesB);
        numberOfWave = 2;
    }
    private void Wave4()
    {
        int zombiesA = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int zombiesB = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int obstacleA = RandomWaveValue<Obstacles>(_obstaclesList);
        int obstaclesB = RandomWaveValue<Obstacles>(_obstaclesList);
        ConfigWave(zombiesA,zombiesB,0,2,obstacleA,obstaclesB);
        numberOfWave = 3;
    }
    private void Wave5()
    {
        int zombiesA = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int zombiesB = RandomWaveValue<RespawnZombie>(_zombieListRespawns);
        int obstacleA = RandomWaveValue<Obstacles>(_obstaclesList);
        int obstaclesB = RandomWaveValue<Obstacles>(_obstaclesList);
        ConfigWave(zombiesA,zombiesB,0,2, obstacleA,obstaclesB);
        numberOfWave = 4;
    }
    private void Finish()
    {
        numberOfWave = 5;
        Debug.Log("Ganaste");
    }
    public int RandomWaveValue<T>(List<T> list)
    {
        if(list.Count > 0 && list != null)
        {
            int randomValue = UnityEngine.Random.Range(0, list.Count);
            return randomValue;
        }
        else
        {
            return 0;
        }
    }
    private void ConfigWave(int RangeAZombies,int RangeBZombies, int RangeATurret, int RangeBTurret,int RangeAObstacles, int RangeBObstacles )
    {
        if (_zombieListRespawns != null && _zombieListRespawns.Count > 0)
        {
            _tempZombieListRespawns = _zombieListRespawns.Skip(RangeAZombies).Take(RangeBZombies).ToList();
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
            _tempobstaclesList = _obstaclesList.Skip(RangeAObstacles).Take(RangeBObstacles).ToList();
            if (_tempobstaclesList != null && _tempobstaclesList.Count > 0)
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
    private void OnDisable()
    {
        _currentWave = null;
        _cleanObstaclesList = null;
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
