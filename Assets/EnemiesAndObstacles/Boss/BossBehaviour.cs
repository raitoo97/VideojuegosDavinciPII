using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField]private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    void Start()
    {

    }
    void Update()
    {

    }
    public void ZombieWave()
    {
        if(_respawnZombies.Count <= 0)
        {
            return;
        }
        foreach(var respawn in _respawnZombies)
        {
            respawn.StartWave();
        }
    }
}
public class InvokeZombie : IBossSkill
{
    public void BossSkill()
    {
        throw new System.NotImplementedException();
    }
}
