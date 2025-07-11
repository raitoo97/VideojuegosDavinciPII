using System.Collections;
using UnityEngine;
public class CinematicManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(StartMusic());
    }
    public IEnumerator StartMusic()
    {
        yield return new WaitForEndOfFrame();
        AudioManager.instance.PlayMusic(AudioManager.instance.Level1Music);
    }
}
