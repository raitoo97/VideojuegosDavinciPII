using System;
using System.Collections;
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
    public event Action<IEnemies> _substractEnemyFromWave;
    private bool _canEjecuteCorutine;
    private Coroutine _coroutine;
    public int maxLife = 200;
    private void Awake()
    {
        _enemypoints = 20; //puntos
        life = 200;
        _idleDistance = 1000f;
        _runDistance = 4;
        _atackDistance = 1.5f;
        _agent = GetComponent<NavMeshAgent>();
        _anims = GetComponent<ZombieAnimations>();
        _attackColliders = GetComponentsInChildren<BoxCollider>(true);
    }
    private void OnEnable()
    {
        Bullet.onHitZombie += HandleHitZombie;
        _canEjecuteCorutine = false;
        TrailCollider.onHitZombie += HandleDashUlti;
        DopplegangerEntity.ultiDopplegangerActivate += HandleDopplegangerUlti;
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
            _canEjecuteCorutine = true;
            if (_canEjecuteCorutine && _coroutine == null)
            {
                _coroutine = StartCoroutine(corrutinaDeath());
                _canEjecuteCorutine = false;
            }
            return;
        }
        Transform target = GetClosestClone();
        if (target == null)
        {
            target = GameManager.instance.player.transform;
            if (!this.transform.IsWithinDistanceOf(target, _idleDistance))
            {
                _anims.ChangeState(STATE.Idle);
                _agent.isStopped = true;
                _agent.SetDestination(_agent.transform.position);

            }
            else if (!this.transform.IsWithinDistanceOf(target, _runDistance))
            {
                _anims.ChangeState(STATE.Run);
                _agent.isStopped = false;
                _agent.SetDestination(target.position);

            }
            else if (this.transform.IsWithinDistanceOf(target, _atackDistance))
            {
                _anims.ChangeState(STATE.Atack);
                _agent.isStopped = true;
                _agent.SetDestination(_agent.transform.position);
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
        else
        {
            if (!this.transform.IsWithinDistanceOf(target, _idleDistance))
            {
                _anims.ChangeState(STATE.Idle);
                _agent.isStopped = true;
                _agent.SetDestination(_agent.transform.position);

            }
            else if (!this.transform.IsWithinDistanceOf(target, _runDistance))
            {
                _anims.ChangeState(STATE.Run);
                _agent.isStopped = false;
                _agent.SetDestination(target.position);

            }
            else if (this.transform.IsWithinDistanceOf(target, _atackDistance))
            {
                _anims.ChangeState(STATE.Atack);
                _agent.isStopped = true;
                _agent.SetDestination(_agent.transform.position);
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
    }
    private Transform GetClosestClone()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (var clone in DopplegangerEntity.activeClones)
        {
            if (clone == null) continue;
            float dist = this.transform.IsMostNearDistance(clone.transform);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                closest = clone;
            }
        }
        return closest;
    }
    private void HandleHitZombie(ZombieBehaviour enemy)
    {
        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.turretCategory))
        {
            int randomIndexUltimate = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndexUltimate]); //sound effect
            enemy.life -= 100;
            int randomParticleUltimate = UnityEngine.Random.Range(0, 100);
            if (randomParticleUltimate <= 25)
            {
                ParticlesPool.instance.SpamParticle(ParticleType.TurretUltimate, new Vector3(0f, 2f, 0f), Vector3.zero, enemy.transform);
            }
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
            AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndex]); //sound effect
            ParticlesPool.instance.SpamParticle(ParticleType.Explosion, new Vector3(0f, 2f, 0f), Vector3.zero, enemy.transform);
            enemy.life -= 50;
            
        }
    }
    private void HandleDopplegangerUlti(ZombieBehaviour enemy)
    {
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.turretPlayerImpactSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.turretPlayerImpactSfx[randomIndex]);
        enemy.life = 0;
    }
    private void HandleDashUlti(ZombieBehaviour enemy)
    {
        ParticlesPool.instance.SpamParticle(ParticleType.DashUlti, new Vector3(0f, 2f, 0f), Vector3.zero, enemy.transform);
        enemy.life = 0;
    }
    private void OnDisable()
    {
        if (PointManager.instance != null)
        {
            PointManager.instance.GetHandle.EnemyDesSuscribeEvent(this);
        }
        if (WavesManager.instance != null)
        {
            WavesManager.instance.EnemyDesuscribeEventToWaveSubstract(this);
        }
        Bullet.onHitZombie -= HandleHitZombie;
        TrailCollider.onHitZombie -= HandleDashUlti;
        DopplegangerEntity.ultiDopplegangerActivate -= HandleDopplegangerUlti;
    }
    IEnumerator corrutinaDeath()
    {
        yield return null;
        OnDeath?.Invoke(this);
        _substractEnemyFromWave?.Invoke(this);
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
        _coroutine = null;
    }
    public float GetPointValue()
    {
        return _enemypoints;
    }
    public int SubstractFromWave()
    {
        return 1;
    }
    public Transform GetTransform()
    {
        return this.transform;
    }
}
