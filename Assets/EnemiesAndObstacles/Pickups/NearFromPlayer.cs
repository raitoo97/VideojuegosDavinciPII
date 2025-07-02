using System.Collections;
using UnityEngine;
public class NearFromPlayer
{
    private Transform _transform;
    private float _distance;
    private MonoBehaviour _objectMono;
    public NearFromPlayer(Transform _transform, MonoBehaviour _objectMono, float _distance = 7f)
    {
        this._transform = _transform;
        this._objectMono = _objectMono;
        this._distance = _distance;
    }
    public void OnUpdate()
    {
        GoToPlayer();
    }
    private void GoToPlayer()
    {
        var DistancePj = _transform.IsWithinDistanceOf(GameManager.instance.player.transform, _distance);
        if (DistancePj)
        {
            _objectMono.StartCoroutine(GoToPj());
        }
    }
    IEnumerator GoToPj()
    {
        float t = 0;
        float totalTime = 0.5f;
        while(t <= totalTime)
        {
            t += Time.deltaTime;
            _transform.position = Vector3.Lerp(_transform.position, GameManager.instance.player.transform.position, t / totalTime);
            yield return null;
        }
    }
}
