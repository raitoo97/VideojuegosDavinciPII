using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] float speed = 15f;

    PlayerMovement input;
    Rigidbody rb;
    private Animator animator;
    
    
    void Start()
    {
        input = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Vector3 direction = new Vector3(input.move.x, 0, input.move.y) * speed * Time.deltaTime;
        if (input.move != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
        animator.SetFloat("speed", input.move.magnitude); // input.move.magnitude vector normalizado por sistema
        rb.MovePosition(rb.position + direction);
    }
}
