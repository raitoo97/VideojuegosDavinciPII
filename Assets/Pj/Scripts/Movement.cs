using UnityEngine;
public class Movement
{
    public Vector3 LastMoveDirection { get; private set; }
    /*
    public float CurrentSpeed { get; private set; }
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
        // Vectores _camera
        Vector3 cameraFoward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        cameraFoward.y = 0;
        cameraRight.y = 0;
        cameraFoward = cameraFoward.normalized;
        cameraRight = cameraRight.normalized;

        // Vectores inputs relativos a _camera
        Vector3 dirVertical = cameraFoward * inputVertical;
        Vector3 dirHorizontal = cameraRight * inputHorizontal;
        Vector3 movementRelativeToCamera = dirHorizontal + dirVertical;

        if (movementRelativeToCamera.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementRelativeToCamera);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _speed * Time.fixedDeltaTime);
        }

        Vector3 targetPosition = _rb.position + movementRelativeToCamera * _speed * speedMultiplier * Time.fixedDeltaTime;
        _rb.MovePosition(targetPosition);

        
        CurrentSpeed = Vector3.Distance(_rb.position, _lastPosition) / Time.fixedDeltaTime;
        _lastPosition = _rb.position;

    }

    */
    private float _speed;
    Transform _groundCheck;
    bool _isGrounded;
    LayerMask _groundLayer;
    Rigidbody _rb;
    private Vector3 _pendingKnockback;
    private bool _applyKnockback;
    public float CurrentSpeed { get; private set; }
    private Vector3 _smoothedDirection = Vector3.zero;
    //Constructor
    public Movement(Rigidbody rb, Transform _groundCheck, float speed, LayerMask groundLayer)
    {
        _speed = speed;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        _rb = rb;
    }
    public void Move(float inputHorizontal, float inputVertical)
    {
        Vector2 _directionVector = new Vector2(inputHorizontal, inputVertical);
        Vector3 _dir = new Vector3(_directionVector.x, 0, _directionVector.y).normalized;
        _smoothedDirection = Vector3.Lerp(_smoothedDirection, _dir, 5f * Time.fixedDeltaTime);

        if (_smoothedDirection.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_smoothedDirection);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
        }
        Vector3 newPosition = _rb.position + _smoothedDirection * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
        //LastMoveDirection = movementRelativeToCamera;
    }
    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
            Player.TriggerShootInstant?.Invoke();
        }
    }

    public void Dash(float impulse)
    {
        Vector3 dashDirection = LastMoveDirection;
        if (dashDirection == Vector3.zero )
            dashDirection = _rb.transform.forward; // fallback por si no se mueve

        if (_rb.velocity.magnitude != 0)
        {
            _rb.AddForce(dashDirection.normalized * impulse, ForceMode.Impulse);
        }
    }

    public void StopDash()
    {
        _rb.velocity = Vector2.zero;
    }
    public void UpdateGroundCheck() 
    {

        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        _isGrounded = Physics.CheckSphere(origin, radius, _groundLayer);
    }
    public void OnDraw()
    {
        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, radius);
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
    public void OnFixedUpdate()
    {
        if (_applyKnockback)
        {
            _rb.AddForce(_pendingKnockback, ForceMode.Impulse);
            _applyKnockback = false;
        }
    }
}
