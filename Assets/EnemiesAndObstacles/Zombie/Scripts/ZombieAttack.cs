using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    AudioManager audioManager => AudioManager.Instance;
    

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            Player player = c.gameObject.GetComponent<Player>();
            if (player != null)
            {
                // Play attack sound effect
                int randomIndex = UnityEngine.Random.Range(0, audioManager.zombieAttackSfx.Length);
                audioManager.PlaySfxRandomPitch(audioManager.zombieAttackSfx[randomIndex]);

                player.DamagePlayer(1f);
            }
        }
    }
}
