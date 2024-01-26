using UnityEngine;

[CreateAssetMenu(fileName = "MuzzleModule", menuName = "Modules/MuzzleModule")]
public class MuzzleModule : Module
{
    [Header("Stats")]
    [SerializeField, Range(0f, 5f)] private float damageMod = 1f;
    [SerializeField, Range(0.1f, 10f)] private float shotsPerSecondMod = 1f;
    [SerializeField, Range(-10f, 10f)] private float ammoSpreadMod = 1f;
    // Reload Duration, positive is percent removed, negative is percent added
    [SerializeField, Range(-50f, 50f)] private float reloadDurationPercentRemove;
    [SerializeField, Range(0.1f, 10f)] private float ammoMaxMod = 1f;

    [Header("Not implemented, can leave empty")]
    [SerializeField, Range(0.1f, 10f)] private float ammoTotalCapacityMod = 1f;
    [SerializeField, Range(0f, 100f)] private float ammoTotalRechargePerSecMod = 1f;


    [Header("Module Unique Stats")]
    [SerializeField] private ShootType shootType;




    public float DamageMod => damageMod;
    public float ShotsPerSecondMod => shotsPerSecondMod;
    public float AmmoSpreadMod => ammoSpreadMod;
    public float ReloadDurationPercentRemove => reloadDurationPercentRemove;
    public float AmmoMaxMod => ammoMaxMod;
    public float AmmoTotalCapacityMod => ammoTotalCapacityMod;
    public float AmmoTotalRechargePerSecMod => ammoTotalRechargePerSecMod;


    public enum ShootType
    {
        Basic = 0,
        Spread = 1,
        Snipe = 2,
        Spray = 3,
        Beam = 4
    }

}
