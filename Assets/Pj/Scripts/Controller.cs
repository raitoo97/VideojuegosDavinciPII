using UnityEngine;
public class Controller 
{
    Movement _movement;
    PlayerAnimation _animation;
    public Controller(Movement m, PlayerAnimation a)
    {
        _movement = m;
        _animation = a;
    }
    public void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _movement.Move(horizontal, vertical);

        float speed = new Vector2(horizontal, vertical).magnitude;
        _animation.SetAnimation("speed", speed);
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

}
