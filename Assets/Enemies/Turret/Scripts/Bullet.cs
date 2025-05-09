using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private float _speed;
    private bool _isDesactivate;
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
        if(other.transform.gameObject.TryGetComponent<Player>(out var player))
        {
            print(player.name);
            if (!_isDesactivate)
            {
                DeactivateBullet();
            }
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
