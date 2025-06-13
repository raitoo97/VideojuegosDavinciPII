using System;
public interface IEnemies
{
    public event Action<IEnemies> OnDeath;
    public float GetPointValue();
}
