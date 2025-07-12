using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretBehaviour : MonoBehaviour , IEnemies
{
    [SerializeField]private Vector3 _dirRotVector;
    [SerializeField]private Quaternion _dirRotQuaternion;
    [SerializeField]private Transform _child;
    [SerializeField]private Transform _rayLaser;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private float _distance;
    [SerializeField]private List<Transform> _gunSight = new List<Transform>();
    public Material lineRendererMaterial;
    public LayerMask mask;
    private float _shootCooldown;
    [SerializeField]private float _fireRate = 0.5f;
    public event System.Action<IEnemies> OnDeath;
    public event System.Action<IEnemies> _substractEnemyFromWave;
    private float _enemypoints;
    [SerializeField]private Collider _collider;
    [SerializeField]private float _life;
    [SerializeField]private Animator animator;
    private void Awake()
    {
        _life = 200;
        _enemypoints = 60;
        _distance = Mathf.Infinity;
        _child = this.transform.GetChild(0);
        var _tempList = _child.GetComponentsInChildren<Transform>();
        foreach (var x in _tempList)
        {
            if(x != _child.transform)
            {
                _gunSight.Add(x);
            }
        }
        _collider = this.GetComponent<Collider>();
        _collider.enabled = true;
    }
    void Start()
    {
        _rayTurret = new RayCastTurret(_rayLaser, mask, _distance, lineRendererMaterial,this);
        animator = _child.GetComponent<Animator>();
        animator.enabled = false;
        Bullet.OnTurretDamaged += TakeDamage;
        OnDeath += Death;
        StartCoroutine(WaitForSuscription());
    }
    void Update()
    {
        ActionAtack();
    }
    public void ActionAtack()
    {
        if (_child == null || GameManager.instance.player == null) return;
        _dirRotVector = GameManager.instance.player.transform.position - this.transform.position;
        _dirRotQuaternion = Quaternion.LookRotation(_dirRotVector);
        float tripodSpeed = GameManager.instance.player.GetComponent<Player>().GetInitSpeed * 2.5f;
        _child.transform.rotation = Quaternion.Slerp(_child.transform.rotation, _dirRotQuaternion, tripodSpeed * Time.deltaTime);
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
        var _randomGunSight = _gunSight[Random.Range(0, _gunSight.Count)];
        bullet.transform.position = _randomGunSight.position;
        bullet.transform.rotation = _randomGunSight.rotation;
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.EnemyTurretShot);
    }
    public void TakeDamage(TurretBehaviour turret,float dmg)
    {
        if (turret != this) return;
        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.turretCategory))
        {
            int randomIndexUltimate = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndexUltimate]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.TurretUltimate, new Vector3(0f, 2f, 0f), Vector3.zero, turret.transform);
            _life -= dmg;
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Explosion, new Vector3(0f, 2f, 0f), Vector3.zero, turret.transform);
            _life -= dmg;

        }
        if (_life <= 0)
        {
            OnDeath?.Invoke(this);
            _substractEnemyFromWave?.Invoke(this);
        }
    }
    public void Death(IEnemies enemy)
    {
        StartCoroutine(DeathCorutine());
    }
    private void OnDestroy()
    {
        Bullet.OnTurretDamaged -= TakeDamage;
        PointManager.instance.GetHandle.EnemyDesSuscribeEvent(this);
        WavesManager.instance.EnemyDesuscribeEventToWaveSubstract(this);
    }
    public float GetPointValue()
    {
        return _enemypoints;
    }
    IEnumerator WaitForSuscription()
    {
        yield return new WaitForEndOfFrame();
        PointManager.instance.GetHandle.EnemySuscribeEvent(this);
        WavesManager.instance.EnemySuscribeEventToWaveSubstract(this);
        yield return new WaitForEndOfFrame();
        this.gameObject.SetActive(false);
    }
    IEnumerator DeathCorutine()
    {
        animator.enabled = true;
        animator.SetBool("IsDeath", true);
        _collider.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
    public int SubstractFromWave()
    {
        return 1;
    }
    public int ReturnThisTorret()
    {
        return 1;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
}
