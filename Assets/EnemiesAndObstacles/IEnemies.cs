using System;
public interface IEnemies
{
    public event Action<IEnemies> OnDeath;
    public event Action<IEnemies> _substractEnemyFromWave;
    public float GetPointValue();
    public int SubstractFromWave();
}
