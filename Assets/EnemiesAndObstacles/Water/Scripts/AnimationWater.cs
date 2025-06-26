using UnityEngine;
public class AnimationWater
{
    private Vector2 _animVector;
    private Renderer renderer;
    private Vector2 _animSpeed;
    public AnimationWater(Renderer renderer)
    {
        this.renderer = renderer;
        _animSpeed = new Vector2(0.3f,0.3f);
        renderer.material.SetTextureScale("_MainTex", new Vector2(1,1));
    }
    public void OnUpdate()
    {
        _animVector = new Vector2(_animVector.x + _animSpeed.x * Time.deltaTime, _animVector.y + _animSpeed.y * Time.deltaTime);
        renderer.material.mainTextureOffset = _animVector;
    }
}
