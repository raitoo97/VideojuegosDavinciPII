using System;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyType
{
    Zombie,
}
public class PoolEnemy : MonoBehaviour
{
    public static PoolEnemy instance;
    public List<PoolEnemyStruct> EnemiesTypesList = new List<PoolEnemyStruct>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        foreach (var enemy in EnemiesTypesList)
        {
            enemy.OnStart();
        }
    }
}
[Serializable]
public class PoolEnemyStruct
{
    public EnemyType type;
    [SerializeField] private List<GameObject> _enemyPool = new List<GameObject>();
    public GameObject prefab;
    public int initList;
    public Transform parent;
    public void OnStart()
    {
        CompleteList(initList);
    }
    public void CompleteList(int init)
    {
        for (int i = 0; i < init; i++)
        {
            var _cloneEnemy = GameObject.Instantiate(prefab);
            _cloneEnemy.SetActive(false);
            _enemyPool.Add(_cloneEnemy);
            _cloneEnemy.transform.parent = parent;
        }
    }
    public GameObject GetEnemy()
    {
        for (int i = 0; i < _enemyPool.Count; i++)
        {
            if (!_enemyPool[i].activeSelf)
            {
                if(type == EnemyType.Zombie)
                {
                    if (_enemyPool[i].TryGetComponent<ZombieBehaviour>(out var zombieBehaviour))
                    {
                        zombieBehaviour.life = 100;
                    }
                }
                _enemyPool[i].SetActive(true);
                return _enemyPool[i];
            }
        }
        CompleteList(1);
        GameObject _auxEnemy = _enemyPool[_enemyPool.Count - 1];
        if (type == EnemyType.Zombie)
        {
            if (_auxEnemy.TryGetComponent<ZombieBehaviour>(out var zombieBehaviour))
            {
                zombieBehaviour.life = 100;
            }
        }
        _auxEnemy.SetActive(true);
        return _auxEnemy;
    }
}