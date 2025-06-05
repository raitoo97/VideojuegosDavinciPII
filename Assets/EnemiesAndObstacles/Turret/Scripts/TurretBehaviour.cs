using System.Collections.Generic;
using UnityEngine;
public class TurretBehaviour : MonoBehaviour , IEnemies
{
    [SerializeField]private Vector3 _dirRotVector;
    [SerializeField]private Quaternion _dirRotQuaternion;
    [SerializeField]private Transform _child;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private float _distance;
    [SerializeField]private List<Transform> _gunSight = new List<Transform>();
    public Material lineRendererMaterial;
    public LayerMask mask;
    private float _shootCooldown;
    [SerializeField] private float _fireRate = 0.5f;
    private void Awake()
    {
        _distance = 50f;
        _child = this.transform.GetChild(0);
        var _tempList = _child.GetComponentsInChildren<Transform>();
        foreach (var x in _tempList)
        {
            if(x != _child.transform)
            {
                _gunSight.Add(x);
            }
        }
    }
    void Start()
    {
        _rayTurret = new RayCastTurret(_child.transform, mask, _distance, lineRendererMaterial,this);
    }
    private void OnEnable()
    {
        Player.TriggerShootInstant += ShootInstan;
    }
    void Update()
    {
        //ActionAtack(); DESACTIVADO PARA TRABAJAR
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
    }
    private void ShootInstan()
    {
        var allTurrets = GameObject.FindObjectsOfType<TurretBehaviour>();
        Transform playerTransform = GameManager.instance.player.transform;
        TurretBehaviour closest = null;
        float minDistance = Mathf.Infinity;
        foreach (var turret in allTurrets)
        {
            float dist = turret.transform.IsMostNearDistance(playerTransform);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = turret;
            }
        }
        if (closest == this)
        {
            Shoot();
            print("disparo");
        }
    }
    private void OnDisable()
    {
        Player.TriggerShootInstant -= ShootInstan;
    }
}
