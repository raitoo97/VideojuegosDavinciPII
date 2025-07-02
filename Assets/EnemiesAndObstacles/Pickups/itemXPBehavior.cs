using UnityEngine;
public class itemXPBehavior : MonoBehaviour
{
    public float points = 100f;
    private NearFromPlayer _nearFromPlayer;
    private void Start()
    {
        _nearFromPlayer = new NearFromPlayer(this.transform,this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            PointManager.instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        _nearFromPlayer.OnUpdate();
    }
}
