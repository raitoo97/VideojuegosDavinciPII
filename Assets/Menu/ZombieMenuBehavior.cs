using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieMenuBehavior : MonoBehaviour
{
    [SerializeField]private NavMeshAgent _agent;
    private Transform _currentTarget;
    private float _nearDistance = 2f;
    public List<Transform> target = new List<Transform>();
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
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
        _agent.isStopped = false;
        if (this.transform.IsWithinDistanceOf(_currentTarget,_nearDistance))
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
