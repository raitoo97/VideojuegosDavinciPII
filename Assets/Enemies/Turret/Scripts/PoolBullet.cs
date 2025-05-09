using System.Collections.Generic;
using UnityEngine;
public class PoolBullet : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bullet = new List<GameObject>();
    public GameObject prefab;
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
        CompleteList(50);
    }
    private void CompleteList(int init)
    {
        for (int i = 0; i < init; i++)
        {
            var _cloneBullet = Instantiate(prefab);
            _cloneBullet.SetActive(false);
            _bullet.Add(_cloneBullet);
            _cloneBullet.transform.parent = this.transform;
        }
    }
    public GameObject GetBullet()
    {
        for (int i = 0; i < _bullet.Count; i++)
        {
            if (!_bullet[i].activeSelf)
            {
                _bullet[i].SetActive(true);
                return _bullet[i];
            }
        }
        CompleteList(1);
        GameObject _auxBullet = _bullet[_bullet.Count - 1];
        _auxBullet.SetActive(true);
        return _auxBullet;
    }
}
