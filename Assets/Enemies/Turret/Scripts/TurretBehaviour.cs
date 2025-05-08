using UnityEngine;
public class TurretBehaviour : MonoBehaviour
{
    [SerializeField] private Vector3 _dirRotVector;
    [SerializeField] private Quaternion _dirRotQuaternion;
    [SerializeField] private Transform _child;
    void Start()
    {
        _child = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_child == null) return;
        _dirRotVector = GameManager.instance.player.transform.position - this.transform.position;
        if (GameManager.instance.player == null) return;
        _dirRotQuaternion = Quaternion.LookRotation(_dirRotVector);
        _child.transform.rotation = _dirRotQuaternion;
    }
}
