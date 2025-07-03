using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCollider : MonoBehaviour
{
    public static event  Action<ZombieBehaviour> onHitZombie;
    public static event Action<TurretBehaviour, float> OnTurretDamaged;
    private float _dmgPlayer = 100f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<ZombieBehaviour>() is { } zombie)
        {
            onHitZombie?.Invoke(zombie);
        }
        else if (other.GetComponentInParent<TurretBehaviour>() is { } turret)
        {
            OnTurretDamaged?.Invoke(turret, _dmgPlayer);
        }
    }
}
