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
    [SerializeField]private float _dmgPlayer;
    private void Start()
    {
        _dmgPlayer = 50;
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
