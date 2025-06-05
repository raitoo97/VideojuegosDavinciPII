using System;
using UnityEngine;
public class ControlPlayer
{
    Movement _movement;
    PlayerAnimation _animation;
    bool _wasHoldingShift = false;
    bool _wasInGround;

    // Dash
    bool canDash = true;
    float dashCooldown = 1f;
    float dashCooldownTimer = 0f;
    float dashDuration = 0.2f;
    float dashTimer = 0f;
    bool isDashing = false;
    float dashImpulse = 20f;

    // Input guardado
    float horizontal;
    float vertical;
    float dodgeSpeedMultiplier = 1f;

    public ControlPlayer(Movement m, PlayerAnimation a)
    {
        _movement = m;
        _animation = a;
    }

    public void HandleInput() // Se llama en Update
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        var foward = MathF.Abs(horizontal) + MathF.Abs(vertical);
        foward = Math.Clamp(foward, 0f, 1f);
        bool isFoward = foward != 0;
        bool isDodgeMode = Input.GetKey(KeyCode.LeftShift);
        bool isGrounded = _movement.IsGrounded;
        bool isJumping = Input.GetKeyDown(KeyCode.Space);
        bool isDash = Input.GetKeyDown(KeyCode.Mouse0); // botón de dash

        dodgeSpeedMultiplier = 1f;

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
                _movement.StopDash(); // Este método frena el dash
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

        // ---------- ANIMACIONES ----------
        _animation.SetGround("ground", isGrounded);

        if (!isGrounded && _wasInGround)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
        else if (!isGrounded)
        {
            _animation.SetJump("jump", true);
            _animation.SetIdle("idle", false);
            _animation.SetWalk("walk", 0f);
        }
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

        // ---------- JUMP ----------
        if (isJumping && isGrounded && !isDodgeMode)
        {
            float jumpForce = 8f;
            _animation.SetJump("jump", true);
            _movement.Jump(jumpForce);
        }

        // ---------- TRANSFORMACIÓN ----------
        if (isDodgeMode && !_wasHoldingShift)
        {
            _animation.SetTransforming("transforming", true);
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            turretPj?.DesactivateSelf();
            GameManager.instance.player.GetComponent<Player>().GetMovement.ChangeSpeed(8f);
        }

        if (!isDodgeMode && _wasHoldingShift)
        {
            _animation.SetTransforming("transforming", false);
            var turretPj = GameObject.FindAnyObjectByType<TurretPj>();
            turretPj?.ActivateSelf();
            GameManager.instance.player.GetComponent<Player>().GetMovement.ChangeSpeed(
                GameManager.instance.player.GetComponent<Player>().GetInitSpeed
            );
        }

        // ---------- DODGE MODE ----------
        if (isDodgeMode && isFoward && isGrounded)
        {
            _animation.SetTransforming("transforming", true);
            _animation.SetDodge("dodging", foward);
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

        // ---------- TORRETA ----------
        var turretCheck = GameObject.FindAnyObjectByType<TurretPj>();
        if (isGrounded && !isDodgeMode) turretCheck?.ActivateSelf();
        else turretCheck?.DesactivateSelf();

        _wasHoldingShift = isDodgeMode;
        _wasInGround = isGrounded;
    }

    public void OnFixedUpdate() // Se llama en FixedUpdate
    {
        _movement.Move(horizontal, vertical, dodgeSpeedMultiplier);
        _movement.UpdateGroundCheck();
    }
}