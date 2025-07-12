using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject RespawnZombie;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var go = Instantiate(RespawnZombie,this.transform.position,this.transform.rotation);
        if (go == null) return;
        if(go.TryGetComponent<RespawnZombie>(out var Respawn))
        {
            Respawn.StartWave();
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
