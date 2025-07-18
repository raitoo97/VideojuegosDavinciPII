using UnityEngine;
public class Movement
{
    public Vector3 LastMoveDirection { get; private set; }
    private float _speed;
    private Transform _groundCheck;
    private bool _isGrounded;
    private LayerMask _groundLayer;
    private Rigidbody _rb;
    private Vector3 _pendingKnockback;
    private bool _applyKnockback;
    private Transform _transformPj;
    private LayerMask _layerWall;
    private DashUlti _dashUlti;
    public bool _isInBossFight = false;
    private Transform _bossTransform;
    private Transform _cameraTransform;
    public Movement(Rigidbody rb, Transform _groundCheck, float speed, LayerMask groundLayer, Transform _transformPj, LayerMask _layerWall, DashUlti dashUlti,Transform _bossTransform, Transform _cameraTransform)
    {
        _speed = speed;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        this._transformPj = _transformPj;
        this._layerWall = _layerWall;
        _rb = rb;
        _dashUlti = dashUlti;
        this._bossTransform = _bossTransform;
        this._cameraTransform = _cameraTransform;
    }
    public void SetBossFightMode(bool isActive)
    {
        _isInBossFight = isActive;
    }
    public void MoveInBossFight(float inputHorizontal, float inputVertical)
    {
        if (_bossTransform == null || _cameraTransform == null) return;
        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 _dir = (camForward * inputVertical + camRight * inputHorizontal).normalized;
        if (_dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_dir);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
        }
        else
        {
            Vector3 lookDir = (_bossTransform.position - _rb.position);
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion rot = Quaternion.LookRotation(lookDir);
                _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
            }
        }
        Vector3 newPosition = _rb.position + _dir * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    public void RotateOnlyInBossFight(float inputHorizontal, float inputVertical)
    {
        if (_bossTransform == null || _cameraTransform == null) return;
        Vector3 camForward = _cameraTransform.forward;
        Vector3 camRight = _cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 _dir = (camForward * inputVertical + camRight * inputHorizontal).normalized;
        if (_dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_dir);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
        }
        else
        {
            Vector3 lookDir = (_bossTransform.position - _rb.position);
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion rot = Quaternion.LookRotation(lookDir);
                _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
            }
        }
    }
    public void Move(float inputHorizontal, float inputVertical)
    {
        Vector3 _dir = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        if (_dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_dir);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
        }
        Vector3 newPosition = _rb.position + _dir * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(newPosition);
    }
    public void RotateOnly(float inputHorizontal, float inputVertical)
    {
        Vector3 _dirRot = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        if(_dirRot.magnitude > 0.001f)
        {
            Quaternion _rotDir = Quaternion.LookRotation(_dirRot);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, _rotDir,10 * Time.fixedDeltaTime));
        }
    }
    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
        }
    }
    public void Dash()
    {
        Vector3 startPosition = _rb.position;
        Vector3 dashDirection = LastMoveDirection;
        if (dashDirection == Vector3.zero)
        dashDirection = _rb.transform.forward; 
        float dashForce = ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed);
        _rb.AddForce(dashDirection.normalized * dashForce, ForceMode.Impulse);
            

            int randomIndex = Random.Range(0, AudioManager.instance.skillPlayerDash.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.skillPlayerDash[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Dash, new Vector3(0f, 0f, 0f), Vector3.zero, _rb.transform);

        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.dashCategory))
        {
            Vector3 endPosition = _rb.position + dashDirection.normalized * dashForce * 0.2f;
            _dashUlti.CreateDashTrail(startPosition,endPosition);
        }
    }
    public void StopDash()
    {
        _rb.velocity = Vector2.zero;
    }
    public void UpdateGroundCheck() 
    {
        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        _isGrounded = Physics.CheckSphere(origin, radius, _groundLayer);
    }
    public void OnDraw()
    {
        Vector3 origin = _groundCheck.position;
        float radius = 0.25f;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_transformPj.position + _transformPj.up + _transformPj.forward, 0.5f);
    }
    public void ReceiveKnockback(Vector3 direction, float force)
    {
        if (_applyKnockback) return;
        _pendingKnockback = direction.normalized * force;
        _applyKnockback = true;
    }
    public void ChangeSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    public void OnFixedUpdate()
    {
        if (_applyKnockback)
        {
            _rb.AddForce(_pendingKnockback, ForceMode.Impulse);
            _applyKnockback = false;
        }
    }
    public bool IsBlocked(float radius = 0.5f)
    {
        return Physics.CheckSphere(_transformPj.position + _transformPj.up + _transformPj.forward, radius, _layerWall);
    }
    public float GetSpeed { get => _speed; }
    public bool IsGrounded => _isGrounded;
    public float CurrentSpeed { get; private set; }
}

