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
    public List<PoolEnemyStruct> _zombies = new List<PoolEnemyStruct>();
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
        foreach (var enemy in _zombies)
        {
            enemy.CompleteList(enemy.initList);
        }
    }
}
[Serializable]
public class PoolEnemyStruct
{
    [SerializeField] private List<GameObject> _zombiesListPool = new List<GameObject>();
    public GameObject prefab;
    public int initList;
    public Transform parent;
    public EnemyType type;
    public void CompleteList(int init)
    {
        for (int i = 0; i < init; i++)
        {
            var _cloneZombie = GameObject.Instantiate(prefab);
            _cloneZombie.SetActive(false);
            _zombiesListPool.Add(_cloneZombie);
            _cloneZombie.transform.parent = parent;
        }
    }
    public GameObject GetZombie()
    {
        for (int i = 0; i < _zombiesListPool.Count; i++)
        {
            if (!_zombiesListPool[i].activeSelf)
            {
                if (_zombiesListPool[i].TryGetComponent<ZombieBehaviour>(out var zombieBehaviour))
                {
                    zombieBehaviour.life = 100;
                }
                _zombiesListPool[i].SetActive(true);
                return _zombiesListPool[i];
            }
        }
        CompleteList(1);
        GameObject _auxZombie = _zombiesListPool[_zombiesListPool.Count - 1];
        if (_auxZombie.TryGetComponent<ZombieBehaviour>(out var zombieBehaviourAux))
        {
            zombieBehaviourAux.life = 100;
        }
        _auxZombie.SetActive(true);
        return _auxZombie;
    }
}