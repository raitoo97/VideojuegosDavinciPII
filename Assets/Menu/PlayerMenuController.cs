using System;
using UnityEngine;
public class PlayerMenuController
{
    private PlayerMenuMovement _movement;
    private PlayerMenuAnimation _animation;
    private bool _wasHoldingShift = false;
    private bool _wasInGround;
    private float horizontal;
    private float vertical;
    private bool isDodgeMode;
    private PlayerMenu _playerMenu;
    private bool _cinematicMode;
    public PlayerMenuController(PlayerMenuMovement m, PlayerMenuAnimation a,PlayerMenu player)
    {
        _movement = m;
        _animation = a;
        _playerMenu = player;
        _cinematicMode = true;
    }
    public void OnUpdate()
    {
        if (!_cinematicMode)
        {
            //Input
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            var foward = MathF.Abs(horizontal) + MathF.Abs(vertical);
            foward = Math.Clamp(foward, 0f, 1f);
            bool isFoward = foward != 0;
            isDodgeMode = Input.GetKey(KeyCode.LeftShift);
            bool isGrounded = _movement.IsGrounded;
            bool isJumping = Input.GetKeyDown(KeyCode.Space);
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
                if (_playerMenu != null)
                    _playerMenu.GetMovement.ChangeSpeed(15f);//Acelero la velocidad del player
            }
            if (!isDodgeMode && _wasHoldingShift)
            {
                _animation.SetTransforming("transforming", false);
                if (_playerMenu != null)
                    _playerMenu.GetMovement.ChangeSpeed(_playerMenu.GetInitSpeed);
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
            _wasHoldingShift = isDodgeMode;
            _wasInGround = isGrounded;
            _movement.UpdateGroundCheck();
        }
    }
    public void OnfixedUpdate()
    {
        if (!_cinematicMode)
        {
            bool IsBlocked = _movement.IsBlocked();
            if (!IsBlocked)
            {
                _movement.Move(horizontal, vertical);
            }
            else
            {
                _movement.RotateOnly(horizontal, vertical);
            }
        }
    }
    public bool ChangeModeCinematic { get => _cinematicMode; set => _cinematicMode = value; }
}
