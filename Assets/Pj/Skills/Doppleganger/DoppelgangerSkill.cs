using UnityEngine;
public class DoppelgangerSkill : MonoBehaviour
{
    [SerializeField]private GameObject _pjDoppelganger;
    private GameObject _currentInstance;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
        {
            _currentInstance = Instantiate(_pjDoppelganger);
            _currentInstance.gameObject.transform.position = this.transform.position + Vector3.back * 2;
            _currentInstance.gameObject.transform.rotation = this.transform.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && !ManagerSkills.instance.IsUnlocked(SkillCategory.dopplegangerCategory))
        {
            print("Habilidad no activa");
        }
    }
}
