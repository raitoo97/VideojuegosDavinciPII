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
    //Constructor
    public Movement(Rigidbody rb, Transform _groundCheck, float speed, LayerMask groundLayer, Transform _transformPj, LayerMask _layerWall)
    {
        _speed = speed;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        this._transformPj = _transformPj;
        this._layerWall = _layerWall;
        _rb = rb;
      
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
        Vector3 dashDirection = LastMoveDirection;
        if (dashDirection == Vector3.zero )
            dashDirection = _rb.transform.forward; // fallback por si no se mueve
            _rb.AddForce(dashDirection.normalized * ManagerSkills.instance.GetValueSkill(SkillCategory.dashCategory, SkillStatType.dashSpeed), ForceMode.Impulse);
            int randomIndex = Random.Range(0, AudioManager.instance.skillPlayerDash.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.skillPlayerDash[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Dash, new Vector3(0f, 0f, 0f), Vector3.zero, _rb.transform);
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

