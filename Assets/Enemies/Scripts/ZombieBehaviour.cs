using UnityEngine;
using UnityEngine.AI;
public class ZombieBehaviour : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private ZombieAnimations _anims;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
    }
    void Update()
    {
        ZombieStates();
    }
    private void ZombieStates()
    {
        if (Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) > 50)
        {
            _anims.ChangeState(STATE.Idle);
        }
        else if(Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) > 2)
        {
            _anims.ChangeState(STATE.Run);
        }
        else if(Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) < 2)
        {
            _anims.ChangeState(STATE.Atack);
        }
    }
}
