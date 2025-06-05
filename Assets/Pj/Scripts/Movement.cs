using UnityEngine;
public class Movement
{
    private float _speed = 2f;
    Transform _transform;
    Transform _groundCheck;
    Transform _camera;
    bool _isGrounded;
    LayerMask _groundLayer;
    Rigidbody _rb;
    Vector3 _lastPosition;
    private Vector3 _pendingKnockback;
    private bool _applyKnockback;
    public float CurrentSpeed { get; private set; }
    //Constructor
    public Movement(Transform transform,Transform _groundCheck, float speed, LayerMask groundLayer, Transform camera)
    {
        _speed = speed;
        _transform = transform;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        _rb = transform.GetComponent<Rigidbody>();
        //Camera
        _camera = camera;
    }
    public void Move(float inputHorizontal, float inputVertical, float speedMultiplier) 
    {
        //Vectores _camera
        Vector3 cameraFoward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraFoward.y = 0;
        cameraRight.y = 0;
        cameraFoward = cameraFoward.normalized;
        cameraRight = cameraRight.normalized;
        //Vectores inputs relativos a _camera
        Vector3 dirVertical = cameraFoward * inputVertical;
        Vector3 dirHorizontal= cameraRight * inputHorizontal;
        Vector3 movementRelativeToCamera = dirHorizontal + dirVertical;
        if (movementRelativeToCamera.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementRelativeToCamera);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * Time.deltaTime);
        }
        _transform.position += movementRelativeToCamera * _speed * speedMultiplier * Time.deltaTime;
        CurrentSpeed = Vector3.Distance(_transform.position, _lastPosition) / Time.deltaTime; //Calcula la distancia recorrida
        _lastPosition = _transform.position;
    }
    public void UpdateGroundCheck() 
    {

        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        _isGrounded = Physics.CheckSphere(origin, radius, _groundLayer);
        Debug.Log(_isGrounded);
    }
    public void OnDraw()
    {
        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, radius);
    }
    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
            Player.TriggerShootInstant?.Invoke();
        }
    }
    public void OnFixedUpdate()
    {
        if (_applyKnockback)
        {
            _rb.AddForce(_pendingKnockback, ForceMode.Impulse);
            _applyKnockback = false;
        }
    }
    public void ReceiveKnockback(Vector3 direction, float force)
    {
        if (_applyKnockback) return;
        _pendingKnockback = direction.normalized * force;
        _applyKnockback = true;
    }
    public void ChangeSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    public float GetSpeed { get => _speed; }
    public bool IsGrounded => _isGrounded;
}
