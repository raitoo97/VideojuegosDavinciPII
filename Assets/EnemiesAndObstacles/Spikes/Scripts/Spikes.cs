using System;
using System.Collections;
using UnityEngine;
public class Spikes : Obstacles
{
    public static Action<float> OnTriggerSpikes;
    private float damage;
    private bool canShield;
    private bool _hasActiveShield;
    protected override void Awake()
    {
        base.Awake();
        damage = 10f;

        
    }

    private void Start()
    {
        if (Shield.instance != null)
        {
            canShield = Shield.instance.canShield;
            _hasActiveShield = canShield;
        }
    }
    protected override void ActionOntriggerEnter()
    {
            _isOnPlataform = true;
        bool shieldActive = Shield.instance != null && Shield.instance.canShield;

            if (_corrutine == null && !shieldActive)
                _corrutine = StartCoroutine(ActionCoroutine());
    }
    protected override void ActionOntriggerExitr()
    {
        if (_corrutine == null) return;
        StopCoroutine(_corrutine);
        _corrutine = null;
        _isOnPlataform = false;
    }
    protected override IEnumerator ActionCoroutine()
    {
        while (_isOnPlataform)
        {

            OnTriggerSpikes?.Invoke(damage);
            yield return new WaitForSeconds(2);
            OnTriggerSpikes?.Invoke(damage);
            yield return new WaitForSeconds(2);
        }
    }
}
