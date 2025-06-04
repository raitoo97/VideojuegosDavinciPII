using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float _speed;
    private bool _isDesactivate;
    public ShooterType shooterType;
    //Sound 
    AudioManager audioManager => AudioManager.instance;
    private void OnEnable()
    {
        StartCoroutine(DesactivateBulletCourutine());
        _speed = 60;
        _isDesactivate = false;
    }
    private void Update()
    {
        this.transform.localPosition += this.transform.forward * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isDesactivate) return;
        if (shooterType == ShooterType.Enemy && other.TryGetComponent<Player>(out var player))
        {
            Vector3 knockbackDir = (player.transform.position - transform.position) + Vector3.up * 2f;
            float knockbackForce = 5f;
            CameraShakeManager.instance.ShakeCamera(Shakes.EnemyMisilShoot);
            player.GetMovement.ReceiveKnockback(knockbackDir, knockbackForce);
            DesactivateBullet();
        }
        if (shooterType == ShooterType.Player && other.TryGetComponent<ZombieBehaviour>(out var enemy))
        {
            int randomIndex = Random.Range(0, audioManager.turretPlayerImpactSfx.Length);
            audioManager.PlaySfxRandomPitch(audioManager.turretPlayerImpactSfx[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Explosion, new Vector3(0f, 2f, 0f), Vector3.zero, enemy.transform);
            enemy.life = 0;
            DesactivateBullet();
        }
    }
    private void DesactivateBullet()
    {
        _isDesactivate = true;
        this.gameObject.SetActive(false);
    }
    IEnumerator DesactivateBulletCourutine()
    {
        yield return new WaitForSeconds(5);
        if (!_isDesactivate)
        {
            DesactivateBullet();
        }
    }
}
