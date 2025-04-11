using UnityEngine;
public class NewBehaviourScript : MonoBehaviour
{
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
}
