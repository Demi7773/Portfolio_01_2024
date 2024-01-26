using UnityEngine;

[CreateAssetMenu(fileName = "AimModule", menuName = "Modules/AimModule")]
public class AimModule : Module
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
    [SerializeField] private bool autoAim = false;
    [SerializeField] private bool homingProjectiles = false;
    [SerializeField] private bool fullRandomDirections = false;
    [SerializeField] private AimType type;



    public float DamageMod => damageMod;
    public float ShotsPerSecondMod => shotsPerSecondMod;
    public float AmmoSpreadMod => ammoSpreadMod;
    public float ReloadDurationPercentRemove => reloadDurationPercentRemove;
    public float AmmoMaxMod => ammoMaxMod;
    public float AmmoTotalCapacityMod => ammoTotalCapacityMod;
    public float AmmoTotalRechargePerSecMod => ammoTotalRechargePerSecMod;



    public bool AutoAim => autoAim;
    public bool HomingProjectiles => homingProjectiles;
    public bool FullRandomDirections => fullRandomDirections;

    public AimType Type => type;
    
    public enum AimType
    {
        Normal,
        Auto,
        Homing,
        FullRandom
    }





    private void OnValidate()
    {
        if (type == AimType.Normal)
        {
            autoAim = false;
            homingProjectiles = false;
            fullRandomDirections = false;
        }
        else if (type == AimType.Auto)
        { 
            autoAim = true;
            homingProjectiles = false;
            fullRandomDirections = false;
        }
        else if (type == AimType.Homing)
        {
            autoAim = false;
            homingProjectiles = true;
            fullRandomDirections = false;
        }
        else if (type == AimType.FullRandom)
        {
            autoAim = false;
            homingProjectiles = false;
            fullRandomDirections = true;
        }
    }
}
