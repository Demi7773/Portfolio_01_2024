using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField/*, Range(1, 3)*/] protected int itemTier = 1;
    [SerializeField] protected int itemPrice;
    [SerializeField] protected Sprite itemSprite;
    
    // Dodatne varijable za jednostavniju integraciju u shop. Po defaultu su 0 pa childovi overrideaju sta treba
    protected float itemHPMod = 0f;
    protected float itemDmgReducMod = 0f;
    protected float itemSpeedMod = 0f;
    protected float itemDmgMod = 0f;
    protected float itemReloadSpeedMod = 0f;
    protected float itemRangeMod = 0f;
    protected float itemTurnRateMod = 0f;
    protected float itemStoppingMod = 0f;

    public float ItemHPMod => itemHPMod;
    public float ItemDmgReducMod => itemDmgReducMod;
    public float ItemSpeedMod => itemSpeedMod;
    public float ItemDmgMod => itemDmgMod;
    public float ItemReloadSpeedMod => itemReloadSpeedMod;
    public float ItemRangeMod => itemRangeMod;
    public float ItemTurnRateMod => itemTurnRateMod;
    public float ItemStoppingMod => itemStoppingMod;
    //



    public string ItemName => itemName;
    public string ItemDescription => itemDescription;
    public int ItemTier => itemTier;
    public int ItemPrice => itemPrice;
    public Sprite ItemSprite => itemSprite;



    //[SerializeField] private ItemKind whatKindOfItem;

    //public enum ItemKind
    //{
    //    Cannon,
    //    Armor,
    //    Sails,
    //    Rudder,
    //    Crew,
    //    Flag
    //}

    //public ItemKind WhatKindOfItem => whatKindOfItem;
}
