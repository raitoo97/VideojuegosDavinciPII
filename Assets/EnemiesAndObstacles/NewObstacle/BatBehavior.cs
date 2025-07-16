using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
    private float _speed = 1.0f;
    private Vector3 movement;

    private void Start()
    {
        
    }
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }
}
