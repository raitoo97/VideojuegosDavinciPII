using UnityEngine;
public class DoppelgangerSkill : MonoBehaviour
{
    [SerializeField]private GameObject _pjDoppelganger;
    private GameObject _currentInstance;
    public float cooldown = 3f;
    private float cooldownTimer = 0f;
    private void Update()
    {
        ActivateSkill();
    }
    private void ActivateSkill()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
        {
            _currentInstance = Instantiate(_pjDoppelganger);
            var entity = _currentInstance.GetComponent<DopplegangerEntity>();
            entity.Initialize(1000f);
            _currentInstance.gameObject.transform.position = this.transform.position + Vector3.back * 2;
            _currentInstance.gameObject.transform.rotation = this.transform.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && !ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
        {
            print("Habilidad no activa");
        }
    }
    private bool CanUseSkill()
    {
        return false;
    }
}
