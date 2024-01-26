using UnityEngine;

[CreateAssetMenu(fileName = "AmmoModule", menuName = "Modules/AmmoModule")]
public class AmmoModule : Module
{
    [Header("Ammo stats")]
    [SerializeField, Range(0f, 5f)] private float damageMod = 1f;



    [SerializeField, Range(0.1f, 10f)] private float shotsPerSecondMod = 1f;
    [SerializeField, Range(-10f, 10f)] private float ammoSpreadMod = 1f;



    // Reload Duration, positive is percent removed, negative is percent added
    [SerializeField, Range(-50f, 50f)] private float reloadDurationPercentRemove;


    [SerializeField, Range(0.1f, 10f)] private float ammoMaxMod = 1f;
    [SerializeField, Range(0.1f, 10f)] private float ammoTotalCapacityMod = 1f;

    [SerializeField, Range(0f, 100f)] private float ammoTotalRechargePerSecMod = 1f;


    [Header("Special settings")]
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool autoReloadInBackground = false;
    [SerializeField] private bool infiniteAmmo = false;

    public float DamageMod => damageMod;
    public float ShotsPerSecondMod => shotsPerSecondMod;
    public float AmmoSpreadMod => ammoSpreadMod;
    public float ReloadDurationPercentRemove => reloadDurationPercentRemove;
    public float AmmoMaxMod => ammoMaxMod;
    public float AmmoTotalCapacityMod => ammoTotalCapacityMod;
    public float AmmoTotalRechargePerSecMod => ammoTotalRechargePerSecMod;


    public AmmoType TypeOfAmmo => ammoType;
    public GameObject BulletPrefab => bulletPrefab;
    public bool AutoReloadInBackground => autoReloadInBackground;
    public bool InfiniteAmmo => infiniteAmmo;


    public enum AmmoType
    {
        Normal = 0,
        Explosive = 1,
        Fire = 2
    }
}
