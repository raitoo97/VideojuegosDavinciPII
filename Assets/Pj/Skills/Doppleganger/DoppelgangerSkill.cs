using UnityEngine;
public class DoppelgangerSkill : MonoBehaviour
{
    [SerializeField]private GameObject _pjDoppelganger;
    private GameObject _currentInstance;
    public float _cooldown;
    public float _lifeCopy;
    private float _cooldownTimer = 0f;
    private void Update()
    {
        GetSkillsValue();
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
        ActivateSkill();
        if (Input.GetKeyDown(KeyCode.V))
        {
            ManagerSkills.instance.UpgradeSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ManagerSkills.instance.UpgradeSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ManagerSkills.instance.TryUnlockUltimate(SkillCategory.dopplegangerCategory);
        }
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger));
        print(ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife));
    }
    private void ActivateSkill()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
            {
                Debug.Log("Habilidad no activa");
                return;
            }
            if (_cooldownTimer > 0f)
            {
                return;
            }
            _currentInstance = Instantiate(_pjDoppelganger);
            var entity = _currentInstance.GetComponent<DopplegangerEntity>();
            entity.Initialize(_lifeCopy);
            _currentInstance.transform.position = this.transform.position + Vector3.back * 2;
            _currentInstance.transform.rotation = this.transform.rotation;
            _cooldownTimer = _cooldown;
        }
    }
    private void GetSkillsValue()
    {
        _cooldown = ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.coldowndoppleganger);
        _lifeCopy = ManagerSkills.instance.GetValueSkill(SkillCategory.dopplegangerCategory, SkillStatType.dopplegangerLife);
    }
}
