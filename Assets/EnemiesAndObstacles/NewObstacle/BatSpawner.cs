using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    private Transform _transform;

    private void Start()
    {
        if (_transform == null)
        {
            _transform = GetComponentInChildren<Transform>();
            Debug.Log("TRANSFORM CONSEGUIDO DE: " + _transform.gameObject.name);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Instantiate(_prefab, _transform.position, Quaternion.identity);
        }
    }
}
