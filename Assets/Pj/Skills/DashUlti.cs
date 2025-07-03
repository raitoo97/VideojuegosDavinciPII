using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUlti : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    private List<GameObject> _trailPool = new List<GameObject>();

    public void CreateDashTrail(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        GameObject trail = GetTrailFromPool();
        trail.SetActive(true);
        trail.transform.position = start + direction * (distance / 2f);
        trail.transform.forward = direction;
        trail.transform.localScale = new Vector3(1, 1, distance);

        StartCoroutine(DesactivateCourutine(trail));

    }


    private IEnumerator DesactivateCourutine(GameObject trail)
    {
        yield return new WaitForSeconds(1f);
        trail.SetActive(false);
    }

    public GameObject GetTrailFromPool()
    {

        foreach (GameObject trail in _trailPool)
        {
            if (!trail.activeInHierarchy)
            {
                return trail;
            }
        }

        GameObject newTrail = Instantiate(prefab);
        _trailPool.Add(newTrail);
        return newTrail;
    }
}
