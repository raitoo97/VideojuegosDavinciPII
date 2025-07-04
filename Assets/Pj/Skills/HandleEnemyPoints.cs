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
        Transform t = enemy.GetTransform();
        Vector3 offset = Vector3.up * 2;
        var xpPickup = PoolPickUp.instance.poolPickUpsStructs.Find(p => p.type == PickupType.Xp);
        xpPickup?.Drop(t.position + offset);
        var healPickup = PoolPickUp.instance.poolPickUpsStructs.Find(p => p.type == PickupType.Health);
        healPickup?.Drop(t.position + offset);
    }
}
