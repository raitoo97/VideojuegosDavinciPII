using UnityEngine;
public class ObstaclesDetectedUI
{
    private GameObject _obstacleWarning;
    public bool canShowWarning = false;
    private GameObject _obstacleWarningArrow;
    public ObstaclesDetectedUI(GameObject _obstacleWarning, GameObject _obstacleWarningArrow)
    {
        this._obstacleWarning = _obstacleWarning;
        this._obstacleWarningArrow = _obstacleWarningArrow;
    }
    public void OnStart()
    {
        _obstacleWarning.gameObject.SetActive(false);
    }
    public void OnUpdate()
    {
        ShowWarningObstacles();
    }
    private void ShowWarningObstacles()
    {
        if (canShowWarning)
        {
            _obstacleWarning.gameObject.SetActive(true);
        }
        else
        {
            _obstacleWarning.gameObject.SetActive(false);
        }
        float rotY = GameManager.instance.player.transform.eulerAngles.y;
        float rotZAngle = -rotY + 180f;
        Quaternion rotZ = Quaternion.Euler(0, 0, rotZAngle);
        _obstacleWarningArrow.transform.rotation = rotZ;
    }
}
