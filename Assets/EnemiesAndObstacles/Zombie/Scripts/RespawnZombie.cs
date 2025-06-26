using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class RespawnZombie : MonoBehaviour
{
    public int numberOfRespawn;
    private bool _canRespawn;
    private Coroutine _spawnCoroutine;
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
                if (agent != null) agent.enabled = false;
                Rigidbody rb = Enemy.GetComponent<Rigidbody>();
                if (rb != null) rb.isKinematic = true;
                if (agent == null) return;
                agent.enabled = true;
                agent.Warp(this.transform.position);
                if (rb != null) rb.isKinematic = false;
                PointManager.instance.GetHandle.EnemySuscribeEvent(Enemy.GetComponent<IEnemies>());
                WavesManager.instance.EnemySuscribeEventToWaveSubstract(Enemy.GetComponent<IEnemies>());
            }
        }
        _canRespawn = false;
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(WaitForDestroy());
    }
    public int returnNumberOfEnemies()
    {
        return numberOfRespawn;
    }
    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
    private IEnumerator WaitForDestroy()
    {
        yield return null;
        _spawnCoroutine = null;
        this.gameObject.SetActive(false);
    }
}
