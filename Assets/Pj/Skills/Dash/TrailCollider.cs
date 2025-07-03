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
        Debug.Log($"Trigger with: {other.name}");

        if (other.GetComponentInParent<ZombieBehaviour>() is { } zombie)
        {
            Debug.Log("HIT ZOMBIE");
            onHitZombie?.Invoke(zombie);
        }
        else if (other.GetComponentInParent<TurretBehaviour>() is { } turret)
        {
            Debug.Log("HIT TURRET");
            OnTurretDamaged?.Invoke(turret, _dmgPlayer);
        }
    }
}
