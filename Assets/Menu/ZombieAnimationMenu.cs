using UnityEngine;
public class ZombieAnimationMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
}
