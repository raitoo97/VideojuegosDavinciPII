using System;
using System.Collections.Generic;
using UnityEngine;
public enum ShooterType
{
    Player,
    Enemy,
    SuperPlayer
}
public class PoolBullet : MonoBehaviour
{
    [SerializeField] public List<BulletPoolConfig> bulletConfigs;
    public static PoolBullet instance;
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
        foreach (BulletPoolConfig config in bulletConfigs)
        {
            config.OnStart();
        }
    }
}
[Serializable]
public class BulletPoolConfig
{
    public ShooterType type;
    public GameObject prefab;
    public int initialSize;
    public Transform _parent;
    private List<GameObject> _bullets = new();
    public void OnStart()
    {
        CompleteList(initialSize);
    }
    public void CompleteList(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var _cloneBullet = GameObject.Instantiate(prefab);
            _cloneBullet.SetActive(false);
            _bullets.Add(_cloneBullet);
            _cloneBullet.transform.parent = _parent;
        }
    }
    public GameObject GetBullet()
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].activeSelf)
            {
                _bullets[i].SetActive(true);
                return _bullets[i];
            }
        }
        CompleteList(1);
        GameObject _auxBullet = _bullets[_bullets.Count - 1];
        _auxBullet.SetActive(true);
        return _auxBullet;
    }
}
