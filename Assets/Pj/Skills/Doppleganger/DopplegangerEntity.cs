using System;
using UnityEngine;

public class DopplegangerEntity : MonoBehaviour
{
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentLife;
    public static Action OnDopplegangerDeath;
    private void HandleHitDopplegangerZombie(Player player, float damage)
    {
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.zombieAttackSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.zombieAttackSfx[randomIndex]);
        CameraShakeManager.instance.ShakeCamera(Shakes.PlayerUnderAtack);
        ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f), new Vector3(UnityEngine.Random.Range(0f, 180f), 0f, 0f), GameManager.instance.player.transform);
        player.DamagePlayer(damage);
    }
    public void TakeDamager(float damage)
    {
        _currentLife -= damage;
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.playerDamageSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.playerDamageSfx[randomIndex]); //sound effect
        if (_currentLife <= 0f)
        {
            OnDopplegangerDeath?.Invoke();
        }
    }
}
