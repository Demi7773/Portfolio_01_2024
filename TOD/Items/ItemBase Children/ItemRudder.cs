using UnityEngine;

[CreateAssetMenu(fileName = "Rudder", menuName = "Items/Rudders")]
public class ItemRudder : ItemBase
{
    [SerializeField] protected float newItemSpeedMod;
    [SerializeField] protected float newItemTurnRateMod;
    [SerializeField] protected float newItemStoppingMod;
    //[SerializeField] protected float itemSpeedMod;
    //[SerializeField] protected float itemTurnRateMod;
    //[SerializeField] protected float itemStoppingMod;
    [SerializeField] protected RudderType itemType;


    private void OnEnable()
    {
        itemSpeedMod = newItemSpeedMod;
        itemTurnRateMod = newItemTurnRateMod;
        itemStoppingMod = newItemStoppingMod;
    }

    public enum RudderType
    {
        Speedy,
        Precise,
        Old
    }


    //public float ItemSpeedMod => itemSpeedMod;
    //public float ItemTurnRateMod => itemTurnRateMod;
    //public float ItemStoppingMod => itemStoppingMod;
    public RudderType Type => itemType;
}
