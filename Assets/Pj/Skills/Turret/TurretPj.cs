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
    private void Start()
    {
        turretChild.localRotation = Quaternion.identity;
        _shootRoutine = StartCoroutine(Shoot());
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
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed));
    }
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
}
