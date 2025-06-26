using System.Collections;
using UnityEngine;
public class WaterObstacle : Obstacles
{
    private AnimationWater _anim;
    private void OnEnable()
    {
        _anim = new AnimationWater(this.GetComponent<Renderer>());
    }
    private void Update()
    {
        _anim.OnUpdate();
    }
    protected override void ActionOntriggerEnter()
    {
        print("sdsdsds");
    }

    protected override void ActionOntriggerExitr()
    {
        print("sdsdsds");
    }
    protected override IEnumerator ActionCoroutine()
    {

        yield return null;
        print("sdsdsds");
    }
}
