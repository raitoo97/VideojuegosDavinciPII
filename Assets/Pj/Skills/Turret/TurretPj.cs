using System.Collections;
using UnityEngine;
public class TurretPj : MonoBehaviour
{
    private Collider[] _colliders;
    public LayerMask mask;
    public float nearEnemy;
    public GameObject enemy;
    public Vector3 rotVector;
    public Transform turretChild;
    public Transform gunSight;
    public GameObject turret;
    private bool _detectedTarget;
    private Coroutine _shootRoutine;
    [Header("Audio/effects")]
    [SerializeField] private AudioClip shotSfx;
    private Coroutine _recoilCorutine;
    public Transform recoilPoint;
    private void Awake()
    {
        turretChild.localRotation = Quaternion.identity;
    }
    private void Start()
    {
        ActivateSelf();
    }
    void Update()
    {
        GetZombie();
        RotateTorrete(rotVector);
        RotateArroundDetail();
        if (Input.GetKeyDown(KeyCode.P))
        {
            ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed);
            if (_shootRoutine != null)
            {
                StopCoroutine(_shootRoutine);
            }
            _shootRoutine = StartCoroutine(Shoot());
        }
    }
    #region
    private Vector3 GetZombie()
    {
        float visionRange = ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
        _colliders = Physics.OverlapSphere(this.transform.position, visionRange, mask);
        nearEnemy = Mathf.Infinity;
        GameObject closestZombie = null;
        Vector3 closestPosition = rotVector;
        foreach (Collider collider in _colliders)
        {
            float dist = this.transform.IsMostNearDistance(collider.transform);
            if (dist < nearEnemy)
            {
                nearEnemy = dist;
                closestZombie = collider.gameObject;
                closestPosition = closestZombie.transform.position;
            }
        }
        enemy = closestZombie;
        _detectedTarget = closestZombie != null;
        if (closestZombie != null)
        {
            rotVector = closestPosition;
        }
        return rotVector;
    }
    private void RotateTorrete(Vector3 rotVector)
    {
        if (enemy == null) return;

        Vector3 direction = rotVector - turretChild.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turretChild.rotation = Quaternion.Lerp(turretChild.rotation, targetRotation, Time.deltaTime * 25f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 13f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 16f);
    }
    #endregion
    IEnumerator Shoot()
    {
        while (true)
        {
            if (_detectedTarget && enemy != null)
            {
                var animZombieRef = enemy.GetComponentInParent<ZombieAnimations>();
                if (animZombieRef != null)
                {
                    if (animZombieRef.getStateZombie != STATE.Death)
                    {
                        var bullet = PoolBullet.instance.bulletConfigs.Find(x => x.type == ShooterType.Player).GetBullet();
                        if (bullet != null)
                        {
                            bullet.transform.position = gunSight.position;
                            bullet.transform.rotation = gunSight.rotation;
                            AudioManager.instance.PlaySfxRandomPitch(shotSfx);
                            if (_recoilCorutine == null)
                                _recoilCorutine = StartCoroutine(RecoilTorret());
                            CameraShakeManager.instance.ShakeCamera(Shakes.MisilShoot);
                        }
                    }
                }
                if (enemy.GetComponent<TurretBehaviour>())
                {
                    var bullet = PoolBullet.instance.bulletConfigs.Find(x => x.type == ShooterType.Player).GetBullet();
                    if (bullet != null)
                    {
                        bullet.transform.position = gunSight.position;
                        bullet.transform.rotation = gunSight.rotation;
                        AudioManager.instance.PlaySfxRandomPitch(shotSfx);
                        if (_recoilCorutine == null)
                            _recoilCorutine = StartCoroutine(RecoilTorret());
                        CameraShakeManager.instance.ShakeCamera(Shakes.MisilShoot);
                    }
                }
            }
            yield return new WaitForSeconds(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed));
        }
    }
    public void RotateArroundDetail()
    {
        turret.transform.RotateAround(this.transform.position, Vector3.up * -1, 50 * Time.deltaTime);
    }
    public void DesactivateSelf()
    {
        if (turret == null) return;
        turret.gameObject.SetActive(false);
        if (_shootRoutine != null)
        {
            StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }
    public void ActivateSelf()
    {
        if (turret == null) return;
        turret.gameObject.SetActive(true);
        if (_shootRoutine == null)
        {
            _shootRoutine = StartCoroutine(Shoot());
        }
    }
    IEnumerator RecoilTorret()
    {
        Vector3 _originalPos = turretChild.transform.localPosition;
        Quaternion _orginialRot = turretChild.transform.localRotation;
        Vector3 _recoilDir = turretChild.transform.parent.InverseTransformDirection(-recoilPoint.forward).normalized;
        Vector3 _recoilPos = _originalPos + _recoilDir * 3f;
        Quaternion _recoilRot = _orginialRot * Quaternion.Euler(-5, 0, 0);
        float time = 0f;
        float recoilTime = 0.04f;
        float returnTime = 0.12f;
        while (time <= recoilTime)
        {
            time += Time.deltaTime;
            turretChild.transform.localPosition = Vector3.Lerp(_originalPos, _recoilPos, time / recoilTime);
            turretChild.transform.localRotation = Quaternion.Slerp(_orginialRot, _recoilRot, time / recoilTime);
            yield return null;
        }
        time = 0f;
        while (time <= returnTime)
        {
            time += Time.deltaTime;
            turretChild.transform.localPosition = Vector3.Lerp(_recoilPos, _originalPos, time / returnTime);
            turretChild.transform.localRotation = Quaternion.Slerp(_recoilRot, _orginialRot, time / returnTime);
            yield return null;
        }
        turretChild.transform.localPosition = _originalPos;
        turretChild.transform.localRotation = _orginialRot;
        _recoilCorutine = null;
    }
}
