using System.Collections.Generic;
using UnityEngine;
public class DoppelgangerSkill : MonoBehaviour
{
    [SerializeField]private GameObject _pjDoppelganger;
    private GameObject _currentInstance;
    public float _cooldown;
    public float _lifeCopy;
    private float _cooldownTimer = 0f;
    [Header("PjMaterialChange")]
    [SerializeField]private Material _orginalMaterialPj;
    [SerializeField]private Material _dopplegangerMaterialPj;
    [SerializeField]private List<MeshRenderer> _materialsPj = new List<MeshRenderer>();
    [Header("TurretMaterialChange")]
    [SerializeField]private MeshRenderer _turretMaterial;
    [SerializeField]private Material _orginalMaterialTurret;
    [SerializeField]private Material _dopplegangerMaterialTurert;
    [Header("MisileMaterialChange")]
    [SerializeField]private MeshRenderer _misilMaterial;
    [SerializeField]private Material _orginalMaterialMisil;
    [SerializeField]private Material _dopplegangerMaterialMisil;
    private void Update()
    {
        GetSkillsValue();
        if (_cooldownTimer > 0f)
            _cooldownTimer -= Time.deltaTime;
        ActivateSkill();
        CheckMaterial();
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
    }
    private void CheckMaterial()
    {
        if(DopplegangerEntity.activeClones.Count > 0)
        {
            if (_materialsPj.Count == 0) return;
            foreach (var material in _materialsPj)
            {
                material.material = _dopplegangerMaterialPj;
            }
            _turretMaterial.material = _dopplegangerMaterialTurert;
            _misilMaterial.material = _dopplegangerMaterialMisil;
        }
        else
        {
            if (_materialsPj.Count == 0) return;
            foreach (var material in _materialsPj)
            {
                material.material = _orginalMaterialPj;
            }
            _turretMaterial.material = _orginalMaterialTurret;
            _misilMaterial.material = _orginalMaterialMisil;
        }
    }
    private void ActivateSkill()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
            {
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
