using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private float _currentPoints;
    [SerializeField] GameObject _pointsText;
    [SerializeField] GameObject _pointsNumber;
    public static PointManager instance;
    private HandleEnemyPoints HandelEnemy;
    private float normalizedTime;

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
        var number = _pointsNumber.GetComponent<Text>();

        var originalColor = text.color;
        var originalColorNumber = number.color;

        var originalSize = text.fontSize;

        float duration = 0.3f;
        float t = 0f;

        int targetSize = 22;
        Color targetColor = Color.red;
        while (t < duration) 
        {
            t += Time.deltaTime;
            float normalizeTime = Mathf.Clamp01(t/duration);

            text.color = Color.Lerp(originalColor, targetColor, normalizedTime);
            number.color = Color.Lerp(originalColorNumber, targetColor, normalizedTime);

            
            text.fontSize = (int)Mathf.Lerp(originalSize, targetSize, normalizedTime);
            number.fontSize = (int)Mathf.Lerp(originalSize, targetSize, normalizedTime);

            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.1f);

        // Volver atrás 
        t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float normalizedTime = Mathf.Clamp01(t / duration);

            text.color = Color.Lerp(targetColor, originalColor, normalizedTime);
            number.color = Color.Lerp(targetColor, originalColorNumber, normalizedTime);

            text.fontSize = (int)Mathf.Lerp(targetSize, originalSize, normalizedTime);
            number.fontSize = (int)Mathf.Lerp(targetSize, originalSize, normalizedTime);

            yield return null;
        }

        
        text.color = originalColor;
        number.color = originalColorNumber;
        text.fontSize = originalSize;
        number.fontSize = originalSize;

        PjSkillsUpgradeUI.alreadyClicked = false;
    }
    public float CurrentPoints => _currentPoints;
    public HandleEnemyPoints GetHandle => HandelEnemy;
}
