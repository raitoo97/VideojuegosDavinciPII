using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ParticleType
{
    Sparks,
    Explosion,
    Dash,
    Shield,
    TurretUltimate,
    DashUlti
}
public class ParticlesPool : MonoBehaviour
{
    public static ParticlesPool instance;
    public List<SpecificParticle> particleConfigs = new List<SpecificParticle>();
    public Dictionary<ParticleType, SpecificParticle> _particles = new Dictionary<ParticleType, SpecificParticle>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        foreach (var particle in particleConfigs)
        {
            _particles[particle.type] = particle;
            particle.Onstart(this);
        }
    }
    public void SpamParticle(ParticleType type , Vector3 offset, Vector3 offsetRot, Transform customSpawnParent)
    {
        if(!_particles.ContainsKey(type)) return;
        var particle = _particles[type];
        particle.PlayParticle(offset, offsetRot, customSpawnParent);
    }
    public void SpamParticle(ParticleType type, Vector3 offset, Vector3 offsetRot, Transform customSpawnParent,float size)
    {
        if (!_particles.ContainsKey(type)) return;
        var particle = _particles[type];
        particle.PlayParticle(offset, offsetRot, customSpawnParent,size);
    }
}
[Serializable]
public class SpecificParticle
{
    public ParticleType type;
    public Transform poolParent;
    public int initialPoolSize;
    public GameObject ParticlePrefab;
    public List<GameObject> pooledParticles = new List<GameObject>();
    private MonoBehaviour coroutineRunner;
    public void Onstart(MonoBehaviour context)
    {
        coroutineRunner = context;
        CompleteList(initialPoolSize);
    }
    private void CompleteList(int num)
    {
        for(int i = 0; i < num; i++)
        {
            var cloneObject = GameObject.Instantiate(ParticlePrefab);
            cloneObject.transform.parent = poolParent;
            pooledParticles.Add(cloneObject);
            cloneObject.SetActive(false);
        }
    }
    private GameObject ReturnParticle()
    {
        foreach (var particle in pooledParticles)
        {
            if (!particle.activeSelf)
            {
                particle.SetActive(true);
                return particle;
            }
        }
        CompleteList(1);
        var auxParticle = pooledParticles[pooledParticles.Count - 1];
        auxParticle.SetActive(true);
        return auxParticle;
    }
    public void PlayParticle(Vector3 offsetPos,Vector3 offsetRot, Transform customSpawnParent)
    {
        if (pooledParticles == null) return;
        var ParticleObject = ReturnParticle();
        ParticleObject.transform.SetParent(customSpawnParent);
        ParticleObject.transform.localPosition = Vector3.zero + offsetPos;
        ParticleObject.transform.rotation = customSpawnParent.rotation * Quaternion.Euler(offsetRot);
        var ParticleSystem = ParticleObject.GetComponent<ParticleSystem>();
        if (ParticleSystem == null) return;
        coroutineRunner.StartCoroutine(PlayParticleCoroutine(ParticleSystem));
    }
    public void PlayParticle(Vector3 offsetPos, Vector3 offsetRot, Transform customSpawnParent, float size)
    {
        if (pooledParticles == null) return;
        var ParticleObject = ReturnParticle();
        ParticleObject.transform.SetParent(customSpawnParent);
        ParticleObject.transform.localPosition = Vector3.zero + offsetPos;
        ParticleObject.transform.rotation = customSpawnParent.rotation * Quaternion.Euler(offsetRot);
        ParticleObject.transform.localScale = Vector3.one * size;
        var ParticleSystem = ParticleObject.GetComponent<ParticleSystem>();
        if (ParticleSystem == null) return;
        coroutineRunner.StartCoroutine(PlayParticleCoroutine(ParticleSystem));
    }
    IEnumerator PlayParticleCoroutine(ParticleSystem particle)
    {
        particle.Clear();
        particle.Play();
        yield return new WaitWhile(() => particle.IsAlive(true) && particle.particleCount >= 0);
        var go = particle.gameObject;
        go.SetActive(false);
        go.transform.SetParent(poolParent);
    }
}
