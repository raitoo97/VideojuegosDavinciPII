using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Vector2 move;
    void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
}
