using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Movement
{
    private float _speed;
    Transform _transform;

    //Constructor
    public Movement(Transform transform, float speed)
    {
        _speed = speed;
        _transform = transform;
    }

    public void Move(float horizontal, float vertical) 
    {
        var dir = _transform.forward * vertical;
        dir += _transform.right * horizontal;

        _transform.position += dir * _speed * Time.deltaTime;

        
        if (dir.magnitude != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _speed * Time.deltaTime);
        }
    }

    /*
    public Vector2 move;
    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    */
}
