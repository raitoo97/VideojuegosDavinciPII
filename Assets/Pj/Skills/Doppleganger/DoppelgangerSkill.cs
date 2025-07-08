using UnityEngine;
public class DoppelgangerSkill : MonoBehaviour
{
    [SerializeField]private GameObject _pjDoppelganger;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            var Go = Instantiate(_pjDoppelganger);
            Go.gameObject.transform.position = this.transform.position;
            Go.gameObject.transform.rotation = this.transform.rotation;
        }
    }
    private void OnDopplengangerDead()
    {

    }
}
