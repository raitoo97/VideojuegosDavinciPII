using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public Button protoypeButton;
    public Button startGameButon;
    public Button returnMenuButon;
    public Button returnMenuButon2;
    public Button tutorialButon;
    public Button creditsButon;
    public Button ExitButton;
    public GameObject panelMain;
    public GameObject panelTutorial;
    public GameObject panelCredits;
    [SerializeField] private AudioClip startSound;
    void Start()
    {
        StartCoroutine(StartMusic());
        protoypeButton.onClick.AddListener(Protoype);
        startGameButon.onClick.AddListener(StartGame);
        returnMenuButon.onClick.AddListener(ReturnButon);
        returnMenuButon2.onClick.AddListener(ReturnButon);
        tutorialButon.onClick.AddListener(TutorialButon);
        creditsButon.onClick.AddListener(CreditsButon);
        ExitButton.onClick.AddListener(QuitGame);
    }
    private void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    private void Protoype()
    {
        SceneManager.LoadScene(1);
    }
    private void TutorialButon()
    {
        panelTutorial.SetActive(true);
        panelCredits.SetActive(false);
        panelMain.SetActive(false);
    }
    private void ReturnButon()
    {
        panelTutorial.SetActive(false);
        panelCredits.SetActive(false);
        panelMain.SetActive(true);
    }
    private void CreditsButon()
    {
        panelTutorial.SetActive(false);
        panelCredits.SetActive(true);
        panelMain.SetActive(false);
    }
    private void QuitGame()
    {
        Application.Quit();
        print("No funciona en Editor");
    }
    public IEnumerator StartMusic()
    {
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayMusic(startSound);
    }
}
