using System.Collections.Generic;
using UnityEngine;
public class DopplegangerEntity : MonoBehaviour
{
    [SerializeField]private float _maxLife;
    [SerializeField]private float _currentLife;
    public static List<Transform> activeClones = new List<Transform>();
    private void OnEnable()
    {
        ZombieAttack.onHitDopplegangerZombie += HandleHitDopplegangerZombie;
        activeClones.Add(this.transform);
    }
    private void Start()
    {
        _currentLife = _maxLife;
    }
    private void HandleHitDopplegangerZombie(DopplegangerEntity doppleganger, float damage)
    {
        int randomIndex = Random.Range(0, AudioManager.instance.zombieAttackSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.zombieAttackSfx[randomIndex]);
        doppleganger.TakeDamage(damage);
    }
    public void TakeDamage(float damage)
    {
        _currentLife -= damage;
        int randomIndex = Random.Range(0, AudioManager.instance.playerDamageSfx.Length);
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.playerDamageSfx[randomIndex]); //sound effect
        if (_currentLife <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDisable()
    {
        ZombieAttack.onHitDopplegangerZombie -= HandleHitDopplegangerZombie;
        activeClones.Remove(this.transform);
    }
}
