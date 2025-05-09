using Unity.VisualScripting.FullSerializer;
using UnityEngine;
public class ControlPlayer
{
    Movement _movement;
    PlayerAnimation _animation;
    bool _wasHoldingShift = false;
    bool _wasInGround;

    
    public ControlPlayer(Movement m, PlayerAnimation a)
    {
        _movement = m;
        _animation = a;
    }
    public void OnUpdate()
    {
        //Input
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var foward = Mathf.Clamp(vertical, 0f, 1f);
        var reverse = Mathf.Clamp(vertical, -1f, 0f);
        //float speed = _movement.CurrentSpeed;
        //float normalizedSpeed = Mathf.Clamp(speed, 0f, 1f);

        float absoluteReverse = -reverse;

        bool isFoward = foward > 0.1f;
        bool isReverse = reverse < 0f;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKey(KeyCode.Space);
        bool isGrounded = _movement.IsGrounded;

        float dodgeSpeedMultiplier = 1f;

        // -------ANIMACIONES DE MOVIMIENTO------
   
        _animation.SetGround("ground", isGrounded);

        // Cae (solo cuando estaba en el suelo y ahora no lo está)
        if (!isGrounded && _wasInGround)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        // Está en el aire (sigue saltando) Se mantiene la animación de salto
        else if (!isGrounded)
        {           
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        // Está en el suelo
        else
        {
            _animation.SetJump("jump", false);
            if ( isReverse )
            {
                _animation.SetIdle("idle", false);
                _animation.SetReverse("reverse", absoluteReverse);
                _animation.SetWalk("walk", 0f);
            }
            else if (isFoward)
            {
                _animation.SetWalk("walk", foward);
                _animation.SetReverse("reverse", 0f);
                _animation.SetIdle("idle", false);
            }
            else
            {
                _animation.SetWalk("walk", 0f);
                _animation.SetReverse("reverse", 0f);
                _animation.SetIdle("idle", true);
            }
        }

        

        //          ======== JUMP ========
        if (isJumping && isGrounded && !isDodgeMode)
        {
            float jumpForce = isFoward ? 1.3f : 1.2f;
            _animation.SetJump("jump", true);
            _movement.Jump(jumpForce);
        }

        //          ======== DODGE Y TRANSFORMING ========
        if (isDodgeMode && !_wasHoldingShift)
        {
            _animation.SetTransforming("transforming", true); // Modo Bola
        }

        if (!isDodgeMode && _wasHoldingShift)
        {
            _animation.SetTransforming("transforming", false); // Volvió al modo trípode
        }

        if (isDodgeMode && isFoward && isGrounded)
        {
            _animation.SetTransforming("transforming", true);
            _animation.SetDodge("dodging", 1f);
            _animation.SetIdle("idle", false);
            dodgeSpeedMultiplier = 2f;
        }
        else if (isDodgeMode && !isFoward && isGrounded)
        {
            _animation.SetDodge("dodging", 0f); 
            _animation.SetIdle("idle", true);
            dodgeSpeedMultiplier = 1f;
        }
        else
        {
            _animation.SetDodge("dodging", 0f);
        }

        //      ======== MOVIMIENTO FÍSICO Y ESTADO ========
        _wasHoldingShift = isDodgeMode;
        _wasInGround = isGrounded;
        _movement.Move(horizontal, vertical, dodgeSpeedMultiplier);
        _movement.UpdateGroundCheck();
    }
}
