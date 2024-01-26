using UnityEngine;

public class SawbladeDamage : MonoBehaviour
{
    [SerializeField] protected Collider wielderCollider;
    [SerializeField] protected GameObject bladeModel;
    [SerializeField] protected LayerMask damageTargets;
    [SerializeField] protected float radius = 1.0f;
    [SerializeField] protected float damage = 0.0f;
    [Space(10)]
    [SerializeField] private float bladeRotationSpeed = 1.0f;
    [Space(10)]
    [Header("Settings")]
    [SerializeField] private float damageSetting1 = 2.0f;
    [SerializeField] private float damageSetting2 = 5.0f;
    [SerializeField] private float damageSetting3 = 10.0f;
    [Space(10)]
    [SerializeField] private float bladesRotationSpeed1 = 1.0f;
    [SerializeField] private float bladesRotationSpeed2 = 10.0f;
    [SerializeField] private float bladesRotationSpeed3 = 100.0f;



    public void SetRotationSpeed(int speedSetting)
    {
        if (speedSetting < 1)
            speedSetting = 1;
        if (speedSetting > 3)
            speedSetting = 3;

        switch (speedSetting)
        {
            case 1:
                bladeRotationSpeed = bladesRotationSpeed1;
                damage = damageSetting1;
                break;
            case 2:
                bladeRotationSpeed = bladesRotationSpeed2;
                damage = damageSetting2;
                break;
            case 3:
                bladeRotationSpeed = bladesRotationSpeed3;
                damage = damageSetting3;
                break;
        }

        Debug.Log("Rotation speed settings changed to " + speedSetting);
    }

    protected virtual void Update()
    {
        CheckForHits();
        bladeModel.transform.Rotate(0.0f, bladeRotationSpeed * Time.deltaTime, 0.0f);
    }

    private void CheckForHits()
    {
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, radius, damageTargets);

        foreach (Collider target in hitTargets)
        {
            if (target == wielderCollider)
            {
                continue;
            }

            EffectOnHitTarget(target);
        }
    }
    protected virtual void EffectOnHitTarget(Collider hitTarget)
    {
        IDamageable damageable = hitTarget.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.GetHitFor(damage);
        }
        else
        {
            Debug.Log("IDamageable null on hitTarget!");
        }
    }



    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, radius);
    }
}
