using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public enum Shakes
{
    PlayerUnderAtack,
}
public class CameraShakeManager : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;
    public List<ShakesClass> tempList = new List<ShakesClass>();
    private Dictionary<Shakes, ShakesClass> DictionaryShake = new Dictionary<Shakes, ShakesClass>();
    public static CameraShakeManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        foreach (var shake in tempList)
        {
            DictionaryShake[shake.type] = shake;
        }
    }
    private void Start()
    {
        if (virtualCam == null) return;
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Update()
    {
        if (noise != null && virtualCam != null)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                ShakeCamera(Shakes.PlayerUnderAtack);
            }
        }
    }
    public void ShakeCamera(Shakes type)
    {
        if (!DictionaryShake.ContainsKey(type)) return;
        var shake = DictionaryShake[type];
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(shake.setting, shake.intensity, shake.frequency, shake.pivotOffset, shake.duration));
    }
    private IEnumerator ShakeRoutine(NoiseSettings setting, float intensity, float frequency, Vector3 pivotOffset, float duration)
    {
        noise.m_NoiseProfile = setting;
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = frequency;
        noise.m_PivotOffset = pivotOffset;
        yield return new WaitForSeconds(duration);
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
        noise.m_PivotOffset = Vector3.zero;
    }
}
[Serializable]
public class ShakesClass
{
    public Shakes type;
    public NoiseSettings setting;
    public float intensity, frequency, duration;
    public Vector3 pivotOffset;
    public ShakesClass(Shakes type, NoiseSettings setting, float intensity, float frequency,Vector3 pivotOffset , float duration)
    {
        this.type = type;
        this.setting = setting;
        this.intensity = intensity;
        this.frequency = frequency;
        this.pivotOffset = pivotOffset;
        this.duration = duration;
    }
}

