using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    // DEPENDENCIES
    private GunBase currentGunScript;
    private PlayerUI playerUIScript;

    [Header("Guns and Current Equipped")]
    [SerializeField] private GameObject currentGun;
    [SerializeField] private List<GameObject> availableGunsList;
    [SerializeField] private int currentGunIndex;

    [Header("Current Weapon Stats")]
    [SerializeField] private int ammoCurrent;
    [SerializeField] private int ammoMax;

    [Header("Controller Stats")]
    [SerializeField] private bool canControllerShoot = true;
    [SerializeField] private float swapWeaponTime = 0.5f;



    public bool CanControllerShoot() { return canControllerShoot; }


    private void Start()
    {
        playerUIScript = GetComponentInChildren<PlayerUI>();
        InitializeAllGuns();
    }

    // Weapon Swap controls, shoot and reload moved to Gun scripts for custom inputs
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentGunScript.CanShoot())
            {
                SetGun(0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentGunScript.CanShoot())
            {
                SetGun(1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentGunScript.CanShoot())
            {
                SetGun(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (currentGunScript.CanShoot())
            {
                SetGun(3);
            }
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }



    // Setup for All and Current Gun
    private void InitializeAllGuns()
    {
        for (int i = 0; i < availableGunsList.Count; i++)
        {
            availableGunsList[i].SetActive(true);
            availableGunsList[i].GetComponent<GunBase>().SetGunController(this);
        }
        currentGunScript = availableGunsList[0].GetComponent<GunBase>();
        SetGun(0);
    }
    private void SetGun(int indexInAvailableGunsList)
    {
        if (currentGunScript.GunReloading())
        {
            currentGunScript.CancelReload();
        }

        for (int i = 0; i < availableGunsList.Count;  i++)
        {
            availableGunsList[i].SetActive(false);
        }
        currentGunIndex = indexInAvailableGunsList;
        currentGun = availableGunsList[indexInAvailableGunsList];
        currentGun.SetActive(true);
        currentGunScript = currentGun.GetComponent<GunBase>();


        UpdateAmmoCount();
        GetItemImgAndName();
        StartCoroutine(SwapWeaponsDisableActionTimer());
    }
    IEnumerator SwapWeaponsDisableActionTimer()
    {
        canControllerShoot = false;
        yield return new WaitForSeconds(swapWeaponTime);
        canControllerShoot = true;
    }



    // Get ammo count + Set UIs
    private void GetItemImgAndName()
    {
        playerUIScript.SetGunNameAndSprite(currentGunScript.ItemName(), currentGunScript.ItemSprite());
    }
    public void UpdateAmmoCount()
    {
        ammoCurrent = currentGunScript.CurrentAmmo();
        ammoMax = currentGunScript.MaxAmmo();
        playerUIScript.SetAmmoUI(ammoCurrent, ammoMax);
    }
}
