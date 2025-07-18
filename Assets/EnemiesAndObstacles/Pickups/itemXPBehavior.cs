using UnityEngine;
public class itemXPBehavior : MonoBehaviour
{
    public float points = 100f;
    private NearFromPlayer _nearFromPlayer;
    public float distance = 6;


    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && !player.controlPlayer.GetDodgeMode)
            {
                PointManager.instance.AddPoints(points);
                this.gameObject.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if ( !Player.instance.controlPlayer.GetDodgeMode)
        {
            _nearFromPlayer?.OnUpdate();
        }
    }

    public void InitDistanceBehavior(float newDistance)
    {
        distance = newDistance;
        _nearFromPlayer = new NearFromPlayer(this.transform, this, distance);
        
    }
}
