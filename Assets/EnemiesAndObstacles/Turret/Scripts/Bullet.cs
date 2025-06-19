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
    [Header("Player dmg")]
    private float _dmgPlayer;
    private float _ultimtateRadius;
    public LayerMask mask;
    private void Start()
    {
        _dmgPlayer = 50;
        _ultimtateRadius = 3;
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
        if (shooterType == ShooterType.Enemy && other.gameObject.layer == 14)
        {
            DesactivateBullet();
        }
        if (shooterType == ShooterType.Player && other.TryGetComponent<ZombieBehaviour>(out var enemy))
        {
            onHitZombie?.Invoke(enemy);
            DesactivateBullet();
        }
        if (other.gameObject.layer == 13)
        {
            DesactivateBullet();
        }
        if (shooterType == ShooterType.Player && other.TryGetComponent<TurretBehaviour>(out var turret))
        {
            OnTurretDamaged?.Invoke(turret, _dmgPlayer);
            DesactivateBullet();
        }
        if (shooterType == ShooterType.SuperPlayer && other.TryGetComponent<TurretBehaviour>(out var turrets))
        {
            var hits = Physics.OverlapSphere(this.transform.position, _ultimtateRadius, mask);
            foreach (var hit in hits)
            {
                var currenthit = hit.GetComponent<TurretBehaviour>();
                if (currenthit == null) continue;
                OnTurretDamaged?.Invoke(currenthit, _dmgPlayer);
            }
            DesactivateBullet();
        }
        if (shooterType == ShooterType.SuperPlayer && other.TryGetComponent<ZombieBehaviour>(out var enemies))
        {
            var hits = Physics.OverlapSphere(this.transform.position, _ultimtateRadius, mask);
            foreach (var hit in hits)
            {
                var currenthit = hit.GetComponent<ZombieBehaviour>();
                if (currenthit == null) continue;
                onHitZombie?.Invoke(currenthit);
            }
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
