using UnityEngine;

public class ProjectilePhysics : ProjectileScriptBase
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        rb.velocity = transform.forward * projectileSpeed;
        Debug.Log("Velocity: " + rb.velocity);
    }
    protected override void OnDisable()
    {
        rb.velocity = Vector3.zero;
        Debug.Log("Velocity: " + rb.velocity + " (should be 0,0,0)");
        base.OnDisable();
    }
}
