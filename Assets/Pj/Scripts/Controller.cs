using UnityEngine;
public class Controller
{
    Movement _movement;
    PlayerAnimation _animation;
    bool _wasHoldingShift = false;
    
    public Controller(Movement m, PlayerAnimation a)
    {
        _movement = m;
        _animation = a;
    }
    public void OnUpdate()
    {
        //Input
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        float rawSpeed = new Vector2(horizontal, vertical).magnitude;
        float speed = rawSpeed < 0.05f ? 0f : rawSpeed; // Elimina ruido


        bool isMoving = speed > 0f;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKey(KeyCode.Space);
        bool isGrounded = _movement.IsGrounded;

        float dodgeSpeedMultiplier = 1f;

        // ANIMACIONES DE MOVIMIENTO 
        if (!isGrounded)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        else if (isMoving)
        {
            _animation.SetJump("jump", false);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", speed);
        }
        else // Quieto y en el piso
        {
            _animation.SetJump("jump", false);
            _animation.SetWalk("walk", 0f);
            _animation.SetIdle("idle", true);
        }

        // ======== JUMP ========
        if (isJumping && isGrounded && !isDodgeMode)
        {
            float jumpForce = isMoving ? 1.3f : 1.2f;
            _movement.Jump(jumpForce);
            _animation.SetJump("jump", true);
        }

        // ======== DODGE Y TRANSFORMING ========
        if (isDodgeMode && !_wasHoldingShift && isGrounded)
        {
            _animation.SetTransforming("transforming", true); // Empezó a transformar
        }

        if (!isDodgeMode && _wasHoldingShift && isGrounded)
        {
            _animation.SetTransforming("transforming", false); // Volvió al modo trípode
        }

        if (isDodgeMode && isMoving && isGrounded)
        {
            _animation.SetDodge("dodging", 1f);
            _animation.SetIdle("idle", false);
            dodgeSpeedMultiplier = 2f;
        }
        else if (isDodgeMode && !isMoving && isGrounded)
        {
            _animation.SetDodge("dodging", 0f); // Usá primer frame de animación si querés idle en bola
            _animation.SetIdle("idle", true);
            dodgeSpeedMultiplier = 1f;
        }
        else
        {
            _animation.SetDodge("dodging", 0f);
        }

        // ======== MOVIMIENTO FÍSICO Y ESTADO ========
        _wasHoldingShift = isDodgeMode;

        _movement.Move(horizontal, vertical, dodgeSpeedMultiplier);
        _movement.UpdateGroundCheck();
    }
}
/*
[SerializeField] float _speed = 15f;
PlayerMovement _input;
Rigidbody _rb;
private Animator _animator;    
void Start()
{
    _input = GetComponent<PlayerMovement>();
    _rb = GetComponent<Rigidbody>();
    _animator = GetComponentInChildren<Animator>();
}
void Update()
{
    Vector3 direction = new Vector3(_input.move.x, 0, _input.move.y) * _speed * Time.deltaTime;
    if (_input.move != Vector2.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
    }
    _animator.SetFloat("speed", _input.move.magnitude); // input.move.magnitude vector normalizado por sistema
    _rb.MovePosition(_rb.position + direction);
}
*/