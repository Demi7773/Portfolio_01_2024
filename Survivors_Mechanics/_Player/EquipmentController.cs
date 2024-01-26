using UnityEngine;
using static PlayerEvents;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] private WeaponBodyModule equippedWeaponBodyModule;
    [SerializeField] private AmmoModule equippedAmmoModule;
    [SerializeField] private AimModule equippedAimModule;
    [SerializeField] private MuzzleModule equippedMuzzleModule;

    public WeaponBodyModule WeaponBodyModule => equippedWeaponBodyModule;
    public AmmoModule AmmoModule => equippedAmmoModule;
    public AimModule AimModule => equippedAimModule;
    public MuzzleModule MuzzleModule => equippedMuzzleModule;



        // Not used
    //[SerializeField] private GameObject player;
    //[SerializeField] private WeaponManager weaponManager;





    private void OnEnable()
    {
        PlayerEvents.NeedPlayerEquipmentReference += EquipmentRefreshReference;
    }
    private void OnDisable()
    {
        PlayerEvents.NeedPlayerEquipmentReference -= EquipmentRefreshReference;
    }
    private void Start()
    {
        EquipmentRefreshReference();
    }



    public void EquipNewModule(Module module)
    {
        if (module is WeaponBodyModule)
        {
            equippedWeaponBodyModule = module as WeaponBodyModule;
        }
        else if (module is AmmoModule)
        {
            equippedAmmoModule = module as AmmoModule;
        }
        else if (module is AimModule)
        {
            equippedAimModule = module as AimModule;
        }
        else if (module is MuzzleModule)
        {
            equippedMuzzleModule = module as MuzzleModule;
        }

        EquipmentRefreshReference();
    }
    private void EquipmentRefreshReference(/*WeaponBodyModule newWeaponBodyModule, AmmoModule newAmmoModule, AimModule newAimModule, MuzzleModule newMuzzleModule*/)
    {
        Debug.Log("Equipment Reference Ping");
        PlayerEvents.EquipmentReference?.Invoke(new PlayerEquipmentReference(this)/*NewModules(newWeaponBodyModule, newAmmoModule, newAimModule, newMuzzleModule)*/);

        //weaponManager.NewModulesUpdate(newWeaponBodyModule, newAmmoModule, newAimModule, newMuzzleModule);
    }
}
