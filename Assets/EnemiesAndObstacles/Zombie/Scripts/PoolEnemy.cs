using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        StartCoroutine(WaitForCompletePool());
    }
    IEnumerator WaitForCompletePool()
    {
        yield return new WaitForEndOfFrame();
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
            var _cloneEnemy = GameObject.Instantiate(prefab, parent.position, parent.rotation,parent);
            if (!_cloneEnemy.TryGetComponent<NavMeshAgent>(out var agentTest))
            {
                Debug.LogWarning("El prefab instanciado no tiene NavMeshAgent");
            }
            else
            {
                Debug.Log("NavMeshAgent instanciado correctamente");
            }
            _cloneEnemy.SetActive(false);
            _enemyPool.Add(_cloneEnemy);

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