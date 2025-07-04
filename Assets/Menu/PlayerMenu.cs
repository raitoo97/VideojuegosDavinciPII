using UnityEngine;
public class PlayerMenu : MonoBehaviour
{
    [SerializeField] private PlayerMenuMovement _movement;
    [SerializeField] private PlayerMenuController _controller;
    [SerializeField] private PlayerMenuAnimation _playerAnimation;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _initSpeed;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = new PlayerMenuMovement(_rb, _groundCheck, _initSpeed, groundLayer, this.transform, wallLayer);
        _playerAnimation = new PlayerMenuAnimation(_animator);
        _controller = new PlayerMenuController(_movement, _playerAnimation,this);
        _initSpeed = 5f;
    }
    private void Update()
    {
        _controller.OnUpdate();
    }
    private void FixedUpdate()
    {
        _controller.OnfixedUpdate();
    }
    private void OnDrawGizmos()
    {
        if (_movement != null)
        {
            _movement.DrawGizmos();
        }
    }
    public Animator GetAnimator { get => _animator; }
    public PlayerMenuMovement GetMovement { get => _movement; }
    public PlayerMenuController GetController { get => _controller; }
    public float GetInitSpeed { get => _initSpeed; }
}
