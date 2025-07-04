using Cinemachine;
using UnityEngine;
public class CameraEvents : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCameraMain;
    public CinemachineVirtualCamera VirtualCameraCinematic;
    public GameObject canvas;
    private void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    public void ChangeModeCinematic()
    {
        MenuManager.instance.FinishCinematic = true;
        VirtualCameraMain.Priority = 100;
        VirtualCameraCinematic.Priority = 0;
        canvas.gameObject.SetActive(true);
    }
}
