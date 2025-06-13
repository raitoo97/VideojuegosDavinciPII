using System.Collections;
using UnityEngine;
public class RespawnZombie : MonoBehaviour
{
    [Tooltip("Dejan de respawnear los Zombies")]public bool keepSpawning;
    private void Awake()
    {
        keepSpawning = true; //desactivado 
    }
    void Start()
    {
        StartCoroutine(WaitForFrame());
    }
    IEnumerator RespawnEnemy()
    {
        while (keepSpawning)
        {
            int num = Random.Range(0, 100);
            if (num > 50)
            {
                GameObject Enemy = PoolEnemy.instance.EnemiesTypesList.Find(x => x.type == EnemyType.Zombie).GetEnemy();
                Enemy.transform.position = this.transform.position;
                Enemy.transform.rotation = this.transform.rotation;
                PointManager.instance.GetHandle.EnemySuscribeEvent(Enemy.GetComponent<IEnemies>());
            }
            yield return new WaitForSeconds(3f);
        }
    }
    public IEnumerator WaitForFrame()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(RespawnEnemy());
    }
}
