using UnityEngine;
public class RayCastTurretPj
{
    private LayerMask _mask;
    private Transform _transform;
    private float _distance;
    private bool _enabled;
    public RayCastTurretPj(Transform transform, LayerMask mask, float distance)
    {
        _transform = transform;
        _mask = mask;
        _distance = distance;
    }
    public void OnUpdate()
    {
        Ray _ray = new Ray(_transform.position, _transform.forward);
        if (Physics.Raycast(_ray, out RaycastHit _hit, _distance, _mask))
        {
            if (_hit.transform.gameObject.TryGetComponent<IEnemies>(out var Enemie) && GameManager.instance.player.GetComponent<TurretPj>().isActivateGetter)
            {
                _enabled = true;
            }
            else
            {
                _enabled = false;
            }
        }
        else
        {
            _enabled = false;
        }
        Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.magenta);
    }
    public bool IsEnabled { get => _enabled; }
}
