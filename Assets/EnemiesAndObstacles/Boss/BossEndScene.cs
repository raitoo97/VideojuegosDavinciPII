using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossEndScene : MonoBehaviour
{
    public static Action EndSequence;
    public List<ZombieBehaviour> _zombiesAnim = new List<ZombieBehaviour>();
    public Color init_color;
    public Color final_color;
    public Image BackGroundImage;
    public TurretPj _turretref;
    private void OnEnable()
    {
        EndSequence += EndEscene;
    }
    private void EndEscene()
    {
        StartCoroutine(CorutineFinishLevel());
    }
    private IEnumerator CorutineFinishLevel()
    {
        yield return null;
        _turretref.enabled = false;
        var playerref = GameManager.instance.player.GetComponent<Player>();
        if (playerref != null)
        {
            playerref.GetComponent<TurretPj>().canShoot = false;
            playerref.GetMovement.SetBossFightMode(false);
        }
        ZombieBehaviour[] allZombies = GameObject.FindObjectsByType<ZombieBehaviour>(FindObjectsSortMode.None);
        foreach (var zombie in allZombies)
        {
            _zombiesAnim.Add(zombie);
        }
        foreach (var zombie in _zombiesAnim)
        {
            zombie.life = 0;
        }
        yield return new WaitForSeconds(2);
        BackGroundImage.gameObject.SetActive(true);
        BackGroundImage.color = init_color;
        float time = 0f;
        float finishied_time = 2f;
        while (time <= finishied_time)
        {
            BackGroundImage.color = Color.Lerp(init_color, final_color, time / finishied_time);
            yield return new WaitForSeconds(0.2f);
            time += 0.2f;
        }
        BackGroundImage.color = final_color;
        SceneManager.LoadScene(0);
    }
    private void OnDisable()
    {
        EndSequence -= EndEscene;
    }
}
