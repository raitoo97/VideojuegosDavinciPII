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
        //Moving
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        float speed = new Vector2(horizontal, vertical).magnitude;

        //Dodge
        bool isMoving = speed > 0.01f;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        float dodgeSpeedMultiplier = 1f;
        
        bool isJumping = Input.GetKey(KeyCode.Space);

        if (isMoving)
        {
            _animation.SetIdle("idle", false);
        }
        else
        {
            _animation.SetIdle("idle", true);
        }

        #region Walk/Dodge

        if (isDodgeMode && !_wasHoldingShift && _movement.IsGrounded)
        {
            _animation.SetTransforming("transforming", true);
            dodgeSpeedMultiplier = 2f;
        }

        if (!isDodgeMode && _wasHoldingShift && _movement.IsGrounded)
        {
            _animation.SetTransforming("transforming", false);
            dodgeSpeedMultiplier= 1f;
        }

        if (isDodgeMode && isMoving && _movement.IsGrounded)
        {
            _animation.SetTransforming("transforming", true);
            _animation.SetDodge("dodging", 1f);
            _animation.SetIdle("idle", false);
            dodgeSpeedMultiplier = 2f;
        }
        else if (isDodgeMode && !isMoving && _movement.IsGrounded)
        {
            _animation.SetDodge("dodging", 0f);
            _animation.SetIdle("idle", true);
            dodgeSpeedMultiplier = 1f;
        }
        else
        {
            _animation.SetDodge("dodging", 0f);
            _animation.SetWalk("walk", speed);
        }
        #endregion

        #region Jump

        
        if (isJumping && _movement.IsGrounded && !isDodgeMode && !isMoving) 
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", true);
            _movement.Jump(1.2f);
        }
        else if (isJumping && _movement.IsGrounded && !isDodgeMode && isMoving)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _movement.Jump(1.3f);
        }
        else
        {
            _animation.SetJump("jump", false);
        }
        #endregion
        //Al salir del if guarda el ultimo estado para el proximo frame
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