using System.Collections.Generic;
using UnityEngine;
public class PoolZombies : MonoBehaviour
{
    [SerializeField]private List<GameObject> _zombies = new List<GameObject>();
    public GameObject prefab;
    public static PoolZombies instance;
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
            var _cloneZombie = Instantiate(prefab);
            _cloneZombie.SetActive(false);
            _zombies.Add(_cloneZombie);
            _cloneZombie.transform.parent = this.transform;
        }
    }
    public GameObject GetZombie()
    {
        for (int i = 0; i < _zombies.Count; i++)
        {
            if (!_zombies[i].activeSelf)
            {
                if (_zombies[i].TryGetComponent<ZombieBehaviour>(out var zombieBehaviour))
                {
                    zombieBehaviour.life = 100;
                }
                _zombies[i].SetActive(true);
                return _zombies[i];
            }
        }
        CompleteList(1);
        GameObject _auxZombie = _zombies[_zombies.Count - 1];
        if (_auxZombie.TryGetComponent<ZombieBehaviour>(out var zombieBehaviourAux))
        {
            zombieBehaviourAux.life = 100;
        }
        _auxZombie.SetActive(true);
        return _auxZombie;
    }
}
