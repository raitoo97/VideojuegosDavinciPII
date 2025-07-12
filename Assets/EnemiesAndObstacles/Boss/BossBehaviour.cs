using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField]private List<IBossSkill> _bossSkills = new List<IBossSkill>();
    [Header("TurretSkill")]
    [SerializeField]private List <TurretBoss> _turretBoss;
    [Header("ZombieSkill")]
    [SerializeField]private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    private InvokeZombie _InvokeZombie;
    [Header("PunchSkill")]
    private Punch _punch;
    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        _InvokeZombie = new InvokeZombie(_respawnZombies);
        _bossSkills.Add(_InvokeZombie);
        foreach (var turrets in _turretBoss)
        {
            _bossSkills.Add(turrets);
        }
        foreach (var turrets in _turretBoss)
        {
            turrets.OnStart();
        }
    }
    private void Update()
    {
    }
}
public class InvokeZombie : IBossSkill
{
    private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    public bool canRespawn = true;
    public InvokeZombie(List<RespawnZombie> _respawnZombies)
    {
        this._respawnZombies = _respawnZombies;
    }
    public void BossSkill()
    {
        if (canRespawn)
        {
            if (_respawnZombies.Count <= 0)
            {
                return;
            }
            foreach (var respawn in _respawnZombies)
            {
                respawn.StartWave();
            }
        }
    }
}
[Serializable]
public class TurretBoss : IBossSkill
{
    [SerializeField]private Transform _rayLaser;
    [SerializeField]private Transform _gunSight;
    [SerializeField]private Transform _child;
    [SerializeField]private Material _lineRendererMaterial;
    [SerializeField]private LayerMask _mask;
    [SerializeField]private MonoBehaviour _bossrefCorutine;
    private Vector3 _dirRotVector;
    private Quaternion _dirRotQuaternion;
    private float _shootCooldown;
    private float _fireRate;
    private float _distance;
    private RayCastTurret _rayTurret;
    public bool canShoot;
    public TurretBoss(Transform _rayLaser, Transform _gunSight,Transform _child,Material _lineRendererMaterial, LayerMask _mask,MonoBehaviour _bossrefCorutine)
    {
        this._rayLaser = _rayLaser;
        this._gunSight = _gunSight;
        this._child = _child;
        this._lineRendererMaterial = _lineRendererMaterial;
        this._mask = _mask;
        this._bossrefCorutine = _bossrefCorutine;
    }
    public void OnStart()
    {
        canShoot = false;
        _fireRate = 0.5f;
        _distance = Mathf.Infinity;
        _rayTurret = new RayCastTurret(_rayLaser, _mask, _distance, _lineRendererMaterial,_bossrefCorutine, 2f);
    }
    public void BossSkill()
    {
        if (canShoot)
        {
            if (_child == null || GameManager.instance.player == null) return;
            _dirRotVector = GameManager.instance.player.transform.position - _child.position;
            if (_dirRotVector != Vector3.zero)
            {
                _dirRotQuaternion = Quaternion.LookRotation(_dirRotVector);
                float tripodSpeed = 5;
                _child.rotation = Quaternion.Slerp(_child.rotation, _dirRotQuaternion, tripodSpeed * Time.deltaTime);
            }
            _rayTurret.OnUpdate();
            _shootCooldown -= Time.deltaTime;
            if (_rayTurret.IsEnabled && _shootCooldown <= 0f)
            {
                Shoot();
                _shootCooldown = _fireRate;
            }
        }
        else
        {
            _rayTurret.HiddenLaser();
        }
    }
    private void Shoot()
    {
        var bulletConfig = PoolBullet.instance.bulletConfigs.Find(x => x.type == ShooterType.Enemy);
        if (bulletConfig == null) return;
        var bullet = bulletConfig.GetBullet();
        if (bullet == null) return;
        bullet.transform.position = _gunSight.position;
        bullet.transform.rotation = _gunSight.rotation;
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.EnemyTurretShot);
    }
}
public class Punch : IBossSkill
{
    public void BossSkill()
    {
        
    }
}
