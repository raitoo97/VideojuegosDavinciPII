using UnityEngine;
using System;
public class ControlPlayer
{
    private Movement _movement;
    private PlayerAnimation _animation;
    private bool _wasHoldingShift = false;
    private bool _wasInGround;
    private float horizontal;
    private float vertical;
    // Dash
    private bool canDash = true;
    private float dashCooldown = 1f;
    private float dashCooldownTimer = 0f;
    private float dashDuration = 0.2f;
    private float dashTimer = 0f;
    private bool isDashing = false;
    private float dashImpulse = 40f;
    public ControlPlayer(Movement m, PlayerAnimation a)
    {
        _movement = m;
        _animation = a;
    }
    public void OnUpdate()
    {
        //Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        var foward = MathF.Abs(horizontal) + MathF.Abs(vertical);
        foward = Math.Clamp(foward, 0f, 1f);
        bool isFoward = foward != 0;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        bool isGrounded = _movement.IsGrounded;
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        bool isDash = Input.GetKeyDown(KeyCode.Mouse0); // botón de dash
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
            if (isFoward)
            {
                _animation.SetIdle("idle", false);
                _animation.SetWalk("walk", foward);
            }
            else
            {
                _animation.SetWalk("walk", 0f);
                _animation.SetIdle("idle", true);
            }
        }
        //          ======== JUMP ========
        if (isJumping && isGrounded && !isDodgeMode)
        {
            float jumpForce = 8f;
            _animation.SetJump("jump", true);
            _movement.Jump(jumpForce);
        }
        //          ======== DODGE Y TRANSFORMING ========
        if (isDodgeMode && !_wasHoldingShift)
        {
            _animation.SetTransforming("transforming", true); // Modo Bola
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            if (turretPj != null)
            {
                turretPj.DesactivateSelf();
                GameManager.instance.player.GetComponent<Player>().GetMovement.ChangeSpeed(15f);//Acelero la velocidad del player
            }
        }
        if (!isDodgeMode && _wasHoldingShift)
        {
            _animation.SetTransforming("transforming", false); // Volvió al modo trípode
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            if (turretPj != null)
            {
                turretPj.ActivateSelf();
                GameManager.instance.player.GetComponent<Player>().GetMovement.ChangeSpeed(GameManager.instance.player.GetComponent<Player>().GetInitSpeed);//Toma la velocidad normal del player
            }
        }
        if (isDodgeMode && isFoward && isGrounded)
        {
            _animation.SetTransforming("transforming", true);
            _animation.SetDodge("dodging", foward);
            _animation.SetIdle("idle", false);
        }
        else if (isDodgeMode && !isFoward && isGrounded)
        {
            _animation.SetDodge("dodging", 0f);
            _animation.SetIdle("idle", true);
        }
        else
        {
            _animation.SetDodge("dodging", 0f);
        }
        if (isGrounded && !isDodgeMode)
        {
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            if (turretPj != null)
            {
                turretPj.ActivateSelf();
            }
        }
        else
        {
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            if (turretPj != null)
            {
                turretPj.DesactivateSelf();
            }
        }
        // ---------- DASH ----------
        if (isDash && canDash && !isDodgeMode && isGrounded)
        {
            //_animation.SetDash("dash", true); Animacion si es que hay
            _movement.Dash(dashImpulse);
            isDashing = true;
            dashTimer = dashDuration;
            canDash = false;
            dashCooldownTimer = dashCooldown;
        }
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                _movement.StopDash(); // Este mtodo frena el dash
                //_animation.SetDash("dash", false); animacion si quieren poner
            }
        }
        if (!canDash)
        {
            dashCooldownTimer -= Time.deltaTime;
            if (dashCooldownTimer <= 0f)
            {
                canDash = true;
            }
        }
        //      ======== MOVIMIENTO FÍSICO Y ESTADO ========
        _wasHoldingShift = isDodgeMode;
        _wasInGround = isGrounded;
        _movement.UpdateGroundCheck();
    }
    public void OnfixedUpdate()
    {
        _movement.Move(horizontal, vertical);
    }
}
