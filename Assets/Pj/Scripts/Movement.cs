using Unity.VisualScripting;
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
    public GameObject shield = Player.instance.transform.Find("Shield").gameObject;

    //Jump

    //Constructor
    public Movement(Rigidbody rb, Transform _groundCheck, float speed, LayerMask groundLayer)
    {
        _speed = speed;
        _groundLayer = groundLayer;
        this._groundCheck = _groundCheck;
        _rb = rb;
      
    }
    public void Move(float inputHorizontal, float inputVertical)
    {
        Vector2 _directionVector = new Vector2(inputHorizontal, inputVertical);
        Vector3 _dir = new Vector3(_directionVector.x, 0, _directionVector.y).normalized;
        if (_dir.sqrMagnitude > 0.001f)
        {
            Quaternion rot = Quaternion.LookRotation(_dir);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 10 * Time.fixedDeltaTime));
            Vector3 newPosition = _rb.position + _dir * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }
    }
    public void Jump(float impulse)
    {
        if (_rb != null)
        {
            _rb.AddForce(Vector3.up * impulse, ForceMode.Impulse);
            Player.TriggerShootInstant?.Invoke();
        }
    }
    public void Dash(float impulse)
    {
        
        Vector3 dashDirection = LastMoveDirection;
        if (dashDirection == Vector3.zero )
            dashDirection = _rb.transform.forward; // fallback por si no se mueve
            
            
        
            _rb.AddForce(dashDirection.normalized * impulse, ForceMode.Impulse);

            int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.skillPlayerDash.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.skillPlayerDash[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Dash, new Vector3(0f, 0f, 0f), Vector3.zero, _rb.transform);
        
    }
    public void StopDash()
    {
        _rb.velocity = Vector2.zero;
    }
    public void ActivateShield()
    {
        
        shield.SetActive(true);
        ParticlesPool.instance.SpamParticle(ParticleType.Shield, new Vector3(0f, 0f, 0f), Vector3.zero, _rb.transform);

    }
    public void DeactivateShield()
    {
       
        shield.SetActive(false);
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
    public float GetSpeed { get => _speed; }
    public bool IsGrounded => _isGrounded;
    public float CurrentSpeed { get; private set; }
}

/*
[SerializeField]private float radius = 7.0f;             
[SerializeField]private float power = 800.0f;            
[SerializeField]private float slowDuration = 1.8f;       
[SerializeField]private float timeLow = 0.1f;            
[SerializeField]private float timenormal = 1f;
[SerializeField] private float originalFixedDeltaTime;
void Start()
{
    originalFixedDeltaTime = Time.fixedDeltaTime;
}
private void OnDrawGizmos()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, radius);
}
void Update()
{
    if (Input.GetKeyDown(KeyCode.U))
    {
        StartCoroutine(CorrtuineTime());
    }
}
IEnumerator CorrtuineTime()
{
    Vector3 explosionPos = transform.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
        Rigidbody rb = hit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(power, explosionPos, radius, 3f,ForceMode.Impulse);
        }
    }
    Time.timeScale = timeLow;
    float t = 0f;
    while (t <= slowDuration)
    {
        t += Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Lerp(timeLow, timenormal, t / slowDuration);
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        print(Time.fixedDeltaTime);
        yield return null;
    }*/