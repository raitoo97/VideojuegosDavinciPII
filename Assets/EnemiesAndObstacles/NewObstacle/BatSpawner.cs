using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatSpawner : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] private List <Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List <GameObject> _bats= new List<GameObject>();
    private int amount = 20;
    public bool canSpawnBats = true;

    private void Start()
    {
        foreach (Transform spawn in transform)
        {
            _spawnPoints.Add(spawn);
        }


        CompleteList(amount);
    }

    private void Update()
    {
        if (WavesManager.instance.waveStarted && canSpawnBats)
        {
            StartCoroutine(Spawn());
        }
    }

    public void CompleteList(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var _clonedBat = GameObject.Instantiate(_prefab);
            _clonedBat.SetActive(false);
            
            _bats.Add(_clonedBat);
        }
    }
    public IEnumerator Spawn()
    {
        canSpawnBats = false;
        GetBat();
        //Transform chosenSpawn = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];
        //Instantiate(prefab, chosenSpawn.position, Quaternion.LookRotation(chosenSpawn.forward));
        yield return new WaitForSeconds(5);
        canSpawnBats = true;
    }

    public GameObject GetBat()
    {
        Transform chosenSpawn = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

        for (int i = 0; i < _bats.Count; i++)
        {
            if (!_bats[i].activeSelf)
            {
                _bats[i].SetActive(true);
                _bats[i].transform.position = chosenSpawn.position;
                _bats[i].transform.forward = chosenSpawn.forward;
                return _bats[i];
            }
        }

        CompleteList(1);
        GameObject _auxBat = _bats[_bats.Count - 1];
        _auxBat.transform.position = chosenSpawn.position;
        _auxBat.transform.forward = chosenSpawn.forward;

        _auxBat.SetActive(true);
        return _auxBat;
    }
}
