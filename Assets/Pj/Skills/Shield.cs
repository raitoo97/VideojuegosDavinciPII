using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private float radius = 7.0f;
    [SerializeField] private float power = 800.0f;
    [SerializeField] private float slowDuration = 1f;
    [SerializeField] private float timeLow = 0.1f;
    [SerializeField] private float timenormal = 1f;
    [SerializeField] private float originalFixedDeltaTime;
    [SerializeField] public GameObject shield;
    [SerializeField] private Rigidbody _rb;
    public bool canShield;
    void Start()
    {
        originalFixedDeltaTime = Time.fixedDeltaTime;
        
    }

    
    void Update()
    {
        if (canShield)
        {
            StartCoroutine(CorrtuineTime());
        }
    }

    IEnumerator CorrtuineTime()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null && rb != _rb)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3f, ForceMode.Impulse);
            }
        }
        Time.timeScale = timeLow;
        float t = 0f;
        while (t <= slowDuration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(timeLow, timenormal, t / slowDuration);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            
            yield return null;


        }
    }
    public void ActivateShield()
    {
        if (shield == null)
        {
            Debug.LogError("Shield no está asignado");
            return;
        }
        shield.SetActive(true);
        ParticlesPool.instance.SpamParticle(ParticleType.Shield, new Vector3(0f, 0f, 0f), Vector3.zero, _rb.transform);

    }
    public void DeactivateShield()
    {

        shield.SetActive(false);
    }
}


/*
[SerializeField]private float radius = 7.0f;             
[SerializeField]private float power = 800.0f;            
[SerializeField]private float slowDuration = 1.8f;       
[SerializeField]private float timeLow = 0.1f;            
[SerializeField]private float timenormal = 1f;
[SerializeField] private float originalFixedDeltaTime;
void Start()
{
    originalFixedDeltaTime = Time.fixedDeltaTime;
}
private void OnDrawGizmos()
{
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, radius);
}
void Update()
{
    if (Input.GetKeyDown(KeyCode.U))
    {
        StartCoroutine(CorrtuineTime());
    }
}
IEnumerator CorrtuineTime()
{
    Vector3 explosionPos = transform.position;
    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    foreach (Collider hit in colliders)
    {
        Rigidbody rb = hit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddExplosionForce(power, explosionPos, radius, 3f,ForceMode.Impulse);
        }
    }
    Time.timeScale = timeLow;
    float t = 0f;
    while (t <= slowDuration)
    {
        t += Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Lerp(timeLow, timenormal, t / slowDuration);
        Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
        print(Time.fixedDeltaTime);
        yield return null;
    }*/