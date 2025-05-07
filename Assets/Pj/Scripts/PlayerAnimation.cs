using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

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


    public void SetDodge(string action, float value)
    {
        _animator.SetFloat("dodging", value);
    }

   
}
