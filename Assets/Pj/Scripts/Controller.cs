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
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        float speed = new Vector2(horizontal, vertical).magnitude;
        _movement.Move(horizontal, vertical);

        bool isMoving = speed > 0.01f;
        bool isHoldingShift = Input.GetKey(KeyCode.LeftShift);

        
        if (isHoldingShift && !_wasHoldingShift)
        {
            _animation.SetTransforming("transforming", true);
        }

        if (!isHoldingShift && _wasHoldingShift)
        {
            _animation.SetTransforming("transforming", false);
        }

        if (isHoldingShift && isMoving)
        {
            _animation.SetDodge("dodging", 1f);
            _animation.SetIdle("idle", false);
        }
        else if (isHoldingShift && !isMoving)
        {
            _animation.SetDodge("dodging", 0f);
            _animation.SetIdle("idle", true);
        }
        else
        {
            _animation.SetDodge("dodging", 0f);
            _animation.SetWalk("walk", speed);
        }

        //Al salir del if guarda el ultimo estado para el proximo frame
        _wasHoldingShift = isHoldingShift;
        
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