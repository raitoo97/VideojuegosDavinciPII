using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private List <Transform> _spawnPoints = new List<Transform>();

    private void Start()
    {
        foreach (Transform spawn in transform)
        {
            _spawnPoints.Add(spawn);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("ENTRE");
            Spawn(_prefab);
        }
    }

    public void Spawn(GameObject prefab)
    {
        Transform chosenSpawn = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        Instantiate(prefab, chosenSpawn.position, Quaternion.LookRotation(chosenSpawn.forward));
    }
}
