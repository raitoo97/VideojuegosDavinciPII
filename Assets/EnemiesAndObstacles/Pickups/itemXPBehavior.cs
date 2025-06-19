using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemXPBehavior : MonoBehaviour
{
    public float points = 50f;

    /*public void SetXpPoints(float amount)
    {
        _points = amount;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            //float points = PoolPickUp.instance.poolPickUpsStructs[0].points;
            PointManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
            
        }
    }

   
}
