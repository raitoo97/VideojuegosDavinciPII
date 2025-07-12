using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _respawnZombie;
    void Start()
    {

    }
    void Update()
    {

    }
    public void ZombieWave()
    {
        //if (_respawnZombie == null) return;
        //_respawnZombie.SetActive(true);
        //if (_respawnZombie.TryGetComponent<RespawnZombie>(out var _respawn))
        //{
        //    _respawn.StartWave();
        //}
    }
}
public class InvokeZombie : IBossSkill
{
    public void BossSkill()
    {
        throw new System.NotImplementedException();
    }
}
