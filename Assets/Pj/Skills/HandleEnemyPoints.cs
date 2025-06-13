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
        float points = enemy.GetPointValue();
        PointManager.instance.AddPoints(points);
        Debug.Log($"Enemigo eliminado. +{points} puntos");
    }
}
