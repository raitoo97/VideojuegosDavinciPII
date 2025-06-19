using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBehavior : MonoBehaviour
{
    public float points = 50f;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            float points = PoolPickUp.instance.poolPickUpsStructs[0].points;
            PointManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }

   
}
