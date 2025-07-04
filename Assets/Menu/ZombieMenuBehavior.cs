using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieMenuBehavior : MonoBehaviour
{
    [SerializeField]private ZombieAnimations _anims;
    [SerializeField]private NavMeshAgent _agent;
    private Transform _currentTarget;
    private float _nearDistance = 2f;
    public List<Transform> target = new List<Transform>();
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
    }
    private void Start()
    {
        Targets[] targetsFound = GameObject.FindObjectsOfType<Targets>();
        foreach (var t in targetsFound)
        {
            target.Add(t.transform);
        }
        _currentTarget = GetTarget();
    }
    void Update()
    {
        if (_currentTarget == null) return;
        _anims.ChangeState(STATE.Run);
        _agent.isStopped = false;
        if(Vector3.Distance(this.transform.position, _currentTarget.position) < _nearDistance)
        {
            _currentTarget = GetTarget();
        }
    }
    private Transform GetTarget()
    {
        if (target.Count == 0) return null;
        var newTarget = target[Random.Range(0, target.Count)];
        _agent.SetDestination(newTarget.position);
        return newTarget;
    }
}
