using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DopplegangerEntity : MonoBehaviour
{
    public float _maxLife;
    private float _currentLife;
    public static List<Transform> activeClones = new List<Transform>();
    public static event Action<ZombieBehaviour> ultiDopplegangerActivate;
    public LayerMask layerMask;
    private void OnEnable()
    {
        ZombieAttack.onHitDopplegangerZombie += HandleHitDopplegangerZombie;
        activeClones.Add(this.transform);
    }
    private void HandleHitDopplegangerZombie(DopplegangerEntity doppleganger, float damage)
    {
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.zombieAttackSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.zombieAttackSfx[randomIndex]);
        doppleganger.TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        _currentLife -= damage;
        int randomIndex = UnityEngine.Random.Range(0, AudioManager.instance.playerDamageSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.playerDamageSfx[randomIndex]);
        if (_currentLife <= 0f)
        {
            UltimateDoppelganger();
        }
    }
    private void UltimateDoppelganger()
    {
        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.dopplegangerCategory))
        {
            var Colliders = Physics.OverlapSphere(this.transform.position,5,layerMask);
            foreach (var zombies in Colliders)
            {
                if (zombies.TryGetComponent<ZombieBehaviour>(out var zombie))
                    ultiDopplegangerActivate?.Invoke(zombie);
            }
        }
        bool initCorutine = false;
        if (!initCorutine)
        {
            initCorutine = true;
            StartCoroutine(DestroyGameObject());
        }
    }
    private void OnDisable()
    {
        ZombieAttack.onHitDopplegangerZombie -= HandleHitDopplegangerZombie;
        activeClones.Remove(this.transform);
    }
    public void Initialize(float maxLife)
    {
        _maxLife = maxLife;
        _currentLife = _maxLife;
    }
    IEnumerator DestroyGameObject()
    {
        ParticlesPool.instance.SpamParticle(ParticleType.Explosion, new Vector3(0f, 2f, 0f), Vector3.zero, transform);
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
