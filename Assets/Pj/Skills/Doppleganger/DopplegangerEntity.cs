using System;
using System.Collections.Generic;
using UnityEngine;
public class DopplegangerEntity : MonoBehaviour
{
    public float _maxLife;
    private float _currentLife;
    public static List<Transform> activeClones = new List<Transform>();
    public static event Action<ZombieBehaviour> ultiDopplegangerActivate;
    public LayerMask layerMask;
    private bool _ultiExecuted = false;
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
        if (_currentLife <= 0f && !_ultiExecuted)
        {
            _ultiExecuted = true;
            UltimateDoppelganger();
            Destroy(gameObject);
        }
    }
    private void UltimateDoppelganger()
    {
        Debug.Log("Ultimate activada");
        if (ManagerSkills.instance.IsUnlockUltimate(SkillCategory.dopplegangerCategory))
        {
            var Colliders = Physics.OverlapSphere(this.transform.position, 5, layerMask);
            foreach (var zombies in Colliders)
            {
                if (zombies.TryGetComponent<ZombieBehaviour>(out var zombie))
                    ultiDopplegangerActivate?.Invoke(zombie);
            }
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
}
