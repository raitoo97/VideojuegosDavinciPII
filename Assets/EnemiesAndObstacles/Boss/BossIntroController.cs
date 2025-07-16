using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
public class BossIntroController : MonoBehaviour
{
    [Header("PLAYER")]
    [SerializeField] private Player _playerController;
    [SerializeField] private TurretPj _turretPlayerController;
    [SerializeField] private Animator _playerAnimator;           
    [Header("BOSS")]
    [SerializeField] private BossBehaviour _bossBehaviour;
    [SerializeField] private NavMeshAgent _bossNavMesh;
    [SerializeField] private Animator _bossAnimator;             
    [Header("TIMELINE")]
    [SerializeField] private PlayableDirector _bossIntroTimeline;
    private bool _hasPlayed = false;
    public void StartIntroCinematic()
    {
        if (_hasPlayed) return;
        _hasPlayed = true;
        PlayCinematicSequence();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            _bossIntroTimeline.Play();
        }
    }
    private void PlayCinematicSequence()
    {
        _playerController.enabled = false;
        _turretPlayerController.enabled = false;
        _bossBehaviour.enabled = false;
        _bossNavMesh.enabled = false;
        _bossAnimator.applyRootMotion = true;
        _bossAnimator.SetBool("IsCinematic", true);
    }
    public void EndIntroCinematic()
    {
        _playerController.enabled = true;
        _turretPlayerController.enabled = true;
        _bossBehaviour.enabled = true;
        _bossNavMesh.enabled = true;
        _bossAnimator.applyRootMotion = false;
        _bossAnimator.SetBool("IsCinematic", false);
    }
}
