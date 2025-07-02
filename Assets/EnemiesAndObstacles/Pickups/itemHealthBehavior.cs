using UnityEngine;
public class itemHealthBehavior : MonoBehaviour
{
    public float healingPoints = 10f;
    private NearFromPlayer _nearFromPlayer;
    private void Start()
    {
        _nearFromPlayer = new NearFromPlayer(this.transform,this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Player.instance.HealthPlayer(healingPoints);
            this.gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        _nearFromPlayer.OnUpdate();
    }
}
