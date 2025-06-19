using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHealthBehavior : MonoBehaviour
{
    public float healingpoints = 50f;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            
            Player.instance.HealthPlayer(healingpoints);
            this.gameObject.SetActive(false);
        }
    }
}
