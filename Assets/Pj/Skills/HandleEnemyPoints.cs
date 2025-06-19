using System.Collections.Generic;
using UnityEngine;
public class HandleEnemyPoints
{
    private List <IEnemies> enemies;
    public HandleEnemyPoints()
    {
        enemies = new List<IEnemies>();
    }
    public void EnemySuscribeEvent(IEnemies enemy)
    {
        if (enemies.Contains(enemy)) return;
        enemies.Add(enemy);
        enemy.OnDeath += HandleEnemyDeath;
    }
    public void EnemyDesSuscribeEvent(IEnemies enemy)
    {
        if (!enemies.Contains(enemy)) return;
        enemies.Remove(enemy);
        enemy.OnDeath -= HandleEnemyDeath;
    }
    private void HandleEnemyDeath(IEnemies enemy)
    {

        //float points = enemy.GetPointValue();
        //PointManager.instance.AddPoints(points);
        //ACA DEBERIA INSTANCIAR DROP

        Transform t = enemy.GetTransform();

        var xpPickup = PoolPickUp.instance.poolPickUpsStructs.Find(p => p.type == PickupType.Xp);
        xpPickup?.Drop(t);

        var healPickup = PoolPickUp.instance.poolPickUpsStructs.Find(p => p.type == PickupType.Health);
        healPickup?.Drop(t);

  /*
        if (PoolPickUp.instance.poolPickUpsStructs.Count > 0)
        {
            Transform enemyTransform = enemy.GetTransform();
            PoolPickUp.instance.poolPickUpsStructs[0].Drop(enemyTransform);
        }*/

    }
}
