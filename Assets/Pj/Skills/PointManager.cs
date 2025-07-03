using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private float _currentPoints;
    [SerializeField] GameObject _pointsText;
    public static PointManager instance;
    private HandleEnemyPoints HandelEnemy;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        HandelEnemy = new HandleEnemyPoints();
    }
    public void AddPoints(float value)
    {
        _currentPoints += value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _currentPoints += 1000;
        }
    }
    public bool SpendPoints(float cost)
    {
        if (_currentPoints >= cost)
        {
            _currentPoints -= cost;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasEnoughPoints(float cost)
    {
        if (_currentPoints >= cost)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator CantUnlockRoutine()
    {
        var text = _pointsText.GetComponent<Text>();
        var originalColor = text.color;

        text.color = Color.red;
        Debug.Log("CantUnlockRoutine");

        yield return new WaitForSecondsRealtime(3f);

        text.color = originalColor;
    }
    public float CurrentPoints => _currentPoints;
    public HandleEnemyPoints GetHandle => HandelEnemy;
}
