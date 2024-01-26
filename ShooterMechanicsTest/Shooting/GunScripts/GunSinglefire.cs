using UnityEngine;

public class GunSinglefire : GunBase
{
    // Override for GunBase Update for Singlefire gun
    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CanShoot())
            {
                ShootProjectile();
            }
        }
    }

    // Empty CancelReload, method needed for GunController
    public override void CancelReload()
    {
        return;
    }
}
