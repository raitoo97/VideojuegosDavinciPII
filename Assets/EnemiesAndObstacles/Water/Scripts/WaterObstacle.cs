using UnityEngine;
public class WaterObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent<Player>(out var player))
        {

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.TryGetComponent<Player>(out var player))
        {

        }
    }
}
