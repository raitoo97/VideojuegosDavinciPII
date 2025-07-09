using UnityEngine;
using System;
public class ControlPlayer
{
    private Movement _movement;
    private PlayerAnimation _animation;
    private Shield _shield;
    private bool _wasHoldingShift = false;
    private bool _wasInGround;
    private float horizontal;
    private float vertical;
    private bool isDodgeMode;
    // Dash
    private bool unlockedDash;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashCooldown;
    private float dashCooldownTimer = 0f;
    private float dashDuration = 0.2f;
    private float dashTimer = 0f;
    //private float dashImpulse = 40f;
    //Shield
    private bool canShield = true;
    private bool isShielding = false;
    private float radius;
    public bool unlockedShield = false;
    float shieldCooldown;
    private float shieldDuration;
    private float shieldCooldownTimer = 0f;
    private float shieldTimer = 0f;
    public ControlPlayer(Movement m, PlayerAnimation a, Shield s)
    {
        _movement = m;
        _animation = a;
        _shield = s;
    }
    public void OnUpdate()
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
        bool isDash = Input.GetKeyDown(KeyCode.Mouse0); 
        bool isShield = Input.GetKeyDown(KeyCode.Mouse1);
        // DASH UNLOCK & LEVEL UP
        unlockedDash = ManagerSkills.instance.IsUnlocked(SkillCategory.dashCategory);
        dashCooldown = ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory,SkillStatType.dashCooldown);
        //SHIELD UNLOCK & LEVEL UP
        unlockedShield = ManagerSkills.instance.IsUnlocked(SkillCategory.shieldCategory);
        shieldCooldown = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldCooldown);
        radius = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldRadius);
        shieldDuration = ManagerSkills.instance.GetValueSkill(SkillCategory.shieldCategory, SkillStatType.shieldDuration);
        _shield.radius = radius;
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
        if (isDash && unlockedDash && canDash && isDodgeMode && isGrounded)
        {
            //_animation.SetDodge("dodging", foward * 3);
            _movement.Dash();
            isDashing = true;
            canDash = false;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                _movement.StopDash();
                isDashing = false;
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
        //SHIELD
        if (unlockedShield && isShield && canShield)
        {
            _shield.canShield = true;
            _shield.ActivateShield();
            isShielding = true;
            canShield = false;
            shieldTimer = shieldDuration;
            shieldCooldownTimer = shieldCooldown;
        }
        if (isShielding)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                _shield.DeactivateShield();
                isShielding = false;
            }
        }
        if (!canShield)
        {
            shieldCooldownTimer -= Time.deltaTime;
            if (shieldCooldownTimer <= 0)
            {
                canShield = false;
                canShield = true;
            }
        }
        //      ======== MOVIMIENTO FÍSICO Y ESTADO ========
        _wasHoldingShift = isDodgeMode;
        _wasInGround = isGrounded;
        _movement.UpdateGroundCheck();
    }
    public void OnfixedUpdate()
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
    public bool GetDodgeMode { get => isDodgeMode; }
}
