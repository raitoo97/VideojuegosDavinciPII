using UnityEngine;
public abstract class Obstacles : MonoBehaviour
{
    protected bool _isOnPlataform;
    protected Coroutine _corrutine;
    protected Player _playerRef;
    protected virtual void Awake()
    {
        _isOnPlataform = false;
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent<Player>(out var player))
        {
            _playerRef = player;
            if(_playerRef != null)
            {
                ActionOntriggerEnter();
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent<Player>(out var player))
        {
            _playerRef = null;
            if (_playerRef == null)
            {
                ActionOntriggerExitr();
            }
        }
    }
    protected abstract void ActionOntriggerEnter();
    protected abstract void ActionOntriggerExitr();
}
