using System.Collections;
using UnityEngine;
public class RespawnZombie : MonoBehaviour
{
    public int numberOfRespawn;
    private bool _canRespawn;
    private Coroutine _spawnCoroutine;
    private void OnEnable()
    {
        _canRespawn = true;
        if (_canRespawn)
        {
            for (int i = 0; i < numberOfRespawn; i++)
            {
                GameObject Enemy = PoolEnemy.instance.EnemiesTypesList.Find(x => x.type == EnemyType.Zombie).GetEnemy();
                Enemy.transform.position = this.transform.position;
                Enemy.transform.rotation = this.transform.rotation;
                PointManager.instance.GetHandle.EnemySuscribeEvent(Enemy.GetComponent<IEnemies>());
            }
        }
        _canRespawn = false;
        if (_spawnCoroutine == null)
            _spawnCoroutine = StartCoroutine(WaitForDestroy());

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
