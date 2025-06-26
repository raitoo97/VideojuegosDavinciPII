using UnityEngine;
using UnityEngine.AI;
public class RespawnZombie : MonoBehaviour
{
    public int numberOfRespawn;
    private bool _canRespawn;
    private void Start()
    {
        _canRespawn = true;
    }
    public void StartWave()
    {
        if (_canRespawn)
        {
            for (int i = 0; i < numberOfRespawn; i++)
            {
                GameObject Enemy = PoolEnemy.instance.EnemiesTypesList.Find(x => x.type == EnemyType.Zombie).GetEnemy();
                NavMeshAgent agent = Enemy.GetComponent<NavMeshAgent>();
                if (agent == null)
                {
                    Debug.LogWarning("El enemigo no tiene NavMeshAgent asignado");
                    continue;
                }
                agent.enabled = true;
                agent.Warp(this.transform.position);
                PointManager.instance.GetHandle.EnemySuscribeEvent(Enemy.GetComponent<IEnemies>());
                WavesManager.instance.EnemySuscribeEventToWaveSubstract(Enemy.GetComponent<IEnemies>());
            }
        }
    }
    public int returnNumberOfEnemies()
    {
        return numberOfRespawn;
    }
}
