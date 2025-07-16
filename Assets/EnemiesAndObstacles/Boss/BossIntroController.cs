using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.UI;
public class BossIntroController : MonoBehaviour
{
    [Header("PLAYER")]
    [SerializeField]private Player _playerController;
    [SerializeField]private TurretPj _turretPlayerController;
    [SerializeField]private DoppelgangerSkill _dopplePlayerController;
    [SerializeField]private Survivor _survivorPlayerController;
    [SerializeField]private Animator _playerAnimator;
    [SerializeField]private Transform _playerTransform;
    [SerializeField]private Transform _playerNewPosition;
    [Header("BOSS")]
    [SerializeField]private BossBehaviour _bossBehaviour;
    [SerializeField]private NavMeshAgent _bossNavMesh;
    [SerializeField]private Animator _bossAnimator;             
    [Header("TIMELINE")]
    [SerializeField]private PlayableDirector _bossIntroTimeline;
    [Header("UI / FADE")]
    [SerializeField]private Image _fadeImage;
    [SerializeField]private Color _fadeColorinit;
    [SerializeField]private Color _fadeColorfinish;
    private bool _hasPlayed = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _bossIntroTimeline.Play();
        }
    }
    public void StartIntroCinematic()
    {
        if (_hasPlayed) return;
        _hasPlayed = true;
        PlayCinematicSequence();
    }
    public void ActivateFadeOut()
    {
        _playerController.enabled = false;
        _turretPlayerController.enabled = false;
        _playerAnimator.enabled = false;
        _dopplePlayerController.enabled = false;
        _survivorPlayerController.enabled = false;
        StartCoroutine(FadeOut());
    }
    public void ActivateFadeIn()
    {
        StartCoroutine(FadeIn());
    }
    private void PlayCinematicSequence()
    {
        _bossBehaviour.enabled = false;
        _bossNavMesh.enabled = false;
        _bossAnimator.applyRootMotion = true;
        _bossAnimator.SetBool("IsCinematic", true);
    }
    public void StartScreaming()
    {
        _bossAnimator.SetTrigger("StopWalk");
    }
    public void EndIntroCinematic()
    {
        _playerController.gameObject.SetActive(true);
        _playerTransform.position = _playerNewPosition.position;
        _playerController.enabled = true;
        _turretPlayerController.enabled = true;
        _playerAnimator.enabled = true;
        _dopplePlayerController.enabled = true;
        _survivorPlayerController.enabled = true;
        _bossBehaviour.enabled = true;
        _bossNavMesh.enabled = true;
        _bossAnimator.applyRootMotion = false;
        _bossAnimator.SetBool("IsCinematic", false);
    }
    private IEnumerator FadeOut()
    {
        float time = 0f;
        float finishied_time = 2f;
        while (time <= finishied_time)
        {
            _fadeImage.color = Color.Lerp(_fadeColorinit, _fadeColorfinish, time / finishied_time);
            yield return new WaitForSeconds(0.2f);
            time += 0.2f;
        }
        _fadeImage.color = _fadeColorfinish;
        _playerController.gameObject.SetActive(false);
        _fadeImage.gameObject.SetActive(false);
    }
    private IEnumerator FadeIn()
    {
        float time = 0f;
        float finishied_time = 2f;
        while (time <= finishied_time)
        {
            _fadeImage.color = Color.Lerp(_fadeColorfinish, _fadeColorinit, time / finishied_time);
            yield return new WaitForSeconds(0.2f);
            time += 0.2f;
        }
        _fadeImage.color = _fadeColorinit;
        _fadeImage.gameObject.SetActive(true);
    }
}
