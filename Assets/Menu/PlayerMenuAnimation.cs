using UnityEngine;
public class PlayerMenuAnimation
{
    Animator _animator;
    public PlayerMenuAnimation(Animator a)
    {
        _animator = a;
    }
    public void SetTransforming(string action, bool value)
    {
        _animator.SetBool("transforming", value);
    }
    public void SetWalk(string action, float value)
    {
        _animator.SetFloat("walk", value);
    }
    public void SetReverse(string action, float value)
    {
        _animator.SetFloat("reverse", value);
    }
    public void SetDodge(string action, float value)
    {
        _animator.SetFloat("dodging", value);
    }
    public void SetIdle(string action, bool value)
    {
        _animator.SetBool("idle", value);
    }
    public void SetJump(string action, bool value)
    {
        _animator.SetBool("jump", value);
    }
    public void SetGround(string action, bool value)
    {
        _animator.SetBool("ground", value);
    }
}
