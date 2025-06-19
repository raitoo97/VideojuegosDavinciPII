using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //todo lo comentado es el slow motion
    [SerializeField] public GameObject shield;
    [SerializeField] private Rigidbody _rb;
     
    public bool canShield;
    /*private float slowDuration = 1f;
    private float timeLow = 0.1f;
    private float timenormal = 1f;
    private float originalFixedDeltaTime;*/
    public float power = 2f;
    public float radius;
    void Start()
    {
        //originalFixedDeltaTime = Time.fixedDeltaTime;
        
    }

    
    void Update()
    {
        
        transform.localScale = new Vector3(radius,radius,radius);
        
        transform.Rotate(Vector3.up, 40f * Time.deltaTime);

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
                rb.AddExplosionForce(power, explosionPos, radius, 1f, ForceMode.Impulse);
            }
        }
       /* SLOWMOTION
        * Time.timeScale = timeLow;
        float t = 0f;
        while (t <= slowDuration)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Lerp(timeLow, timenormal, t / slowDuration);
            Time.fixedDeltaTime = originalFixedDeltaTime * Time.timeScale;
            
            yield return null;


        }*/
       
       yield return null;
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


