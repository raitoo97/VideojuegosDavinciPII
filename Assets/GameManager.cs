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
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
    }
    private void Update()
    {
        RestarLevel();
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
}
