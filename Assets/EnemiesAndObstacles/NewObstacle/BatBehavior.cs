using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    private float _speed = 10f;
    private float _damage = 30f;
    private float _existenceTime = 25f;
    private Coroutine _despawnCoroutine;
    public static Action<Player, float> onHitPlayerBeam;

    AudioSource _audioSource;
    void FixedUpdate()
    {
        transform.position += transform.forward * _speed * Time.fixedDeltaTime;
    }
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        if (_despawnCoroutine != null )
        {
            StopCoroutine(_despawnCoroutine);
        }
        _despawnCoroutine = StartCoroutine(Despawn());
    }
    private void OnDisable()
    {
        _audioSource?.Stop();
        if (_despawnCoroutine != null)
        {
            StopCoroutine(_despawnCoroutine);
            _despawnCoroutine = null;
        }
    }
    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_existenceTime);
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            onHitPlayerBeam?.Invoke(player, 30);
            //player.DamagePlayer(_damage);
        }
    }
}
