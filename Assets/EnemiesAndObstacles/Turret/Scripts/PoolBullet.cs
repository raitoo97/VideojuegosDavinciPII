using System.Collections.Generic;
using UnityEngine;
public class PoolBullet : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bullet = new List<GameObject>();
    public GameObject prefab;
    [SerializeField] private Material playerMaterial;
    [SerializeField] private Material enemyMaterial;
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
    public GameObject GetBullet(ShooterType shooter)
    {
        for (int i = 0; i < _bullet.Count; i++)
        {
            if (!_bullet[i].activeSelf)
            {
                _bullet[i].SetActive(true);
                SetupBullet(_bullet[i], shooter);
                return _bullet[i];
            }
        }
        CompleteList(1);
        GameObject _auxBullet = _bullet[_bullet.Count - 1];
        _auxBullet.SetActive(true);
        SetupBullet(_auxBullet, shooter);
        return _auxBullet;
    }
    private void SetupBullet(GameObject bullet, ShooterType shooter)
    {
        var renderer = bullet.GetComponent<Renderer>();
        if (renderer != null)
        {
            if (shooter == ShooterType.Player)
                renderer.material = playerMaterial;
            else if (shooter == ShooterType.Enemy)
                renderer.material = enemyMaterial;
        }
    }
}
