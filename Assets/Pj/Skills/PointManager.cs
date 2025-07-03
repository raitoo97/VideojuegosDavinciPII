using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] private float _currentPoints;
    [SerializeField] GameObject _pointsText;
    [SerializeField] GameObject _pointsNumber;
    [SerializeField] Text _cantUpgradeText;

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
        var number = _pointsNumber.GetComponent<Text>();
        var originalColorNumber = number.color;
        var originalSize = number.fontSize;

        float duration = 0.2f;
        float t = 0f;

        int targetSize = 22;
        Color targetColor = Color.red;
        while (t < duration) 
        {
            t += Time.unscaledDeltaTime;
            float normalizeTime = Mathf.Clamp01(t/duration);

            number.color = Color.Lerp(originalColorNumber, targetColor, normalizeTime);
            number.fontSize = (int)Mathf.Lerp(originalSize, targetSize, normalizeTime);
            yield return null;
        }
        yield return new WaitForSecondsRealtime(0.5f);

        // Volver atrás 
        t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float normalizedTime = Mathf.Clamp01(t / duration);

            number.color = Color.Lerp(targetColor, originalColorNumber, normalizedTime);
            number.fontSize = (int)Mathf.Lerp(targetSize, originalSize, normalizedTime);
            yield return null;
        }

        number.color = originalColorNumber;
        number.fontSize = originalSize;

        PjSkillsUpgradeUI.alreadyClickedUnlock = false;
    }

    public IEnumerator CantUpgradeRoutine()
    {
        Color originalColor = _cantUpgradeText.color;
        float duration = 0.3f;
        float t = 0;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float normalizeTime = Mathf.Clamp01(t / duration);

            Color c = originalColor;
            c.a = Mathf.Lerp(0, 1, normalizeTime);
            _cantUpgradeText.color = c;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(3f);

        t = 0f;
        while (t < duration) 
        {
            t += Time.unscaledDeltaTime;
            float normalizeTime = Mathf.Clamp01(t / duration);

            Color c = originalColor;
            c.a = Mathf .Lerp(1,0 , normalizeTime);
            _cantUpgradeText.color = c;

            yield return null;
        }

        Color finalColor = originalColor;
        finalColor.a = 0f;
        _cantUpgradeText.color = finalColor;
        PjSkillsUpgradeUI.alreadyClickedUpgrade = false;
    }
    public float CurrentPoints => _currentPoints;
    public HandleEnemyPoints GetHandle => HandelEnemy;
}
