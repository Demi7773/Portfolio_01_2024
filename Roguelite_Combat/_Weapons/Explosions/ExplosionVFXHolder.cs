using UnityEngine;
using UnityEngine.VFX;

public class ExplosionVFXHolder : MonoBehaviour
{
    [SerializeField] private VisualEffect explosionVFX;

    [SerializeField] private float timer;
    [SerializeField] private float duration;
    [SerializeField] private float radius;



    public void PlayAnimation(float duration, float radius)
    {
        explosionVFX.SetFloat("_Duration", duration);
        explosionVFX.SetFloat("_SpawnRadius", radius);
        explosionVFX.Play();
    }
}
