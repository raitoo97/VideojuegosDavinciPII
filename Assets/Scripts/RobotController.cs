using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    PlayerMovement input;
    Rigidbody rb;
    public float speed = 15f;
    
    void Start()
    {
        input = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {

        Vector3 direction = new Vector3(input.move.x * speed * Time.deltaTime,0, input.move.y * speed * Time.deltaTime);
        
        rb.MovePosition(rb.position + direction);
    }
}
