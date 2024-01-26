using UnityEngine;

public class ItemScriptable : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private int itemPrice;

    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public Sprite ItemSprite => itemSprite; 
    public int ItemPrice => itemPrice;
}
