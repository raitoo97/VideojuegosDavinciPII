using System.Collections;
using UnityEngine;
public class TurretPj : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    public LayerMask mask;
    public float nearZombie;
    public GameObject zombie;
    public Vector3 RotVector;
    public Transform TurretChild;
    public Transform _gunSight;
    public GameObject Turret;
    private bool _detectedTarget;
    private Coroutine _shootRoutine;
    private void Start()
    {
        TurretChild.localRotation = Quaternion.identity;
        _shootRoutine = StartCoroutine(Shoot());
        ActivateSelf();
    }
    void Update()
    {
        GetZombie();
        RotateTorrete(RotVector);
        if (Input.GetKeyDown(KeyCode.P))
        {
            ManagerSkills.instance.UpgradeSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
            if (_shootRoutine != null)
            {
                StopCoroutine(_shootRoutine);
            }
            _shootRoutine = StartCoroutine(Shoot());
        }
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turreRotationSpeed));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed));
    }
    private Vector3 GetZombie()
    {
        float visionRange = ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretVisionRange);
        colliders = Physics.OverlapSphere(this.transform.position, visionRange, mask);
        nearZombie = Mathf.Infinity;
        GameObject closestZombie = null;
        Vector3 closestPosition = RotVector;
        foreach (Collider collider in colliders)
        {
            float dist = Vector3.Distance(this.transform.position, collider.transform.position);
            if (dist < nearZombie)
            {
                nearZombie = dist;
                closestZombie = collider.gameObject;
                closestPosition = closestZombie.transform.position;
            }
        }
        zombie = closestZombie;
        _detectedTarget = (closestZombie != null);
        if (closestZombie != null)
        {
            RotVector = closestPosition;
        }
        return RotVector;
    }
    private void RotateTorrete(Vector3 rotVector)
    {
        if (zombie == null) return;

        Vector3 direction = rotVector - TurretChild.position;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            TurretChild.rotation = Quaternion.Lerp(TurretChild.rotation, targetRotation, Time.deltaTime * ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turreRotationSpeed));
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 15f);
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            if (_detectedTarget)
            {
                var bullet = PoolBullet.instance.GetBullet(ShooterType.Player);
                var _randomGunSight = _gunSight;
                bullet.transform.position = _randomGunSight.position;
                bullet.transform.rotation = _randomGunSight.rotation;
                bullet.gameObject.GetComponent<Bullet>().shooterType = ShooterType.Player;
            }
            yield return new WaitForSeconds(ManagerSkills.instance.GetValueSkill(SkillCategory.turretCategory, SkillStatType.turretShotSpeed));
        }
    }
    public void DesactivateSelf()
    {
        if (Turret == null) return;
        Turret.gameObject.SetActive(false);
        if (_shootRoutine != null)
        {
            StopCoroutine(_shootRoutine);
            _shootRoutine = null;
        }
    }
    public void ActivateSelf()
    {
        if (Turret == null) return;
        Turret.gameObject.SetActive(true);
        if (_shootRoutine == null)
        {
            _shootRoutine = StartCoroutine(Shoot());
        }
    }
}
