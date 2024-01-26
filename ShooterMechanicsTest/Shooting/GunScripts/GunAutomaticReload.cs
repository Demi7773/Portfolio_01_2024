using System.Collections;
using UnityEngine;

public class GunAutomaticReload : GunAutomatic
{
    [Header("Reload Stats")]
    // Added variables for reload mechanic, other stats inherited from GunBase
    [SerializeField] protected float reloadTime;
    [SerializeField] protected bool isReloading = false;



    // Override for GunBase Awake with added setting Reload mechanic values
    protected override void Awake()
    {
        base.Awake();

        reloadTime = gunStatsScriptable.ReloadTime;
    }

    // Override for GunBase Update for Automatic weapon with Reload mechanic
    protected override void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (CanShoot())
            {
                if (!GunEmpty())
                {
                    ShootProjectile();
                }
            }
            else if (GunEmpty() && !GunReloading() && gunController.CanControllerShoot())
            {
                ReloadGun();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gunController.CanControllerShoot() && ammoCurrent < ammoMax)
            {
                ReloadGun();
            }
        }
    }



    // Overriden return methods for GunController
    public override bool CanShoot()
    {
        if (!GunReloading())
        { return base.CanShoot(); }
        else
        { return false; }
    }
    protected override bool GunEmpty()
    {
        if (CurrentAmmo() > 0)
        { return false; }
        else
        { return true; }
    }
    public override bool GunReloading() { return isReloading; }



    // Reload
    protected virtual void ReloadGun()
    {
        StartCoroutine(ReloadTimer());
    }
    public override void CancelReload()
    {
        StopAllCoroutines();

        Debug.Log("Cancelling reload");
        isReloading = false;
    }
    IEnumerator ReloadTimer()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        ammoCurrent = ammoMax;
        gunController.UpdateAmmoCount();
    }
}
