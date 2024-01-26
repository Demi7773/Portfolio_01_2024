using UnityEngine;

public class PassiveEffect : MonoBehaviour
{
    [SerializeField] private float currentTimer = 0.0f;
    [SerializeField, Range(1.0f, 60.0f)] private float effectCooldown = 1.0f;

    

    public virtual void TimeTick()
    {
        currentTimer += Time.deltaTime;
        if (currentTimer < effectCooldown) 
        {
            return;
        }
        else
        {
            ActivateEffectProc();
        }
    }
    protected virtual void ActivateEffectProc()
    {

    }
}
