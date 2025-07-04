using Cinemachine;
using System.Collections;
using UnityEngine;
public class CameraEventsMenu : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCameraMain;
    public CinemachineVirtualCamera VirtualCameraCinematic;
    public GameObject canvas;
    private Coroutine coroutine;
    public bool isFirstTime;
    private void Start()
    {
        canvas.gameObject.SetActive(false);
        isFirstTime = true;
    }
    public void ChangeModeCinematic()
    {
        MenuManager.instance.FinishCinematic = true;
        VirtualCameraMain.Priority = 100;
        VirtualCameraCinematic.Priority = 0;
        canvas.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (isFirstTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isFirstTime = false;
                if (coroutine == null)
                    coroutine = StartCoroutine(ChangeCamera());
            }
        }
    }
    IEnumerator ChangeCamera()
    {
        MenuManager.instance.FinishCinematic = true;
        VirtualCameraMain.Priority = 100;
        VirtualCameraCinematic.Priority = 0;
        yield return new WaitForSeconds(2f);
        canvas.gameObject.SetActive(true);
        coroutine = null;
    }
}
