using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement
{
    private float _speed;
    Transform _transform;
    bool _isGrounded;
    LayerMask _groundLayer;
    float _groundCheckDistance = 0.1f;
    Rigidbody _rb;

    //Constructor
    public Movement(Transform transform, float speed, LayerMask groundLayer)
    {
        _speed = speed;
        _transform = transform;
        _groundLayer = groundLayer;
        _rb = transform.GetComponent<Rigidbody>();
    }

    public void Move(float horizontal, float vertical, float speedMultiplier = 1f) 
    {
        var dir = _transform.forward * vertical;
        dir += _transform.right * horizontal;

        _transform.position += dir * _speed * speedMultiplier * Time.deltaTime;

        
        if (dir.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * Time.deltaTime);
        }

        
    }

    public void UpdateGroundCheck() 
    {
        _isGrounded = Physics.Raycast(_transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }
        public bool IsGrounded => _isGrounded;

    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
        }

    }
    
}
