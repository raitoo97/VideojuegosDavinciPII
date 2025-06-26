using System.Collections;
using UnityEngine;
public class WaterObstacle : Obstacles
{
    private AnimationWater _anim;
    private void OnEnable()
    {
        _anim = new AnimationWater(this.GetComponent<Renderer>());
    }
    private void Update()
    {
        _anim.OnUpdate();
    }
    protected override void ActionOntriggerEnter()
    {
        _isOnPlataform = true;
        if (_corrutine == null)
            _corrutine = StartCoroutine(ActionCoroutine());
    }
    protected override void ActionOntriggerExitr()
    {
        if (_corrutine == null) return;
        StopCoroutine(_corrutine);
        _corrutine = null;
        _isOnPlataform = false;
        if(_playerRef.GetController.GetDodgeMode)
            _playerRef.GetMovement.ChangeSpeed(15f);
        else
            _playerRef.GetMovement.ChangeSpeed(3f);
    }
    protected override IEnumerator ActionCoroutine()
    {
        while (_isOnPlataform)
        {
            _playerRef.GetMovement.ChangeSpeed(1f);
            yield return null;
        }
    }
}
