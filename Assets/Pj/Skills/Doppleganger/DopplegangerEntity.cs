using System.Collections.Generic;
using UnityEngine;
public class DopplegangerEntity : MonoBehaviour
{
    public float _maxLife;
    private float _currentLife;
    public static List<Transform> activeClones = new List<Transform>();
    private void OnEnable()
    {
        ZombieAttack.onHitDopplegangerZombie += HandleHitDopplegangerZombie;
        activeClones.Add(this.transform);
    }
    private void Update()
    {
        print($"La vida actual es : {_currentLife}");
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
        AudioManager.instance.PlaySfxRandomPitch(AudioManager.instance.playerDamageSfx[randomIndex]);
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
    public void Initialize(float maxLife)
    {
        _maxLife = maxLife;
        _currentLife = _maxLife;
    }
}
