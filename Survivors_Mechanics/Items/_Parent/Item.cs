using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected ItemType itemType;
    [SerializeField] protected ItemRarity itemRarity;
    [SerializeField] protected int itemPrice;

    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public ItemType ItemTypeReference => itemType;
    public ItemRarity ItemRarityReference => itemRarity;
    public int ItemPrice => itemPrice;




    public enum ItemType
    {
        Weapon,
        SpecialItem,
        ConsumableItem
    }
    public enum ItemRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}
