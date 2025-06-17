using UnityEngine;
public class RespawnZombie : MonoBehaviour
{
    void Start()
    {

    }
    private void OnEnable()
    {
        int num = Random.Range(0, 100);
        if (num > 50)
        {
            GameObject Enemy = PoolEnemy.instance.EnemiesTypesList.Find(x => x.type == EnemyType.Zombie).GetEnemy();
            Enemy.transform.position = this.transform.position;
            Enemy.transform.rotation = this.transform.rotation;
            PointManager.instance.GetHandle.EnemySuscribeEvent(Enemy.GetComponent<IEnemies>());
        }
    }
}
