using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float delayBeforeDissapear = 3f;
    public float delayBeforeReset = 1f;

    private bool hasTriggered = false;
    private Material material;
    private Color originalColor;

    private void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            material = rend.material;
            originalColor = material.color;
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!hasTriggered && other.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;

            Invoke(nameof(ChangeVisual), delayBeforeDissapear * 0.8f);
            Invoke(nameof(ChangeVisual), delayBeforeDissapear * 0.5f);
            Invoke(nameof(ChangeVisual), delayBeforeDissapear * 0.3f);
            Invoke(nameof(ChangeVisual), delayBeforeDissapear * 0.15f);
            Invoke(nameof(StartDisappearing), delayBeforeDissapear);
        }
    }

    private void ChangeVisual()
    {
        Color color = material.color;
        color.a *= 0.5f; // reduce la opacidad
        material.color = color;
    }

    private void StartDisappearing()
    {
        gameObject.SetActive(false);
        Invoke(nameof(ResetPlatform), delayBeforeReset);
    }

    private void ResetPlatform()
    {
        gameObject.SetActive(true);
        material.color = originalColor;
        hasTriggered = false;
    }
}