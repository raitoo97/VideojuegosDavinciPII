using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    private float _speed = 4.0f;
    private float _damage = 30f;
    private float _existenceTime = 10f;

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.DamagePlayer(_damage);
        }
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_existenceTime);
        this.gameObject.SetActive(false);
    }
}
