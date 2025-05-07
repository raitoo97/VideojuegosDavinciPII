using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Movement _movement;
    Controller _controller;
    Animator _animator;
    PlayerAnimation _playerAnimation;

    [SerializeField] float speed = 5f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _movement = new Movement(transform, speed);
        _playerAnimation = new PlayerAnimation(_animator);
        _controller = new Controller(_movement, _playerAnimation);
        
    }

    private void Update()
    {
        _controller.OnUpdate();
    }
}
