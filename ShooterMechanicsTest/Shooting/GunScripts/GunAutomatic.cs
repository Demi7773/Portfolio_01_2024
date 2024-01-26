using UnityEngine;

public class GunAutomatic : GunBase
{
    // Override for GunBase Update for Automatic weapon
    protected override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (CanShoot())
            {
                ShootProjectile();
            }
        }
    }

    // Empty CancelReload for GunController
    public override void CancelReload()
    {
        return;
    }
}
