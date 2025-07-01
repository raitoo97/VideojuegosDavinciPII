using System.Collections;
using UnityEngine;
public class RayCastTurret
{
    private LayerMask _mask;
    private Transform _transform;
    private float _distance;
    private bool _enabled;
    private float _endWidth = 0.5f;
    private float _startWidth = 0.05f;
    private LineRenderer _lineRenderer;
    private MonoBehaviour _corutineControl;
    private Coroutine _colorCoroutine;
    public RayCastTurret(Transform transform,LayerMask mask, float distance,Material _linerenderematerial,MonoBehaviour corutineControl)
    {
        _transform = transform;
        _mask = mask;
        _distance = distance;
        _corutineControl = corutineControl;
        _lineRenderer = _transform.gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = _linerenderematerial;
        _lineRenderer.positionCount = 2;
        _lineRenderer.numCapVertices = 20;
        _lineRenderer.startWidth = _startWidth;
        _lineRenderer.endWidth = _startWidth;
        _lineRenderer.startColor = new Color(0, 1, 0, 0.2f);
        _lineRenderer.endColor = new Color(0, 1, 0, 0.2f);
    }
    public void OnUpdate()
    {
        if(_transform == null) return;
        Ray _ray = new Ray(_transform.position, _transform.forward);
        if (Physics.Raycast(_ray, out RaycastHit _hit, _distance, _mask))
        {
            if (_hit.transform.gameObject.TryGetComponent<Player>(out var player))
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _ray.origin);
                _lineRenderer.SetPosition(1, _hit.point);
                if (_colorCoroutine == null)
                    _colorCoroutine = _corutineControl.StartCoroutine(ChangeLaserColor());
                return;
            }
        }
        _enabled = false;
        _lineRenderer.enabled = false;
        ResetLaser();
        if (_colorCoroutine != null)
        {
            _corutineControl.StopCoroutine(_colorCoroutine);
            _colorCoroutine = null;
        }
        Debug.DrawRay(_ray.origin, _ray.direction * _distance, Color.red);
    }
    public IEnumerator ChangeLaserColor()
    {
        Color colorOrginal = new Color(0, 1, 0, 0.2f);
        Color colorFinal = Color.red;
        float t = 0f;
        float totalTime = 2f;
        while (t <= totalTime)
        {
            t += Time.deltaTime;
            _lineRenderer.endColor = Color.Lerp(colorOrginal, colorFinal, t/ totalTime);
            _lineRenderer.startColor = Color.Lerp(colorOrginal, colorFinal, t / totalTime);
            _lineRenderer.endWidth = Mathf.Lerp(_startWidth, _endWidth, t / totalTime);
            yield return null;
        }
        _enabled = true;
        _colorCoroutine = null;
    }
    private void ResetLaser()
    {
        _lineRenderer.startColor = Color.green;
        _lineRenderer.endColor = Color.green;
        _lineRenderer.endWidth = _startWidth;
    }
    public bool IsEnabled { get => _enabled ; }
}
