using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnEnable()
    {
        Player.OnPlayerDeath += RestarLevel;
    }
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        Time.timeScale = 1.0f;
        var refFade = FindObjectOfType<FadeInBlack>();
        refFade.BackGroundImage.gameObject.SetActive(true);
        StartCoroutine(StartMusic());
    }
    private void RestarLevel()
    {
        if (player.TryGetComponent<Player>(out var playerlife))
        {
            if(playerlife.GetLife <= 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    private void OnDisable()
    {
        Player.OnPlayerDeath -= RestarLevel;
    }
    public IEnumerator StartMusic()
    {
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayMusic(AudioManager.instance.Level1Music);
    }
}
