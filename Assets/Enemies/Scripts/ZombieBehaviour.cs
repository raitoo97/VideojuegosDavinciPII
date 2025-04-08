using UnityEngine;
using UnityEngine.AI;
public class ZombieBehaviour : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private ZombieAnimations _anims;
    [SerializeField][Tooltip("Vida del zombie")]private int _life;
    private void Awake()
    {
        _life = 100;
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
    }
    void Update()
    {
       // ZombieStates();
        if (Input.GetKeyDown(KeyCode.R))
        {
            _life = 0;
        }
    }
    private void ZombieStates()
    {
        if(_life <= 0)
        {
            _anims.ChangeState(STATE.Death);
            _agent.isStopped = true;
            return;
        }
        if (Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) > 50)
        {
            _anims.ChangeState(STATE.Idle);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
        }
        else if(Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) > 2)
        {
            _anims.ChangeState(STATE.Run);
            _agent.isStopped = false;
            _agent.SetDestination(GameManager.instance.player.transform.position);
        }
        else if(Vector3.Distance(this.transform.position, GameManager.instance.player.transform.position) < 2)
        {
            _anims.ChangeState(STATE.Atack);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
        }
    }
}
