using System.Collections;
using UnityEngine;

public abstract class GunBase : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] protected Transform muzzleTransform;
    [SerializeField] protected GunScriptable gunStatsScriptable;

    // other dependencies
    protected GunController gunController;
    protected AmmoPool ammoPoolScript;

    [Header("Gun Stats")]
    [SerializeField] protected int ammoCurrent;
    [SerializeField] protected int ammoMax;
    [SerializeField] protected bool thisGunOnCooldown = false;
    [SerializeField] protected float timeBetweenShots;
    [SerializeField] protected float aimBloom;
    [SerializeField] protected float recoilIntensityX;
    [SerializeField] protected float recoilIntensityY;



    // Abstract Update, override inputs in children
    protected abstract void Update();
    // Awake with no Reload mechanics, override if necessary
    protected virtual void Awake()
    {
        ammoPoolScript = GetComponent<AmmoPool>();
        recoilScript = GetComponent<GunRecoilBase>();
        SetStatsFromScriptable();
        recoilScript.SetRecoilIntensityStat(recoilIntensityX, recoilIntensityY);
        ammoCurrent = ammoMax;
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }



    protected virtual void SetStatsFromScriptable()
    {
        timeBetweenShots = gunStatsScriptable.TimeBetweenShots;
        ammoMax = gunStatsScriptable.MaxAmmo;
        aimBloom = gunStatsScriptable.AimBloom;
        recoilIntensityX = gunStatsScriptable.RecoilIntensityX;
        recoilIntensityY = gunStatsScriptable.RecoilIntensityY;
    }
    // Sets GunController reference. Called from Guncontroller in Initialization
    public virtual void SetGunController(GunController gunCont)
    {
        gunController = gunCont;
    }



    // Return methods for GunController:

    // 1 These stay the same - virtual in case other changes needed
    public virtual int CurrentAmmo() { return ammoCurrent; }
    public virtual int MaxAmmo() { return ammoMax; }
    public virtual string ItemName() { return gunStatsScriptable.ItemName; }
    public virtual Sprite ItemSprite() { return gunStatsScriptable.ItemSprite; }

    // 2 These need override based on Reload system or other changes
    public virtual bool CanShoot()
    {
        if (gunController.CanControllerShoot() && !thisGunOnCooldown)
            { 
                //Debug.Log("CanShoot true, shooting"); 
                return true; 
            }
        else
            { return false; }
    }
    protected virtual bool GunEmpty() { return false; }
    public virtual bool GunReloading() { return false; }



    // Shoot - no need for overrides for now - virtual in case other changes needed
    protected virtual void ShootProjectile()
    {
        GameObject projectile = ammoPoolScript.RemoveObjectFromPool();
        SetProjectileTrajectory(projectile);
        projectile.SetActive(true);

        ammoCurrent--;
        gunController.UpdateAmmoCount();

        StartCoroutine(ShotCooldownTimer());
    }

    // Sets pos and rot of projectile, with aimBloom
    protected virtual void SetProjectileTrajectory(GameObject projectile)
    {
        float aimOffsetX = Random.Range(-aimBloom, aimBloom);
        //Debug.Log("aimOffsetX = " + aimOffsetX);
        float aimOffsetY = aimBloom - Mathf.Abs(aimOffsetX);
        aimOffsetY = Random.Range(-aimOffsetY, aimOffsetY);
        //Debug.Log("aimOffsetY = " + aimOffsetY);
        Quaternion newRotation = Quaternion.Euler(muzzleTransform.rotation.eulerAngles.x + aimOffsetX,
                                                  muzzleTransform.rotation.eulerAngles.y + aimOffsetY,
                                                  muzzleTransform.rotation.eulerAngles.z);


        projectile.transform.position = muzzleTransform.position;
        projectile.transform.localRotation = newRotation;
    }
    protected virtual void RecoilProc()
    {
        recoilScript.RecoilInstanceHappens();
    }


    protected IEnumerator ShotCooldownTimer()
    {
        thisGunOnCooldown = true;
        yield return new WaitForSeconds(timeBetweenShots);
        thisGunOnCooldown = false;
    }



    // Reload
    // Only CancelReload here because GunController requires it. 
    // If gun has Reload mechanic, overload this method and add Reload mechanic
    public abstract void CancelReload(); 
}
