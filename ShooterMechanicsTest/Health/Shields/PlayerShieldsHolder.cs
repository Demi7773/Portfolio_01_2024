using UnityEngine;

public class PlayerShieldsHolder : MonoBehaviour
{
    [Header("ShieldScriptable and values it sets")]
    [SerializeField] private ShieldScriptable shieldStatsScriptable;
    [SerializeField] private PlayerShields playerShields;

    [SerializeField] private float maxShieldsValue;
    [SerializeField] private float timeBeforeRecharge;
    [SerializeField] private float timeBetweenRechargeTicks;
    [SerializeField] private float healShieldsValuePerRechargeTick;
    [SerializeField] private float healPerSecForUI;



    private void Awake()
    {
        playerShields = GetComponent<PlayerShields>();
        EquipNewShields(shieldStatsScriptable);
    }




    // Public method to set new scriptable object, called in Awake with default
    public void EquipNewShields(ShieldScriptable newShieldStatsScriptable)
    {
        shieldStatsScriptable = newShieldStatsScriptable;
        SetNewShieldValues();
    }

    // Sends new values to ShieldScriptable and for other references
    private void SetNewShieldValues()
    {
        maxShieldsValue = shieldStatsScriptable.MaxShieldsValue;
        timeBeforeRecharge = shieldStatsScriptable.TimeBeforeRecharge;
        timeBetweenRechargeTicks = shieldStatsScriptable.TimeBetweenRechargeTicks;
        healShieldsValuePerRechargeTick = shieldStatsScriptable.HealShieldsValuePerRechargeTick;
        healPerSecForUI = shieldStatsScriptable.HealPerSecForUI;


        playerShields.SetValuesFromShieldsHolder
            (maxShieldsValue, timeBeforeRecharge, timeBetweenRechargeTicks, 
             healShieldsValuePerRechargeTick, healPerSecForUI);
    }
}
