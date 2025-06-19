using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHealthBehavior : MonoBehaviour
{
    public float healingPoints = 10f;

    /*
    public void SetHealingPoints(float amount)
    {
        _healingPoints = amount;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            
            Player.instance.HealthPlayer(healingPoints);
            this.gameObject.SetActive(false);

            
        }
    }
}
