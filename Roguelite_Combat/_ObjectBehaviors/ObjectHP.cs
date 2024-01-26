using UnityEngine;

public class ObjectHP : MonoBehaviour, IDamageable
{
    [SerializeField] protected float currentHP = 1.0f;
    [SerializeField] protected float maxHP = 1.0f;




    protected virtual void OnEnable()
    {
        currentHP = maxHP;
    }
    // add sfx and vfx
    public virtual void GetHitFor(float amount)
    {
        float newHP = currentHP - amount;
        newHP = Mathf.Clamp(newHP, 0, maxHP);

        currentHP = newHP;

        if (currentHP <= 0.0f)
        {
            DestroyObject();
        }
    }
        // add sfx and vfx
    protected virtual void DestroyObject()
    {
        //Debug.Log("Object Destroyed");
        gameObject.SetActive(false);
    }
}
