using UnityEngine;
public class PlayerAnimation 
{
    Animator _animator;
    //Constructor
    public PlayerAnimation(Animator a)
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
   public void SetIdle (string action, bool value)
    {
        _animator.SetBool("idle", value);
    }
    public void SetJump (string action, bool value)
    {
        _animator.SetBool("jump", value);
    }
    /*public void SetDash(string action, bool value) ANIMACION DASH POR SI PONEMOS
    {
        _animator.SetBool("dash", value);
    }
    */
    public void SetGround (string action, bool value)
    {
        _animator.SetBool("ground", value);
    }
}
