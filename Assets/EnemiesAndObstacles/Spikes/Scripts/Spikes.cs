using System.Collections;
using UnityEngine;
public class Spikes : MonoBehaviour
{
    [SerializeField] private bool _isOnPlataform;
    private Coroutine _corrutine;
    private void Awake()
    {
        _isOnPlataform = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.TryGetComponent<Player>(out var player))
        {
            print("sadasdasdsadsa");
            _isOnPlataform = true;
            _corrutine = StartCoroutine(MakeDamage());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent<Player>(out var player))
        {
            if (_corrutine == null) return;
            StopCoroutine(_corrutine);
            _corrutine = null;
            _isOnPlataform = false;
        }
    }
    IEnumerator MakeDamage()
    {
        while (_isOnPlataform)
        {
            yield return new WaitForSeconds(3);
        }
    }
}
