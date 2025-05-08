using UnityEngine;
public class Controller
{
    Movement _movement;
    PlayerAnimation _animation;
    bool _wasHoldingShift = false;
    bool _wasInGround;


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
        float speed = new Vector2(horizontal, vertical).magnitude;
        


        bool isMoving = speed > 0f;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKey(KeyCode.Space);
        bool isGrounded = _movement.IsGrounded;

        float dodgeSpeedMultiplier = 1f;

        // -------ANIMACIONES DE MOVIMIENTO------

        // Ground state
        _animation.SetGround("ground", isGrounded);

        // Cae (solo cuando estaba en el suelo y ahora no lo está)
        if (!isGrounded && _wasInGround)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        // Está en el aire (sigue saltando) Se mantiene la animación de salto
        else if (!isGrounded)
        {           
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        // Está en el suelo
        else
        {
            _animation.SetJump("jump", false);

            if (isMoving)
            {
                _animation.SetWalk("walk", speed);
                _animation.SetIdle("idle", false);
            }
            else
            {
                _animation.SetWalk("walk", 0f);
                _animation.SetIdle("idle", true);
            }
        }

        // ======== JUMP ========
        if (isJumping && isGrounded && !isDodgeMode)
        {
            float jumpForce = isMoving ? 1.3f : 1.2f;
            _animation.SetJump("jump", true);
            _movement.Jump(jumpForce);
        }

        // ======== DODGE Y TRANSFORMING ========
        if (isDodgeMode && !_wasHoldingShift)
        {
            _animation.SetTransforming("transforming", true); // Empezó a transformar
        }

        if (!isDodgeMode && _wasHoldingShift)
        {
            _animation.SetTransforming("transforming", false); // Volvió al modo trípode
        }

        if (isDodgeMode && isMoving && isGrounded)
        {
            _animation.SetTransforming("transforming", true);
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
        _wasInGround = isGrounded;
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