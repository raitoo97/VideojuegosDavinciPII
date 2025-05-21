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

    //Life
    int _maxLife = 100;
    int _currentLife; 
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _movement = new Movement(transform, speed, groundLayer, _camera);
        _playerAnimation = new PlayerAnimation(_animator);
        _controller = new ControlPlayer(_movement, _playerAnimation);
        _currentLife = _maxLife;
    }

    private void Update()
    {
        _controller.OnUpdate();
        
        bool dmgPlayer = Input.GetKeyDown(KeyCode.K);
        if (dmgPlayer)
        {
            DamagePlayer(20);
            Debug.Log($"Damage!! X 20, CURRENTLIFE: {_currentLife} ");
        }
    }

    public void DamagePlayer(int damage)
    {
        _currentLife -= damage;
        Debug.Log($"Damage!! X 20, CURRENTLIFE: {_currentLife} ");
        if (_currentLife <= 0)
        {
           gameObject.SetActive(false);
        }
    }
}
