using System.Collections.Generic;
using UnityEngine;
public class PoolZombies : MonoBehaviour
{
    [SerializeField]private List<GameObject> Zombies = new List<GameObject>();
    void Start()
    {
        print("Hola");
    }
}
