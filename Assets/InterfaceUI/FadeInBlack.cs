using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class FadeInBlack : MonoBehaviour
{
    public Color init_color;
    public Color final_color;
    public Image BackGroundImage;
    private void Start()
    {
        FadeIn();
    }
    public void FadeIn()
    {
        StartCoroutine(FadeCourrtine());
    }
    IEnumerator FadeCourrtine()
    {
        float time = 0f;
        float finishied_time = 2f;
        while (time <= finishied_time)
        {
            BackGroundImage.color = Color.Lerp(init_color, final_color, time/ finishied_time);
            yield return new WaitForSeconds(0.2f);
            time += 0.2f;
        }
        BackGroundImage.color = final_color;
        BackGroundImage.gameObject.SetActive(false);
    }
}
