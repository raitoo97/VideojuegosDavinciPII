using UnityEngine;
public class PointManager : MonoBehaviour
{
    private float _currentPoints;
    public static PointManager instance;
    private void Awake()
    {
        
    }
    public void AddPoints(float value)
    {
        _currentPoints += value;
    }
    public float GetPoints { get => _currentPoints; }
}
