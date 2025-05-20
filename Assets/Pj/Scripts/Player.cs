using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Movement _movement;
    [SerializeField]ControlPlayer _controller;
    [SerializeField]Animator _animator;
    [SerializeField] PlayerAnimation _playerAnimation;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] float speed = 5f;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _movement = new Movement(transform, speed, groundLayer, _camera);
        _playerAnimation = new PlayerAnimation(_animator);
        _controller = new ControlPlayer(_movement, _playerAnimation);
    }

    private void Update()
    {
        _controller.OnUpdate();
        
    }
}
