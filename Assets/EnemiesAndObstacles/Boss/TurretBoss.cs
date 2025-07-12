using UnityEngine;
public class TurretBoss : MonoBehaviour
{
    [SerializeField]private Transform _rayLaser;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private Transform _gunSight;
    private Transform _child;
    private Vector3 _dirRotVector;
    private Quaternion _dirRotQuaternion;
    public Material lineRendererMaterial;
    public LayerMask mask;
    private float _shootCooldown;
    private float _distance;
    private float _fireRate = 0.5f;
    private void Awake()
    {
        _distance = Mathf.Infinity;
        _child = this.transform.GetChild(0);
    }
    private void Start()
    {
        _rayTurret = new RayCastTurret(_rayLaser, mask, _distance, lineRendererMaterial, this);
    }
    void Update()
    {
        ActionAtack();
    }
    public void ActionAtack()
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
