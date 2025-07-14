using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossBehaviour : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _navMeshAgent;
    [SerializeField]private Animator _animator;
    [SerializeField]private List<IBossSkill> _bossSkills = new List<IBossSkill>();
    [Header("TurretSkill")]
    [SerializeField]private List <TurretBoss> _turretBoss;
    [Header("ZombieSkill")]
    [SerializeField]private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    private InvokeZombie _InvokeZombie;
    [Header("PunchSkill")]
    private Punch _punch;
    [SerializeField]private BossMeleAtack _meleAtack;
    [Header("Random Habilities")]
    private float _turretSkillDuration = 5f;
    private float _zombieSkillDuration = 6f;
    private float _delayBetweenSkills = 5f;
    [SerializeField]private float _specialSkillTimer = 0f;
    private bool _isUsingSpecialSkill = false;
    private IBossSkill _currentSpecialSkill;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _meleAtack = GetComponentInChildren<BossMeleAtack>();
        _animator = GetComponentInChildren<Animator>();
        _punch = new Punch(_navMeshAgent,ActivatePunchCollider,this.transform, _animator);
        _InvokeZombie = new InvokeZombie(_respawnZombies, _animator, _navMeshAgent);
    }
    void Start()
    {
        _bossSkills.Add(_InvokeZombie);
        _bossSkills.Add(_punch);
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
        if (!_isUsingSpecialSkill)
        {
            _punch.BossSkill();
            _specialSkillTimer -= Time.deltaTime;
            if (_specialSkillTimer <= 0f)
            {
                TryActivateRandomSkill();
            }
        }
        else
        {
            if (_currentSpecialSkill != null)
            {
                _currentSpecialSkill.BossSkill();
            }
            _specialSkillTimer -= Time.deltaTime;
            if (_specialSkillTimer <= 0f)
            {
                EndSpecialSkill();
            }
        }
    }
    private void TryActivateRandomSkill()
    {
        int choice = UnityEngine.Random.Range(0, 100);
        print(choice);
        if (choice < 30)
        {
            if (_turretBoss.Count > 0)
            {
                _currentSpecialSkill = new MultipleTurretSkill(_turretBoss);
                _isUsingSpecialSkill = true;
                _specialSkillTimer = _turretSkillDuration;
                _navMeshAgent.isStopped = true;
            }
        }
        else if (choice < 50)
        {
            _currentSpecialSkill = _InvokeZombie;
            _currentSpecialSkill.BossSkill();
            _isUsingSpecialSkill = true;
            _specialSkillTimer = _zombieSkillDuration;
            _navMeshAgent.isStopped = true;
        }
        else
        {
            _isUsingSpecialSkill = false;
            _specialSkillTimer = _delayBetweenSkills;
        }
    }
    private void EndSpecialSkill()
    {
        _isUsingSpecialSkill = false;
        if (_currentSpecialSkill is MultipleTurretSkill turretSkill)
        {
            turretSkill.EndSkill();
        }
        _currentSpecialSkill = null;
        _navMeshAgent.isStopped = false;
    }
    #region//InvokeZombie
    private void InvokeZombies()
    {
        _InvokeZombie.SpawnZombies();
        EndSpecialSkill();
    }
    #endregion
    #region//Punch
    private void ActivatePunchCollider()
    {
        StartCoroutine(PunchColliderWindow());
    }
    private IEnumerator PunchColliderWindow()
    {
        _meleAtack.EnablePunchCollider();         
        yield return new WaitForSeconds(0.1f);
        _meleAtack.DisablePunchCollider();
    }
    #endregion
}
public class InvokeZombie : IBossSkill
{
    private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private bool _isInvoking = false;
    public InvokeZombie(List<RespawnZombie> _respawnZombies, Animator _animator, NavMeshAgent _navMeshAgent)
    {
        this._respawnZombies = _respawnZombies;
        this._animator = _animator;
        this._navMeshAgent = _navMeshAgent;
    }
    public void BossSkill()
    {
        if (_isInvoking || _respawnZombies.Count == 0) return;
        _isInvoking = true;
        _navMeshAgent.isStopped = true;
        _animator.SetBool("Run", false);
        _animator.SetTrigger("Invoke");
    }
    public void SpawnZombies()
    {
        if (!_isInvoking) return;
        foreach (var respawn in _respawnZombies)
        {
            respawn.StartWave();
        }
        _navMeshAgent.isStopped = false;
        _animator.SetBool("Run", true);
        _isInvoking = false;
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
    [SerializeField]private Animator _animator;
    [SerializeField]private NavMeshAgent _agent;
    private Vector3 _dirRotVector;
    private Quaternion _dirRotQuaternion;
    private float _shootCooldown;
    private float _fireRate;
    private float _distance;
    private RayCastTurret _rayTurret;
    private bool canShoot;
    public TurretBoss(Transform _rayLaser, Transform _gunSight, Transform _child, Material _lineRendererMaterial, LayerMask _mask, MonoBehaviour _bossrefCorutine, Animator _animator, NavMeshAgent _agent)
    {
        this._rayLaser = _rayLaser;
        this._gunSight = _gunSight;
        this._child = _child;
        this._lineRendererMaterial = _lineRendererMaterial;
        this._mask = _mask;
        this._bossrefCorutine = _bossrefCorutine;
        this._animator = _animator;
        this._agent = _agent;
    }
    public void OnStart()
    {
        canShoot = true;
        _fireRate = 0.5f;
        _distance = Mathf.Infinity;
        _rayTurret = new RayCastTurret(_rayLaser, _mask, _distance, _lineRendererMaterial,_bossrefCorutine, 2f);
    }
    public void BossSkill()
    {
        if (!canShoot) return;
        if (_child == null || GameManager.instance.player == null) return;
        _dirRotVector = GameManager.instance.player.transform.position - _child.position;
        _agent.isStopped = true;
        _animator.SetBool("Run", false);
        _animator.SetTrigger("Turret");
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
    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }
    public void EndSkill()
    {
        _rayTurret.HiddenLaser();
        _animator.SetBool("Run", true);
        canShoot = false;
    }
}
public class MultipleTurretSkill : IBossSkill
{
    private List<TurretBoss> _turrets;
    public MultipleTurretSkill(List<TurretBoss> turrets)
    {
        _turrets = turrets;
    }
    public void BossSkill()
    {
        foreach (var turret in _turrets)
        {
            turret.SetCanShoot(true);
            turret.BossSkill();
        }
    }
    public void EndSkill()
    {
        foreach (var turret in _turrets)
        {
            turret.EndSkill();
        }
    }
}
public class Punch : IBossSkill
{
    private float _attackRange = 4f;
    private float _cooldown = 0.2f;
    private float _cooldownTimer = 0f;
    private Transform _transform;
    private NavMeshAgent _agent;
    private Action _onPunch;
    private Animator _animator;
    public Punch(NavMeshAgent _agent, Action _onPunch,Transform _transform,Animator _animator)
    {
        this._agent = _agent;
        this._onPunch = _onPunch;
        this._transform = _transform;
        this._animator = _animator;
    }
    public void BossSkill()
    {
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
        bool distance = _transform.IsWithinDistanceOf(GameManager.instance.player.transform, _attackRange);
        if (!distance)
        {
            _animator.SetBool("Run", true);
            _agent.isStopped = false;
            _agent.SetDestination(GameManager.instance.player.transform.position);
        }
        else
        {
            _animator.SetBool("Run", false);
            _animator.SetTrigger("Punch");
            _agent.isStopped = true;
            if (_cooldownTimer <= 0f)
            {
                _onPunch?.Invoke();
                _cooldownTimer = _cooldown;
            }
        }
    }
}
