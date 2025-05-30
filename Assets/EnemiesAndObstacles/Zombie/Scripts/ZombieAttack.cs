using UnityEngine;
public class ZombieAttack : MonoBehaviour
{
    AudioManager audioManager => AudioManager.instance;
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            Player player = c.gameObject.GetComponent<Player>();
            if (player != null)
            {
                // Play attack sound effect
                int randomIndex = Random.Range(0, audioManager.zombieAttackSfx.Length);
                audioManager.PlaySfxRandomPitch(audioManager.zombieAttackSfx[randomIndex]);
                player.DamagePlayer(1f);
                CameraShakeManager.instance.ShakeCamera(Shakes.PlayerUnderAtack);
                ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f),new Vector3(-90f,0f,0f));
            }
        }
    }
}
