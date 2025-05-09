using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement
{
    private float _speed = 2f;
    Transform _transform;
    bool _isGrounded;
    LayerMask _groundLayer;
    float _groundCheckDistance = 0.1f;
    Rigidbody _rb;
    Vector3 _lastPosition;
    
    public float CurrentSpeed { get; private set; }
   

    //Constructor
    public Movement(Transform transform, float speed, LayerMask groundLayer)
    {
        _speed = speed;
        _transform = transform;
        _groundLayer = groundLayer;
        _rb = transform.GetComponent<Rigidbody>();
    }


    public void Move(float horizontal, float vertical, float speedMultiplier) 
    {

        var dirVertical = _transform.forward * vertical;
        var dirHorizontal= _transform.right * horizontal;

        

        if (dirVertical.magnitude >1f)
        {
            dirVertical = dirVertical.normalized;
        }

        if (dirHorizontal.magnitude > 1f)
        {
            dirHorizontal = dirHorizontal.normalized;
        }

        if (dirHorizontal.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dirHorizontal);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * Time.deltaTime);
            
        }

        _transform.position += dirVertical * _speed * speedMultiplier * Time.deltaTime;
        
        CurrentSpeed = Vector3.Distance(_transform.position, _lastPosition) / Time.deltaTime; //Calcula la distancia recorrida
        _lastPosition = _transform.position;

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
