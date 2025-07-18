using System.Collections;
using UnityEngine;
public class itemXPBehavior : MonoBehaviour
{
    public float points = 100f;
    private NearFromPlayer _nearFromPlayer;
    public float distance = 6;

    private Renderer _renderer;
    private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        if (_renderer != null)
        {
            _originalColor = _renderer.material.color;
        }
    }

    private void OnEnable()
    {
        // Asegurarse de que _renderer no esté null
        if (_renderer == null)
        {
            _renderer = GetComponentInChildren<Renderer>();
            if (_renderer != null)
            {
                _originalColor = _renderer.material.color;
            }
        }

        if (_renderer != null)
        {
            _renderer.material.color = _originalColor;
            StartCoroutine(Dissapear());
        }
    }

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

    public IEnumerator Dissapear()
    {
        float fadeDuration = 10f;
        float fadeDelay = 0.2f;
        Color color = _renderer.material.color;

        // Fade gradual hasta volverse invisible
        for (float t = 0f; t < fadeDuration; t += fadeDelay)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            color.a = alpha;
            _renderer.material.color = color;
            yield return new WaitForSeconds(fadeDelay);
        }

        // Asegurar alpha en 0
        color.a = 0f;
        _renderer.material.color = color;

        // Desactivar objeto
        this.gameObject.SetActive(false);
    }


}
