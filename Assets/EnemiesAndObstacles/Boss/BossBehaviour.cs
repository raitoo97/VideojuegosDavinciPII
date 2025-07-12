using System.Collections.Generic;
using UnityEngine;
public class BossBehaviour : MonoBehaviour
{
    private List<IBossSkill> _bossSkills = new List<IBossSkill>();
    [Header("TurretSkill")]
    [SerializeField]private Transform _rayLaser;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private Transform _gunSight;
    public Material lineRendererMaterial;
    public LayerMask mask;
    [Header("ZombieSkill")]
    [SerializeField]private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    private InvokeZombie _InvokeZombie;
    void Start()
    {
        _InvokeZombie = new InvokeZombie(_respawnZombies);
        _bossSkills.Add(_InvokeZombie);
    }
    public void ZombieWave()
    {
        foreach(var skills in _bossSkills)
        {
            skills.BossSkill();
        }
    }
}
public class InvokeZombie : IBossSkill
{
    private List<RespawnZombie> _respawnZombies = new List<RespawnZombie>();
    public InvokeZombie(List<RespawnZombie> _respawnZombies)
    {
        this._respawnZombies = _respawnZombies;
    }
    public void BossSkill()
    {
        //if (_respawnZombies.Count <= 0)
        //{
        //    return;
        //}
        //foreach (var respawn in _respawnZombies)
        //{
        //    respawn.StartWave();
        //}
    }
}
public class TurretBoss : IBossSkill
{
    [SerializeField]private Transform _rayLaser;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private Transform _gunSight;
    private Transform _child;
    private Vector3 _dirRotVector;
    private Quaternion _dirRotQuaternion;
    private Material lineRendererMaterial;
    private LayerMask mask;
    private float _shootCooldown;
    private float _distance;
    private float _fireRate = 0.5f;
    private MonoBehaviour _bossrefCorutine;
    private void Awake()
    {
        _distance = Mathf.Infinity;
    }
    private void Start()
    {
        _rayTurret = new RayCastTurret(_rayLaser, mask, _distance, lineRendererMaterial,_bossrefCorutine, .5f);
    }
    public void BossSkill()
    {
        if (_child == null || GameManager.instance.player == null) return;
        _dirRotVector = GameManager.instance.player.transform.position - _child.position;
        if (_dirRotVector != Vector3.zero)
        {
            _dirRotQuaternion = Quaternion.LookRotation(_dirRotVector);
            float tripodSpeed = GameManager.instance.player.GetComponent<Player>().GetInitSpeed * 2.5f;
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
}
