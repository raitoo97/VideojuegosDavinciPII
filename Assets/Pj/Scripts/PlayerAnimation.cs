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

    public void SetAnimation(string action, float value) 
    {
        _animator.SetFloat("speed", value);
    }
}
