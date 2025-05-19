using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement
{
    private float _speed = 2f;
    Transform _transform;
    Transform _camera;
    bool _isGrounded;
    LayerMask _groundLayer;
    float _groundCheckDistance = 0.1f;
    Rigidbody _rb;
    Vector3 _lastPosition;

    
    
    public float CurrentSpeed { get; private set; }
   

    //Constructor
    public Movement(Transform transform, float speed, LayerMask groundLayer, Transform camera)
    {
        _speed = speed;
        _transform = transform;
        _groundLayer = groundLayer;
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
