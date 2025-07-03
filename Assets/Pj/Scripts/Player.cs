using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField]private Transform _groundCheck;
    [SerializeField]private Movement _movement;
    [SerializeField]private ControlPlayer _controller;
    [SerializeField]private Animator _animator;
    [SerializeField]private PlayerAnimation _playerAnimation;
    [SerializeField]private float _initSpeed;
    [SerializeField] private Shield _shield;
    [SerializeField] private DashUlti dashUlti;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody _rb;
    [Header("Life")]
    [SerializeField]private float _maxLife = 100f;
    [SerializeField]private float _currentLife;
    public static Action OnPlayerDeath;
    public static Player instance;
    AudioManager audioManager => AudioManager.instance;//Sound
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _movement = new Movement(_rb, _groundCheck, _initSpeed, groundLayer,this.transform,wallLayer, dashUlti);
        _playerAnimation = new PlayerAnimation(_animator);
        _controller = new ControlPlayer(_movement, _playerAnimation, _shield);
        _currentLife = _maxLife;
    }
    private void OnEnable()
    {
        Bullet.onHitPlayerBullet += HandleHitPlayerBullet;
        ZombieAttack.onHitPlayerZombie += HandleHitPlayerZombie;
        Spikes.OnTriggerSpikes += HandleHitPlayerSpikes;
    }
    private void Update()
    {
        _controller.OnUpdate();
        if (Input.GetKeyDown(KeyCode.G))
        {
            DamagePlayer(10);
        }
    }
    private void FixedUpdate()
    {
        _controller.OnfixedUpdate();
        _movement.OnFixedUpdate();
    }
    public void DamagePlayer(float damage)
    {
        _currentLife -= damage;
        int randomIndex = UnityEngine.Random.Range(0, audioManager.playerDamageSfx.Length);
        audioManager.PlaySfxRandomPitch(audioManager.playerDamageSfx[randomIndex]); //sound effect
        if (_currentLife <= 0f)
        {
            ManagerUI.instance.getLifeBar.CheckLife();
            gameObject.SetActive(false);
            OnPlayerDeath?.Invoke();
        }
    }
    public void HealthPlayer(float healt)
    {
        _currentLife = Mathf.Clamp(_currentLife += healt, 0, _maxLife); 
    }
    private void HandleHitPlayerBullet(Player player,float damage, Transform bulletPos)
    {
        Vector3 knockbackDir = (player.transform.position - bulletPos.position);
        float knockbackForce = 5f;
        ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f), new Vector3(UnityEngine.Random.Range(0f, 180f), 0f, 0f), GameManager.instance.player.transform);
        CameraShakeManager.instance.ShakeCamera(Shakes.EnemyMisilShoot);
        player.GetMovement.ReceiveKnockback(knockbackDir, knockbackForce);
        DamagePlayer(damage);
    }
    private void HandleHitPlayerSpikes(float damage)
    {
        int randomIndex = UnityEngine.Random.Range(0, audioManager.zombieAttackSfx.Length);
        audioManager.PlaySfxRandomPitch(audioManager.zombieAttackSfx[randomIndex]);
        ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f), new Vector3(UnityEngine.Random.Range(0f, 180f), 0f, 0f), GameManager.instance.player.transform);
        CameraShakeManager.instance.ShakeCamera(Shakes.EnemyMisilShoot);
        DamagePlayer(damage);
    }
    private void HandleHitPlayerZombie(Player player, float damage)
    {
        int randomIndex = UnityEngine.Random.Range(0, audioManager.zombieAttackSfx.Length);
        audioManager.PlaySfxRandomPitch(audioManager.zombieAttackSfx[randomIndex]);
        CameraShakeManager.instance.ShakeCamera(Shakes.PlayerUnderAtack);
        ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f), new Vector3(UnityEngine.Random.Range(0f, 180f), 0f, 0f), GameManager.instance.player.transform);
        player.DamagePlayer(damage);
    }
    //private void HandleHitShield(){}
    //public void DamageShield(float){}
    public float GetLife { get => Mathf.Clamp(_currentLife, 0, _maxLife); }
    private void OnDisable()
    {
        Bullet.onHitPlayerBullet -= HandleHitPlayerBullet;
        ZombieAttack.onHitPlayerZombie -= HandleHitPlayerZombie;
    }
    private void OnDrawGizmos()
    {
        if(GetMovement != null)
        {
            GetMovement.OnDraw();
        }
    }
    public Movement GetMovement { get => _movement; }
    public ControlPlayer GetController {get => _controller;}
    public float GetInitSpeed { get => _initSpeed; }
}
