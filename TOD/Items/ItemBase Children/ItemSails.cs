using UnityEngine;

[CreateAssetMenu(fileName = "Sails", menuName = "Items/Sailss")]
public class ItemSails : ItemBase
{
    [SerializeField] private float newItemSpeedMod;
    [SerializeField] private float newItemTurnRateMod;
    [SerializeField] private float newItemStoppingMod;
    [SerializeField] private SailsType itemType;


    public enum SailsType
    {
        Speedy,
        Heavy,
        Agile,
        Small,
        Medium,
        Large
    }


    private void OnEnable()
    {
        itemSpeedMod = newItemSpeedMod;
        itemTurnRateMod = newItemTurnRateMod;
        itemStoppingMod = newItemStoppingMod;
    }

    //public float ItemSpeedMod => itemSpeedMod;
    //public float ItemTurnRateMod => itemTurnRateMod;
    //public float ItemStoppingMod => itemStoppingMod;
    public SailsType Type => itemType;
}
