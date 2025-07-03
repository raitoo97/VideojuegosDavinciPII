using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUlti : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    public static Action<ZombieBehaviour> onHitZombie;
    public static event Action<TurretBehaviour, float> OnTurretDamaged;
    private float _dmgPlayer = 50f;
    public void CreateDashTrail(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        
        GameObject trail = Instantiate(prefab , start + direction * (distance / 2f), Quaternion.identity);

        trail.transform.forward = direction;
        trail.transform.localScale = new Vector3(1, 1, distance);

        ParticlesPool.instance.SpamParticle(ParticleType.DashUlti, trail.transform.forward, trail.transform.localScale, trail.transform);
        
        //Destroy(trail,2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ZombieBehaviour>(out var Zombie))
        {
            onHitZombie?.Invoke(Zombie);
        }
        else if (other.TryGetComponent<TurretBehaviour>(out var turret))
        {
            OnTurretDamaged?.Invoke(turret, _dmgPlayer);
        }
    }
}
