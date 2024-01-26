using UnityEngine;

[CreateAssetMenu(fileName = "StartingItems", menuName = "Scriptables/StartingItems")]
public class StartingItemsHolder : ScriptableObject
{
    [Header("Starting Items")]
    [SerializeField] private ItemCannon cannons;
    [SerializeField] private ItemArmor armor;
    [SerializeField] private ItemSails sails;
    [SerializeField] private ItemRudder rudder;
    [SerializeField] private ItemCrew crew;


    public ItemCannon StartingCannons => cannons;
    public ItemArmor StartingArmor => armor;
    public ItemSails StartingSails => sails;
    public ItemRudder StartingRudder => rudder;
    public ItemCrew StartingCrew => crew;
}
