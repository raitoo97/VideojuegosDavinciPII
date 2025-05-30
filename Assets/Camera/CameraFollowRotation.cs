using Cinemachine;
using UnityEngine;

public class CameraFollowRotation : MonoBehaviour
{
    public Transform cameraTarget;
    public CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed = 5f;
    private void LateUpdate()
    {
        if (cameraTarget == null || virtualCamera == null)
            return;
        Transform camTransform = virtualCamera.transform;
        Quaternion targetRotation = Quaternion.Euler(0, cameraTarget.eulerAngles.y, 0);
        camTransform.rotation = Quaternion.Slerp(camTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
