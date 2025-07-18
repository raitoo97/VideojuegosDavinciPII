using System;
using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField]private float _speed;
    private bool _isDesactivate;
    public ShooterType shooterType;
    public static Action<Player,float,Transform> onHitPlayerBullet;
    public static Action<ZombieBehaviour> onHitZombie;
    public static event Action<TurretBehaviour, float> OnTurretDamaged;
    public static event Action <BossBehaviour, float> OnBossDamaged;
    [Header("Player dmg")]
    private float _dmgPlayer;
    private float _ultimtateRadius;
    public LayerMask mask;
    private void Start()
    {
        _dmgPlayer = 50;
        _ultimtateRadius = 1;
    }
    private void OnEnable()
    {
        StartCoroutine(DesactivateBulletCourutine());
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
            onHitPlayerBullet?.Invoke(player,10,this.transform);
            DesactivateBullet();
        }
        if(shooterType == ShooterType.Player && other.TryGetComponent<IEnemies>(out var detectedOneEnemy))
        {
            if(other.TryGetComponent<ZombieBehaviour>(out var Zombie))
            {
                onHitZombie?.Invoke(Zombie);
            }else if(other.TryGetComponent<TurretBehaviour>(out var turret))
            {
                OnTurretDamaged?.Invoke(turret, _dmgPlayer);
            }
            DesactivateBullet();
        }
        if (shooterType == ShooterType.SuperPlayer && other.TryGetComponent<IEnemies>(out var detectedEnemies))
        {
            var hits = Physics.OverlapSphere(this.transform.position, _ultimtateRadius, mask);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<TurretBehaviour>(out var turrets))
                {
                    OnTurretDamaged?.Invoke(turrets, _dmgPlayer);
                }
                else if (hit.TryGetComponent<ZombieBehaviour>(out var zombies))
                {
                    onHitZombie?.Invoke(zombies);
                }
            }
            DesactivateBullet();
        }
        if (shooterType == ShooterType.SuperPlayer && other.TryGetComponent<BossBehaviour>(out var bossulti))
        {
            OnBossDamaged?.Invoke(bossulti, _dmgPlayer);
            DesactivateBullet();
        }
        if (shooterType == ShooterType.Player && other.TryGetComponent<BossBehaviour>(out var boss))
        {
            OnBossDamaged?.Invoke(boss, _dmgPlayer);
            DesactivateBullet();
        }
        if (other.gameObject.layer == 13)//Ground
        {
            DesactivateBullet();
        }
        if (shooterType == ShooterType.Enemy && other.gameObject.layer == 14)//Shield
        {
            DesactivateBullet();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _ultimtateRadius);
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
