using System.Collections;
using UnityEngine;
public class TurretPj : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    public LayerMask mask;
    public float nearEnemy;
    public GameObject enemy;
    public Vector3 rotVector;
    public Transform turretChild;
    public Transform gunSight;
    public GameObject turret;
    private bool _detectedTarget;
    private Coroutine _shootRoutine;
    private bool _enemyWasDestroyed;
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
        colliders = Physics.OverlapSphere(this.transform.position, visionRange, mask);
        nearEnemy = Mathf.Infinity;
        GameObject closestZombie = null;
        Vector3 closestPosition = rotVector;
        foreach (Collider collider in colliders)
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
        if(enemy != null)
        {
            if (enemy.TryGetComponent<ZombieAnimations>(out var animZombieRef))
            {
                _enemyWasDestroyed = animZombieRef.getStateZombie == STATE.Death;
            }
        }
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
            if (_detectedTarget && !_enemyWasDestroyed)
            {
                var bullet = PoolBullet.instance.bulletConfigs.Find(x => x.type == ShooterType.Player).GetBullet();
                if (bullet == null) break;
                var _gunSight = gunSight;
                bullet.transform.position = _gunSight.position;
                bullet.transform.rotation = _gunSight.rotation;
            }
            yield return new WaitForSeconds(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed));
        }
    }
    public void RotateArroundDetail()
    {
        turret.transform.RotateAround(this.transform.position, Vector3.up, 50 * Time.deltaTime);
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
