using UnityEngine;
public class TurretBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
//

    }

    // Update is called once per frame
    void Update()
    {
        var x = this.transform.GetChild(0);
        //print(x.name);
        if (GameManager.instance.player == null) return;
        x.transform.rotation = GameManager.instance.player.transform.rotation;
    }
}
