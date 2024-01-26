using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Shields")]
public class ShieldScriptable : ItemScriptable
{
    [SerializeField] private float maxShiedsValue;
    [SerializeField] private float timeBeforeRecharge;
    [SerializeField] private float timeBetweenRechargeTicks;
    [SerializeField] private float healShieldsValuePerRechargeTick;
    [SerializeField] private float healPerSecForUI;

    public float MaxShieldsValue => maxShiedsValue;
    public float TimeBeforeRecharge => timeBeforeRecharge;
    public float TimeBetweenRechargeTicks => timeBetweenRechargeTicks;
    public float HealShieldsValuePerRechargeTick => healShieldsValuePerRechargeTick;
    public float HealPerSecForUI => (1 / timeBetweenRechargeTicks) * healShieldsValuePerRechargeTick;
}
