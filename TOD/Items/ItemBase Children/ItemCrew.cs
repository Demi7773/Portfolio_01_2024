using UnityEngine;

[CreateAssetMenu(fileName = "Crew", menuName = "Items/Crews")]
public class ItemCrew : ItemBase
{
    [SerializeField] protected float newItemReloadSpeedMod;
    [SerializeField] protected float newItemTurnRateMod;
    [SerializeField] protected float newItemSpeedMod;
    [SerializeField] protected float newItemHPMod;
    //[SerializeField] protected float itemReloadSpeedMod;
    //[SerializeField] protected float itemTurnRateMod;
    //[SerializeField] protected float itemSpeedMod;
    //[SerializeField] protected float itemHPMod;
    [SerializeField] protected CrewType itemType;

    private void OnEnable()
    {
        //Debug.Log("Test OnEnable in ScriptableObject ItemCrew");
        itemReloadSpeedMod = newItemReloadSpeedMod;
        itemTurnRateMod = newItemTurnRateMod;
        itemSpeedMod = newItemSpeedMod;
        itemHPMod = newItemHPMod;
    }


    public enum CrewType
    {
        Deckhands,
        Gunners,
        Navigators,
        Lookouts
    }


    public new float ItemReloadSpeedMod => itemReloadSpeedMod;
    public new float ItemTurnRateMod => itemTurnRateMod;
    public new float ItemSpeedMod => itemSpeedMod;
    public new float ItemHPMod => itemHPMod;
    public CrewType Type => itemType;
}
