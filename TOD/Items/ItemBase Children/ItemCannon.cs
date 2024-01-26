using UnityEngine;

[CreateAssetMenu(fileName = "Cannon", menuName = "Items/Cannons")]
public class ItemCannon : ItemBase
{
    [SerializeField] protected float newItemDmgMod;
    [SerializeField] protected float newItemReloadSpeedMod;
    [SerializeField] protected float newItemRangeMod;
    //[SerializeField] protected float itemDmgMod;
    //[SerializeField] protected float itemReloadSpeedMod;
    //[SerializeField] protected float itemRangeMod;
    [SerializeField] protected CannonType itemType;

    [SerializeField] private GameObject _CannonInstance;//Novo dodano


    private void OnEnable()
    {
        //Debug.Log("Test OnEnable in ScriptableObject ItemCannon");
        itemDmgMod = newItemDmgMod;
        itemReloadSpeedMod = newItemReloadSpeedMod;
        itemRangeMod = newItemRangeMod;
    }

    public enum CannonType
    {
        Balanced,
        LongRange,
        Speedy,
        Volley,
        Glass,
        Unstable
    }


    //public float ItemDmgMod => itemDmgMod;
    //public float ItemReloadSpeedMod => itemReloadSpeedMod;
    //public float ItemRangeMod => itemRangeMod;
    public CannonType Type => itemType;

    public GameObject CannonInstance => _CannonInstance;//Novo dodano
}