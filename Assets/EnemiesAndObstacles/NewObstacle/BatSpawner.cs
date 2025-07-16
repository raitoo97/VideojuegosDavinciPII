using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private List <Transform> _spawnPoints = new List<Transform>();

    public bool canSpawnBats = true;

    private void Start()
    {
        foreach (Transform spawn in transform)
        {
            _spawnPoints.Add(spawn);
        }
    }

    private void Update()
    {
        if (WavesManager.instance.waveStarted && canSpawnBats)
        {
            StartCoroutine(Spawn(_prefab));
        }
    }

    public IEnumerator Spawn(GameObject prefab)
    {
        canSpawnBats = false;
        Transform chosenSpawn = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        Instantiate(prefab, chosenSpawn.position, Quaternion.LookRotation(chosenSpawn.forward));
        yield return new WaitForSeconds(5);
        canSpawnBats = true;
    }
}
