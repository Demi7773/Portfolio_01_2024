using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentController", menuName = "Scriptables/EquipmentController")]
public class EquipmentController : ScriptableObject
{
    //[Header("References")]
    //[SerializeField] private ShipPartsHolder shipPartsHolder;
    [Header("Starting Items")]
    [SerializeField] private StartingItemsHolder startingItems;

    [Header("Equipped Items")]
    [SerializeField] private ItemCannon cannons;
    [SerializeField] private ItemArmor armor;
    [SerializeField] private ItemSails sails;
    [SerializeField] private ItemRudder rudder;
    [SerializeField] private ItemCrew crew;
    //[SerializeField] private ItemFlag flag;

    [Header("Base values before multipliers")]
    [SerializeField] private float baseHP;
    [SerializeField] private float baseDmgReduction;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseTurnRate;
    [SerializeField] private float baseStopping;
    [SerializeField] private float baseDmg;
    [SerializeField] private float baseReloadSpeed;
    [SerializeField] private float baseRange;


    [Header("Final values after calculation")]
    [SerializeField] private float playerHPStat;
    [SerializeField] private float playerDmgReductionStat;
    [SerializeField] private float playerSpeedStat;
    [SerializeField] private float playerTurnStat;
    [SerializeField] private float playerStoppingStat;
    [SerializeField] private float playerDmgStat;
    [SerializeField] private float playerReloadSpeedStat;
    [SerializeField] private float playerRangeStat;

    //[SerializeField] private int playerDiplomacyLvl;
    //[SerializeField] private int playerEncountersMod;


    // public references to equipped Items:
    public ItemCannon EquippedCannons => cannons;
    public ItemArmor EquippedArmor => armor;
    public ItemSails EquippedSails => sails;
    public ItemRudder EquippedRudder => rudder;
    public ItemCrew EquippedCrew => crew;

    //public ItemFlag EquippedFlag => flag;


    // public variables for reference:
    public float PlayerHPStat => playerHPStat;
    public float PlayerDmgReducStat => playerDmgReductionStat;
    public float PlayerSpeedStat => playerSpeedStat;
    public float PlayerTurnStat => playerTurnStat;
    public float PlayerStoppingStat => playerStoppingStat;
    public float PlayerDmgStat => playerDmgStat;
    public float PlayerReloadSpeedStat => playerReloadSpeedStat;
    public float PlayerRangeStat => playerRangeStat;

    public ItemCannon PlayerCannon => cannons;

    //public int PlayerDiplomacyLvl => playerDiplomacyLvl;
    //public int PlayerEncountersMod => playerEncountersMod;


    // Sets all value modifiers for allItems equipped. Use Public Get methods below to read current values for reference
    // Currently called from PlayerHPScript to initialize - figure out better system
    public void CalculateModifiers()
    {
        // calculation for modifiers from allItems
        // NOVA VERZIJA - kak je Tuna predlozio, sa jednostavnim zbrajanjem statsa
        float hpMod = armor.ItemHPMod + crew.ItemHPMod;
        float dmgReducMod = armor.ItemDmgReducMod;
        float speedMod = sails.ItemSpeedMod + armor.ItemSpeedMod + crew.ItemSpeedMod + rudder.ItemSpeedMod /** helm.ItemSpeedMod*/;
        float turnMod = sails.ItemTurnRateMod + rudder.ItemTurnRateMod + crew.ItemTurnRateMod /** helm.ItemTurnRateMod*/;
        float stoppingMod = sails.ItemStoppingMod  + rudder.ItemStoppingMod /** helm.ItemStoppingMod*/;
        float dmgMod = cannons.ItemDmgMod /** ammoType.ItemDmgMod*/;
        float reloadSpeedMod = cannons.ItemReloadSpeedMod + crew.ItemReloadSpeedMod /** ammoType.ItemReloadSpeedMod*/;
        float rangeMod = cannons.ItemRangeMod /** ammoType.ItemRangeMod*/;

        // applies modifiers to base stats
        playerHPStat = baseHP + hpMod;
        playerDmgReductionStat = baseDmgReduction + dmgReducMod;
        if (cannons.Type == ItemCannon.CannonType.Glass)
        {
            playerDmgReductionStat = playerDmgReductionStat * 0.5f;             // TEMP FOR GLASSCANNON
        }
        playerSpeedStat = baseSpeed + speedMod;
        playerTurnStat = baseTurnRate + turnMod;
        playerStoppingStat = baseStopping + stoppingMod;
        playerDmgStat = baseDmg + dmgMod;
        playerReloadSpeedStat = baseReloadSpeed + reloadSpeedMod;
        playerRangeStat = baseRange + rangeMod;

        PlayerEvents.NewEquipment?.Invoke();
        HUDEvents.HUDInventoryUpdateEvent?.Invoke(new HUDInventoryUpdateEventData(cannons.ItemSprite, armor.ItemSprite, sails.ItemSprite, rudder.ItemSprite, crew.ItemSprite));
    }
    // STARA VERZIJA SA MNOZENJEM
    //public void CalculateModifiers_LegacyWithMultipliers()
    //{
    //    // calculation for modifiers from allItems
    //    float hpMod = armor.ItemHPMod * crew.ItemHPMod;
    //    float dmgReducMod = armor.ItemDmgReducMod;
    //    float speedMod = sails.ItemSpeedMod * armor.ItemSpeedMod * crew.ItemSpeedMod * rudder.ItemSpeedMod /** helm.ItemSpeedMod*/;
    //    float turnMod = sails.ItemTurnRateMod * rudder.ItemTurnRateMod * crew.ItemTurnRateMod /** helm.ItemTurnRateMod*/;
    //    float stoppingMod = sails.ItemStoppingMod * rudder.ItemStoppingMod /** helm.ItemStoppingMod*/;
    //    float dmgMod = cannons.ItemDmgMod /** ammoType.ItemDmgMod*/;
    //    float reloadSpeedMod = cannons.ItemReloadSpeedMod * crew.ItemReloadSpeedMod /** ammoType.ItemReloadSpeedMod*/;
    //    float rangeMod = cannons.ItemRangeMod /** ammoType.ItemRangeMod*/;

    //    // applies modifiers to base stats
    //    playerHPStat = baseHP * hpMod;
    //    playerDmgReductionStat = baseDmgReduction * dmgReducMod;
    //    playerSpeedStat = baseSpeed * speedMod;
    //    playerTurnStat = baseTurnRate * turnMod;
    //    playerStoppingStat = baseStopping * stoppingMod;
    //    playerDmgStat = baseDmg * dmgMod;
    //    playerReloadSpeedStat = baseReloadSpeed * reloadSpeedMod;
    //    playerRangeStat = baseRange * rangeMod;

    //    //playerDiplomacyLvl = flag.DiplomacyLvl;
    //    //playerEncountersMod = flag.RandomEncountersAvailable;

    //    HUDEvents.HUDInventoryUpdateEvent?.Invoke(new HUDInventoryUpdateEventData(cannons.ItemSprite, armor.ItemSprite, sails.ItemSprite, rudder.ItemSprite, crew.ItemSprite));

    //}



    public void EquipAllStartingItems()
    {
        EquipNewCannon(startingItems.StartingCannons);
        EquipNewArmor(startingItems.StartingArmor);
        EquipNewSails(startingItems.StartingSails);
        EquipNewRudder(startingItems.StartingRudder);
        EquipNewCrew(startingItems.StartingCrew);
        Debug.Log("STARTING ITEMS EQUIPPED");
        //CalculateModifiers();
    }


    public void DetermineItemTypeAndEquipNewItem(ItemBase item)
    {
        if (item is ItemCannon)
        {
            EquipNewCannon(item as ItemCannon);
        }
        else if (item is ItemArmor)
        {
            EquipNewArmor(item as ItemArmor);
        }
        else if (item is ItemSails)
        {
            EquipNewSails(item as ItemSails);
        }
        else if (item is ItemRudder)
        {
            EquipNewRudder(item as ItemRudder);
        }
        else if (item is ItemCrew)
        {
            EquipNewCrew(item as ItemCrew);
        }
        //else if (item is ItemFlag)
        //{
        //    EquipNewFlag(item as ItemFlag);
        //}
        else
        {
            Debug.Log("Item is not valid type (???)");
        }

        
        //HUDEvents.HUDInventoryUpdateEvent?.Invoke(new HUDInventoryUpdateEventData
        //    (cannons.ItemSprite, armor.ItemSprite, sails.ItemSprite, rudder.ItemSprite, crew.ItemSprite));
    }

    public void EquipNewCannon(ItemCannon newCannon)
    {
        cannons = newCannon;
        CalculateModifiers();
    }
    public void EquipNewArmor(ItemArmor newArmor)
    {
        armor = newArmor;
        CalculateModifiers();
    }
    public void EquipNewSails(ItemSails newSails)
    {
        sails = newSails;
        CalculateModifiers();
    }
    public void EquipNewRudder(ItemRudder newRudder)
    {
        rudder = newRudder;
        CalculateModifiers();
    }
    public void EquipNewCrew(ItemCrew newCrew)
    {
        crew = newCrew;
        CalculateModifiers();
    }
    //public void EquipNewFlag(ItemFlag newFlag)
    //{
    //    flag = newFlag;
    //}




    // REMOVED - here in case we have separate models for different states of ship

    // Needs to be called somewhere (UI, event?)
    // Missing Item index. Myb have a List of all potential allItems here in same order? or figure out better system
    // myb change ShipPartsHolder reference?
    //public void SetShipPartsAccordingToEquipped()
    //{
    //    //shipPartsHolder.SetShipBodyMesh();
    //    //shipPartsHolder.SetShipSailsMesh();
    //    //shipPartsHolder.SetShipFlagMesh();
    //}
}
