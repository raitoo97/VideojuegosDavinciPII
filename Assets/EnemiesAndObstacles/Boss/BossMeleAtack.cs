using System;
using UnityEngine;
public class BossMeleAtack : MonoBehaviour
{
    [SerializeField]private Collider _armCollider;
    public static Action<Player, float> onHitPlayerBoss;
    private float _damage = 20f;
    void Start()
    {
        _armCollider.enabled = false;
    }
    public void EnablePunchCollider()
    {
        _armCollider.enabled = true;
    }
    public void DisablePunchCollider()
    {
        _armCollider.enabled = false;
    }
    private void Update()
    {
        print("Arm Enable" + _armCollider.enabled);
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.GetComponent<Player>())
        {
            var player = other.GetComponent<Player>();
            onHitPlayerBoss?.Invoke(player, _damage);
        }
    }
}
