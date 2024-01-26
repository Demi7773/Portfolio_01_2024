using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armors")]
public class ItemArmor : ItemBase
{
    [SerializeField] protected float newItemHPMod;
    [SerializeField] protected float newItemDmgReducMod;
    [SerializeField] protected float newItemSpeedMod;
    //[SerializeField] protected float itemHPMod;
    //[SerializeField] protected float itemDmgReducMod;
    //[SerializeField] protected float itemSpeedMod;
    [SerializeField] protected ArmorType itemType;


    private void OnEnable()
    {
        //Debug.Log("Test OnEnable in ScriptableObject ItemArmor");
        itemHPMod = newItemHPMod;
        itemDmgReducMod = newItemDmgReducMod;
        itemSpeedMod = newItemSpeedMod;
    }

    public enum ArmorType
    {
        Light,
        Balanced,
        Heavy,
        Speedy,
        Trickster
    }


    //public float ItemHPMod => itemHPMod;
    //public float ItemDmgReducMod => itemDmgReducMod;
    //public float ItemSpeedMod => itemSpeedMod;
    public ArmorType Type => itemType;
}
