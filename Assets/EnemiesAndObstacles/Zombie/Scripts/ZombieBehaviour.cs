using UnityEngine;
using UnityEngine.AI;
public class ZombieBehaviour : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private ZombieAnimations _anims;
    [SerializeField][Tooltip("Vida del zombie")]public int life;
    private float _idleDistance;
    private float _runDistance;
    private float _atackDistance;
    BoxCollider[] _attackColliders;
    private void Awake()
    {
        life = 100;
        _idleDistance = 50f;
        _runDistance = 4;
        _atackDistance = 1.5f;
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
        _attackColliders = GetComponentsInChildren<BoxCollider>(true);
    }
    void Update()
    {
       ZombieStates();
    }
    private void ZombieStates()
    {
        if (life <= 0)
        {
            _anims.ChangeState(STATE.Death);
            _agent.isStopped = true;
            Invoke("Desactivate", 1f);
            
            return;
        }
        if (!this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _idleDistance))
        {
            _anims.ChangeState(STATE.Idle);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
            
        }
        else if (!this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _runDistance))
        {
            _anims.ChangeState(STATE.Run);
            _agent.isStopped = false;
            _agent.SetDestination(GameManager.instance.player.transform.position);
            
        }
        else if (this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _atackDistance))
        {
            _anims.ChangeState(STATE.Atack);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
            //Danio al Player
            foreach (var col in _attackColliders)
            {
                col.enabled = true;
            }
        }
        else
        {
            foreach (var col in _attackColliders)
            {
                col.enabled = false;
            }
        }
    }
    private void Desactivate()
    {
        this.gameObject.SetActive(false);
    }  
}
