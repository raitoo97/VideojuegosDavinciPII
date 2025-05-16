using System.Collections;
using UnityEngine;
public enum ShooterType
{
    Player,
    Enemy
}
public class Bullet : MonoBehaviour
{
    private float _speed;
    private bool _isDesactivate;
    public ShooterType shooterType;// Quién disparó la bala
    private void OnEnable()
    {
        StartCoroutine(DesactivateBullet());
        _speed = 60;
        _isDesactivate = false;
    }
    private void Update()
    {
        this.transform.position += this.transform.forward * _speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_isDesactivate) return;
        if (shooterType == ShooterType.Enemy && other.TryGetComponent<Player>(out var player))
        {
            DeactivateBullet();
        }
        if (shooterType == ShooterType.Player && other.TryGetComponent<ZombieBehaviour>(out var enemy))
        {
            print("Trigereo con el zombie");
            enemy.life = 0;
            DeactivateBullet();
        }
    }
    private void DeactivateBullet()
    {
        _isDesactivate = true;
        this.gameObject.SetActive(false);
    }
    IEnumerator DesactivateBullet()
    {
        yield return new WaitForSeconds(5);
        if (!_isDesactivate)
        {
            DeactivateBullet();
        }
    }
}
