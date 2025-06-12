using System;
using UnityEngine;
using UnityEngine.AI;
public class ZombieBehaviour : MonoBehaviour , IEnemies
{
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private ZombieAnimations _anims;
    [SerializeField][Tooltip("Vida del zombie")]public int life;
    private float _idleDistance;
    private float _runDistance;
    private float _atackDistance;
    BoxCollider[] _attackColliders;
    private float _enemypoints;
    public event Action<IEnemies> OnDeath;
    private void Awake()
    {
        _enemypoints = 20;
        life = 100;
        _idleDistance = 50f;
        _runDistance = 4;
        _atackDistance = 1.5f;
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
        _attackColliders = GetComponentsInChildren<BoxCollider>(true);
    }
    private void OnEnable()
    {
        Bullet.onHitZombie += HandleHitZombie;
    }
    void Update()
    {
       ZombieStates();
    }
    private void ZombieStates()
    {
        if (life <= 0)
        {
            _anims.ChangeState(STATE.Death);
            _agent.isStopped = true;
            Invoke("Desactivate", 1f);
            
            return;
        }
        if (!this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _idleDistance))
        {
            _anims.ChangeState(STATE.Idle);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
            
        }
        else if (!this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _runDistance))
        {
            _anims.ChangeState(STATE.Run);
            _agent.isStopped = false;
            _agent.SetDestination(GameManager.instance.player.transform.position);
            
        }
        else if (this.transform.IsWithinDistanceOf(GameManager.instance.player.transform, _atackDistance))
        {
            _anims.ChangeState(STATE.Atack);
            _agent.isStopped = true;
            _agent.SetDestination(_agent.transform.position);
            //Danio al Player
            foreach (var col in _attackColliders)
            {
                col.enabled = true;
            }
        }
        else
        {
            foreach (var col in _attackColliders)
            {
                col.enabled = false;
            }
        }
    }
    private void HandleHitZombie(ZombieBehaviour enemy)
    {
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndex]); //sound effect
        ParticlesPool.instance.SpamParticle(ParticleType.Explosion, new Vector3(0f, 2f, 0f), Vector3.zero, enemy.transform);
        enemy.life = 0;

    }
    private void Desactivate()
    {
        OnDeath?.Invoke(this);
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        if (PointManager.instance != null)
        {
            PointManager.instance.GetHandle.EnemyDesSuscribeEvent(this);
        }
        Bullet.onHitZombie -= HandleHitZombie;
    }
    public float GetPointValue()
    {
        return _enemypoints;
    }
}
