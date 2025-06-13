using System;
using UnityEngine;
public class ZombieAttack : MonoBehaviour
{
    public static Action<Player, float> onHitPlayerZombie;
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Shield")
        {
            
        }
        if (c.gameObject.tag == "Player")
        {
            Player player = c.gameObject.GetComponent<Player>();
            if (player != null)
            {
                onHitPlayerZombie?.Invoke(player, 1f);
            }
        }
    }
}
