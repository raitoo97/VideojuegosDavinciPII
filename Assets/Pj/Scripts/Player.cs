using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Movement _movement;
    [SerializeField] ControlPlayer _controller;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerAnimation _playerAnimation;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] float speed = 5f;
    [Header("Life")]
    [SerializeField] private float _maxLife = 100f;
    [SerializeField] private float _currentLife; 
    //Sound
    AudioManager audioManager => AudioManager.instance;
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            DamagePlayer(10);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            HealthPlayer(10);
        }
    }
    public void DamagePlayer(float damage)
    {
        _currentLife -= damage;
        int randomIndex = Random.Range(0, audioManager.playerDamageSfx.Length);
        audioManager.PlaySfxRandomPitch(audioManager.playerDamageSfx[randomIndex]); //sound effect
        if (_currentLife <= 0f)
        {
            ManagerUI.instance.getLifeBar.CheckLife();
            gameObject.SetActive(false);
        }
    }
    public void HealthPlayer(float healt)
    {
        _currentLife = Mathf.Clamp(_currentLife += healt, 0, _maxLife); 
    }
    public float GetLife { get => Mathf.Clamp(_currentLife, 0, _maxLife); }
}
