using System.Collections;
using UnityEngine;
public class RespawnZombie : MonoBehaviour
{

    [Tooltip("Dejan de respawnear los Zombies")]public bool keepSpawning;
    private void Awake()
    {
        keepSpawning = true;
    }
    void Start()
    {
        StartCoroutine(RespawnEnemy());
    }
    IEnumerator RespawnEnemy()
    {
        while (keepSpawning)
        {
            int num = Random.Range(0, 100);
            if (num > 50)
            {
                GameObject Enemy = PoolZombies.instance.GetZombie();
                Enemy.transform.position = this.transform.position;
                Enemy.transform.rotation = this.transform.rotation;
            }
            else
            {
                print("No Respawn");
            }
            yield return new WaitForSeconds(3f);
        }
    }
}
