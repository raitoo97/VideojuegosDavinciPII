using UnityEngine;
public class PlayerMenuMovement
{
    public Vector3 LastMoveDirection { get; private set; }
    private float _speed;
    private Transform _groundCheck;
    private bool _isGrounded;
    private LayerMask _groundLayer;
    private Rigidbody _rb;
    private Transform _transformPj;
    private LayerMask _layerWall;
    public PlayerMenuMovement(Rigidbody rb, Transform _groundCheck, float speed, LayerMask groundLayer, Transform _transformPj, LayerMask _layerWall)
    {
        _speed = speed;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        this._transformPj = _transformPj;
        this._layerWall = _layerWall;
        _rb = rb;
    }
    public void Move(float inputHorizontal, float inputVertical)
    {
        Vector3 _dir = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        if (_dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_dir);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
        }
        Vector3 newPosition = _rb.position + _dir * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    public void RotateOnly(float inputHorizontal, float inputVertical)
    {
        Vector3 _dirRot = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        if (_dirRot.magnitude > 0.001f)
        {
            Quaternion _rotDir = Quaternion.LookRotation(_dirRot);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, _rotDir, 10 * Time.fixedDeltaTime));
        }
    }
    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
        }
    }
    public void UpdateGroundCheck()
    {
        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        _isGrounded = Physics.CheckSphere(origin, radius, _groundLayer);
    }
    public void ChangeSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    public bool IsBlocked(float radius = 0.5f)
    {
        return Physics.CheckSphere(_transformPj.position + _transformPj.up + _transformPj.forward, radius, _layerWall);
    }
    public void DrawGizmos()
    {
        if (_transformPj == null) return;
        Vector3 center = _transformPj.position + _transformPj.up + _transformPj.forward;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, 0.5f);
    }
    public bool IsGrounded => _isGrounded;
}
