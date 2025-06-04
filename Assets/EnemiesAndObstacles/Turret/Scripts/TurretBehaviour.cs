using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretBehaviour : MonoBehaviour , IEnemies
{
    [SerializeField]private Vector3 _dirRotVector;
    [SerializeField]private Quaternion _dirRotQuaternion;
    [SerializeField]private Transform _child;
    [SerializeField]private RayCastTurret _rayTurret;
    [SerializeField]private float _distance;
    [SerializeField]private float _rotationSpeed;
    [SerializeField]private bool _isShooting;
    [SerializeField]private List<Transform> _gunSight = new List<Transform>();
    public Material lineRendererMaterial;
    public LayerMask mask;
    private void Awake()
    {
        _rotationSpeed = 1.0f;
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
    void Update()
    {
        if (_child == null || GameManager.instance.player == null) return;
        _dirRotVector = GameManager.instance.player.transform.position - this.transform.position;
        _dirRotQuaternion = Quaternion.LookRotation(_dirRotVector);
        _child.transform.rotation = Quaternion.Slerp(_child.transform.rotation, _dirRotQuaternion, _rotationSpeed * Time.deltaTime);
        _rayTurret.OnUpdate();
        if(_rayTurret.IsEnabled && !_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        _isShooting = true;
        while (_rayTurret.IsEnabled)
        {
            var bullet = PoolBullet.instance.bulletConfigs.Find(x => x.type == ShooterType.Enemy).GetBullet();
            if (bullet == null) break;
            var _randomGunSight = _gunSight[Random.Range(0, _gunSight.Count)];
            bullet.transform.position = _randomGunSight.position;
            bullet.transform.rotation = _randomGunSight.rotation;
            print(bullet);
            yield return new WaitForSeconds(0.02f);
        }
        _isShooting = false;
    }
}
