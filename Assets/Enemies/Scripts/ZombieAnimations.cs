using UnityEngine;
public enum STATE
{
    Idle,
    Run,
    Atack,
    Death
}
public class ZombieAnimations : MonoBehaviour
{
    [SerializeField]private STATE _currentState;
    [SerializeField]private Animator _animator;
    private void Awake()
    {
        _currentState = STATE.Idle;
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        HangleStates();
    }
    /// <summary>
    /// Cambia los estados del Zombie
    /// </summary>
    public void ChangeState(STATE newState)
    {
        if (_currentState == newState) return;
        _currentState = newState;
    }
    /// <summary>
    /// Determina que sucede en cada estado del zombie.
    /// </summary>
    private void HangleStates()
    {
        if (_currentState == STATE.Death)
        {
            _animator.SetBool("IsRun", false);
            _animator.SetBool("IsAtack", false);
            _animator.SetBool("IsDeath", true);
        }else if(_currentState == STATE.Run)
        {
            _animator.SetBool("IsRun", true);
            _animator.SetBool("IsAtack", false);
        }else if(_currentState == STATE.Atack)
        {
            _animator.SetBool("IsRun", false);
            _animator.SetBool("IsAtack", true);
        }
        else
        {
            _animator.SetBool("IsRun", false);
            _animator.SetBool("IsAtack", false);
        }
    }
}
