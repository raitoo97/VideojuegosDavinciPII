using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Transform _groundCheck;
    [SerializeField] Movement _movement;
    [SerializeField] ControlPlayer _controller;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerAnimation _playerAnimation;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] float _initSpeed = 3f;
    [Header("Life")]
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentLife;
    public static Action OnPlayerDeath;
    public static Action TriggerShootInstant;
    //Sound
    AudioManager audioManager => AudioManager.instance;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _movement = new Movement(transform, _groundCheck, _initSpeed, groundLayer, _camera);
        _playerAnimation = new PlayerAnimation(_animator);
        _controller = new ControlPlayer(_movement, _playerAnimation);
        _currentLife = _maxLife;
    }
    private void OnEnable()
    {
        Bullet.onHitPlayerBullet += HandleHitPlayerBullet;
        ZombieAttack.onHitPlayerZombie += HandleHitPlayerZombie;
    }
    private void Update()
    {
        _controller.OnUpdate();
        if (Input.GetKeyDown(KeyCode.G))
        {
            HealthPlayer(10);
        }
        print(_currentLife);
    }
    private void FixedUpdate()
    {
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
    private void HandleHitPlayerZombie(Player player, float damage)
    {
        int randomIndex = UnityEngine.Random.Range(0, audioManager.zombieAttackSfx.Length);
        audioManager.PlaySfxRandomPitch(audioManager.zombieAttackSfx[randomIndex]);
        CameraShakeManager.instance.ShakeCamera(Shakes.PlayerUnderAtack);
        ParticlesPool.instance.SpamParticle(ParticleType.Sparks, new Vector3(0f, 2f, 0f), new Vector3(UnityEngine.Random.Range(0f, 180f), 0f, 0f), GameManager.instance.player.transform);
        player.DamagePlayer(damage);
    }
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
    public float GetInitSpeed { get => _initSpeed; }
}
