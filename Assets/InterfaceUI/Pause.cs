using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Pause : MonoBehaviour
{
    private bool _onPause;
    [SerializeField]private GameObject _pauseMenu;
    [SerializeField]private Button[] Buttons;
    private void Awake()
    {
        _onPause = false;
    }
    void Start()
    {
        Buttons[0].onClick.AddListener(GoToMainMenu);
        Buttons[1].onClick.AddListener(ExitGame);
        Buttons[2].onClick.AddListener(ContinueButton);
        _pauseMenu.SetActive(false);
    }
    private void MenuState()
    {
        if (!_onPause)
        {
            PauseButton();
        }
        else
        {
            ContinueButton();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuState();
        }
    }
    private void GoToMainMenu()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.buttonClick);
        SceneManager.LoadScene(0);
    }
    private void ExitGame()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.buttonClick);
        Application.Quit();
        Debug.Log("Funciona solo en build");
    }
    private void ContinueButton()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.buttonClick);
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
        _onPause = false;
    }
    private void PauseButton()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.buttonClick);
        Time.timeScale = 0.0f;
        _pauseMenu.SetActive(true);
        _onPause = true;
    }
}
