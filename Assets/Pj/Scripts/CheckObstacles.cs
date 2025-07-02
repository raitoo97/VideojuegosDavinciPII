using UnityEngine;
public class CheckObstacles
{
    private Transform _transform;
    private Vector3 _origin;
    private Vector3 _boxSize;
    private Vector3 _halfExtents;
    private Quaternion _orientation;
    public LayerMask maskObstacles;
    private float _dotProduct;
    public CheckObstacles(Transform _transform , LayerMask maskObstacles)
    {
        this._transform = _transform;
        this.maskObstacles = maskObstacles;
        _boxSize = new Vector3(2f, 2f, 20f);
        _halfExtents = _boxSize * 0.5f;
        _dotProduct = 0.0f;
    }
    public void OnUpdate()
    {
        _origin = _transform.position + _transform.forward * 15f;
        _orientation = _transform.rotation;
        bool isObstaclesfront = Physics.CheckBox(_origin, _halfExtents, _orientation, maskObstacles);
        _dotProduct = Vector3.Dot(_transform.forward, (Camera.main.transform.position - _transform.position).normalized);
        if (isObstaclesfront && _dotProduct > 0f)
        {
            ManagerUI.instance.canShowWarning = true;
        }
        else
        {
            ManagerUI.instance.canShowWarning = false;
        }
    }
    public void Draw()
    {
        Gizmos.color = Color.red;
        Vector3 boxCenter = _transform.position + _transform.forward * 15f;
        Gizmos.matrix = Matrix4x4.TRS(boxCenter, _transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, _boxSize);
    }
}
