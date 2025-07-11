using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
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
    private Animator _playerRefMenuAnimator;
    public RuntimeAnimatorController _newAnimator;
    public RuntimeAnimatorController _originalAnimator;
    public bool FinishCinematic;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        FinishCinematic = false;
        StartCoroutine(StartMusic());
        startGameButon.onClick.AddListener(StartGame);
        returnMenuButon.onClick.AddListener(ReturnButon);
        returnMenuButon2.onClick.AddListener(ReturnButonTutorial);
        tutorialButon.onClick.AddListener(TutorialButon);
        creditsButon.onClick.AddListener(CreditsButon);
        ExitButton.onClick.AddListener(QuitGame);
    }
    private void StartGame()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            SceneManager.LoadScene(1);
        }
    }
    private void TutorialButon()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            panelTutorial.SetActive(true);
            panelCredits.SetActive(false);
            panelMain.SetActive(false);
            var refPlayerMenu = GameObject.FindObjectOfType<PlayerMenu>();
            if (refPlayerMenu != null)
                _playerRefMenuAnimator = refPlayerMenu.GetAnimator;
            _playerRefMenuAnimator.runtimeAnimatorController = _newAnimator;
            refPlayerMenu.GetController.ChangeModeCinematic = false;
        }
    }
    private void ReturnButon()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            panelTutorial.SetActive(false);
            panelCredits.SetActive(false);
            panelMain.SetActive(true);
        }
    }
    private void ReturnButonTutorial()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            panelTutorial.SetActive(false);
            panelCredits.SetActive(false);
            panelMain.SetActive(true);
            var refPlayerMenu = GameObject.FindObjectOfType<PlayerMenu>();
            if (refPlayerMenu != null)
                _playerRefMenuAnimator = refPlayerMenu.GetAnimator;
            _playerRefMenuAnimator.runtimeAnimatorController = _originalAnimator;
            refPlayerMenu.GetController.ChangeModeCinematic = true;
        }
    }
    private void CreditsButon()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            panelTutorial.SetActive(false);
            panelCredits.SetActive(true);
            panelMain.SetActive(false);
        }
    }
    private void QuitGame()
    {
        if (FinishCinematic)
        {
            AudioManager.instance.PlaySfx(AudioManager.instance.buttonClick);
            Application.Quit();
        }
    }
    public IEnumerator StartMusic()
    {
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayMusic(startSound);
    }
}
