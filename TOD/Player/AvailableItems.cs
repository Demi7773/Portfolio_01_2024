using System.Collections.Generic;
using UnityEngine;

public class AvailableItems : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private EquipmentController playerEquipment;
    [Header("All Items")]
    //[SerializeField] private List<ItemBase> allItems = new();
    [SerializeField] private List<ItemCannon> allCannons = new();
    [SerializeField] private List<ItemArmor> allArmors = new();
    [SerializeField] private List<ItemSails> allSails = new();
    [SerializeField] private List<ItemRudder> allRudders = new();
    [SerializeField] private List<ItemCrew> allCrews = new();
    //[SerializeField] private List<ItemFlag> allFlags = new();

    [Header("Currently Available Items")]
    [SerializeField] private List<ItemBase> currentlyAvailableItems = new();
    [SerializeField] private List<ItemBase> currentlyAvailableTier1 = new();
    [SerializeField] private List<ItemBase> currentlyAvailableTier2 = new();
    [SerializeField] private List<ItemBase> currentlyAvailableTier3 = new();
    public List<ItemBase> CurrentlyAvailableItems => currentlyAvailableItems;

    // Here for testing
    [SerializeField] public List<ItemBase> endOfLevelReward = new();
    [SerializeField] public List<ItemBase> shopContents = new();




    private void OnEnable()
    {
        PlayerEvents.LevelStart += NewEndOfLevelRewards;
        PlayerEvents.ShopLevelStart += NewShopContents;
    }
    private void OnDisable()
    {
        PlayerEvents.LevelStart -= NewEndOfLevelRewards;
        PlayerEvents.ShopLevelStart += NewShopContents;
    }



    // Zasad na F za test
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //endOfLevelReward = GenerateLevelRewardsListFromAvailable();
            NewShopContents();
            NewEndOfLevelRewards();
        }
    }
    private void NewShopContents()
    {
        shopContents.Clear();
        shopContents = GenerateShopContentsListFromAvailable();
    }
    private void NewEndOfLevelRewards()
    {
        endOfLevelReward.Clear();
        endOfLevelReward = GenerateLevelRewardsListFromAvailable();
    }

    

    private void CurrentlyAvailableUpdate()
    {
        List<ItemCannon> availableCannons = AvailableCannons();
        List<ItemArmor> availableArmors = AvailableArmors();
        List<ItemSails> availableSails = AvailableSails();
        List<ItemRudder> availableRudders = AvailableRudders();
        List<ItemCrew> availableCrews = AvailableCrews();

        for (int i = 0; i < availableCannons.Count; i++)
        {
            currentlyAvailableItems.Add(availableCannons[i]);
        }
        for (int i = 0; i < availableArmors.Count; i++)
        {
            currentlyAvailableItems.Add(availableArmors[i]);
        }
        for (int i = 0; i < availableSails.Count; i++)
        {
            currentlyAvailableItems.Add(availableSails[i]);
        }
        for(int i = 0; i < availableRudders.Count; i++)
        {
            currentlyAvailableItems.Add(availableRudders[i]);
        }
        for (int i = 0; i < availableCrews.Count; i++)
        {
            currentlyAvailableItems.Add(availableCrews[i]);
        }

        AvailableByTierUpdate();
    }

    private List<ItemCannon> AvailableCannons()
    {
        List<ItemCannon> available = new List<ItemCannon>();

        ItemCannon equipped = playerEquipment.EquippedCannons;

        foreach (ItemCannon item in allCannons)
        {
            if (item.Type != equipped.Type)
            {
                available.Add(item);
            }
            else if (item.ItemTier > equipped.ItemTier)
            {
                available.Add(item);
            }
            //else
            //{
            //    Debug.Log("Armor of same type and equal or lower tier, skipping " + item.ItemName);
            //}
        }
        //for (int i = 0; i < allCannons.Count; i++)
        //{
        //    ItemCannon item = allCannons[i];
        //    if (item.Type != equipped.Type)
        //    {
        //        available.Add(item);
        //        Debug.Log("Different type, adding");
        //    }
        //    else if (item.ItemTier > equipped.ItemTier)
        //    {
        //        available.Add(item);
        //        Debug.Log("Same type but higher Tier, adding");
        //    }
        //    else
        //    {
        //        Debug.Log("Cannons of same type and equal or lower tier, skipping: " + item.ItemName);
        //    }
        //}

        return available;
    }
    private List<ItemArmor> AvailableArmors()
    {
        List<ItemArmor> available = new List<ItemArmor>();

        ItemArmor equipped = playerEquipment.EquippedArmor;

        foreach (ItemArmor item in allArmors)
        {
            if (item.Type != equipped.Type)
            {
                available.Add(item);
            }
            else if (item.ItemTier > equipped.ItemTier)
            {
                available.Add(item);
            }
            //else
            //{
            //    Debug.Log("Armor of same type and equal or lower tier, skipping " + item.ItemName);
            //}
        }
        //for (int i = 0; i < allArmors.Count; i++)
        //{
        //    ItemArmor item = allArmors[i];
        //    if (item.Type != equipped.Type)
        //    {
        //        available.Add(item);
        //    }
        //    else if (item.ItemTier > equipped.ItemTier)
        //    {
        //        available.Add(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Armor of same type and equal or lower tier, skipping " + item.ItemName);
        //    }
        //}

        return available;
    }
    private List<ItemSails> AvailableSails()
    {
        List<ItemSails> available = new List<ItemSails>();

        ItemSails equipped = playerEquipment.EquippedSails;

        foreach (ItemSails item in allSails)
        {
            if (item.Type != equipped.Type)
            {
                available.Add(item);
            }
            else if (item.ItemTier > equipped.ItemTier)
            {
                available.Add(item);
            }
            //else
            //{
            //    Debug.Log("Sails of same type and equal or lower tier, skipping " + item.ItemName);
            //}
        }
        //for (int i = 0; i < allSails.Count; i++)
        //{
        //    ItemSails item = allSails[i];
        //    if (item.Type != equipped.Type)
        //    {
        //        available.Add(item);
        //    }
        //    else if (item.ItemTier > equipped.ItemTier)
        //    {
        //        available.Add(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Sails of same type and equal or lower tier, skipping " + item.ItemName);
        //    }
        //}

        return available;
    }
    private List <ItemRudder> AvailableRudders() 
    {
        List<ItemRudder> available = new List<ItemRudder>();

        ItemRudder equipped = playerEquipment.EquippedRudder;

        foreach (ItemRudder item in allRudders)
        {
            if (item.Type != equipped.Type)
            {
                available.Add(item);
            }
            else if (item.ItemTier > equipped.ItemTier)
            {
                available.Add(item);
            }
            //else
            //{
            //    Debug.Log("Rudder of same type and equal or lower tier, skipping " + item.ItemName);
            //}
        }
        //for (int i = 0; i < allRudders.Count; i++)
        //{
        //    ItemRudder item = allRudders[i];
        //    if (item.Type != equipped.Type)
        //    {
        //        available.Add(item);
        //    }
        //    else if (item.ItemTier > equipped.ItemTier)
        //    {
        //        available.Add(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Rudder of same type and equal or lower tier, skipping " + item.ItemName);
        //    }
        //}

        return available;
    }
    private List<ItemCrew> AvailableCrews()
    {
        List<ItemCrew> available = new List<ItemCrew>();

        ItemCrew equipped = playerEquipment.EquippedCrew;

        foreach (ItemCrew item in allCrews)
        {
            if (item.Type != equipped.Type)
            {
                available.Add(item);
            }
            else if (item.ItemTier > equipped.ItemTier)
            {
                available.Add(item);
            }
            //else
            //{
            //    Debug.Log("Armor of same type and equal or lower tier, skipping " + item.ItemName);
            //}
        }
        //for (int i = 0; i < allRudders.Count; i++)
        //{
        //    ItemCrew item = allCrews[i];
        //    if (item.Type != equipped.Type)
        //    {
        //        available.Add(item);
        //    }
        //    else if (item.ItemTier > equipped.ItemTier)
        //    {
        //        available.Add(item);
        //    }
        //    else
        //    {
        //        Debug.Log("Crew of same type and equal or lower tier, skipping " + item.ItemName);
        //    }
        //}

        return available;
    }
    // Add flag if necessary


    // Test for separate tiers
    private void AvailableByTierUpdate()
    {
        for (int i = 0; i < currentlyAvailableItems.Count; i++)
        {
            //Debug.Log("AvailableByTier iteration: " + i);

            if (currentlyAvailableItems[i].ItemTier == 3)
            {
                currentlyAvailableTier3.Add(currentlyAvailableItems[i]);
            }
            else if (currentlyAvailableItems[i].ItemTier == 2)
            {
                currentlyAvailableTier2.Add(currentlyAvailableItems[i]);
            }
            else 
            {
                currentlyAvailableTier1.Add(currentlyAvailableItems[i]);
                //Debug.Log("Added tier1");
            }
        } 
    }


    // Test for end of level
    public List<ItemBase> GenerateLevelRewardsListFromAvailable()
    {
        CurrentlyAvailableUpdate();

        List<ItemBase> newList = new List<ItemBase>();
        int badLuckInsurance = 0;

        for (int i = 0; i < 3; i++)
        {
            int luckRoll = Random.Range(1, 11);
            //Debug.Log("LuckRoll" + (i + 1) + ": " + luckRoll);

            if (luckRoll < 7)
            {
                badLuckInsurance++;
                if (badLuckInsurance < 3)
                {
                    newList.Add(RollRandomTier1());
                }
                else
                {
                    Debug.Log("Third low roll, bad luck insurance");
                    newList.Add(RollRandomTier2());
                }
            }
            else if (luckRoll < 10)
            {
                newList.Add(RollRandomTier2());
            }
            else
            {
                Debug.Log("Rolled tier3");
                newList.Add(RollRandomTier3());
            }
        }

        Debug.Log("Generated LevelRewardsListFromAvailable, count: " + newList.Count);
        return newList;
    }


    // Test for shop
    public List<ItemBase> GenerateShopContentsListFromAvailable()
    {
        CurrentlyAvailableUpdate();

        List<ItemBase> newList = new List<ItemBase>();

        newList.Add(RollRandomTier3());

        for (int i = 0; i < 8; i++)
        {
            int luckRoll = Random.Range(1, 21);
            //Debug.Log("LuckRoll" + (i + 1) + ": " + luckRoll);

            if (luckRoll < 14)
            {
                newList.Add(RollRandomTier1());
            }
            else if (luckRoll < 20)
            {
                newList.Add(RollRandomTier2());
            }
            else
            {
                Debug.Log("Rolled additional tier3!");
                newList.Add(RollRandomTier3());
            }
        }

        return newList;
    }


    // These remove items from available for now, so Available Lists need to be updated each time when rolling shop rewards or level rewards
    private ItemBase RollRandomTier1()
    {
        //int currentAvailableCount = 
        int roll = Random.Range(0, currentlyAvailableTier1.Count);
        //Debug.Log("Tier1 roll index: " + roll);
        ItemBase item = currentlyAvailableTier1[roll];
        currentlyAvailableTier1.RemoveAt(roll);
        return item;
    }
    private ItemBase RollRandomTier2()
    {
        int roll = Random.Range(0, currentlyAvailableTier2.Count);
        ItemBase item = currentlyAvailableTier2[roll];
        currentlyAvailableTier2.RemoveAt(roll);
        return item;
    }
    private ItemBase RollRandomTier3()
    {
        int roll = Random.Range(0, currentlyAvailableTier3.Count);
        ItemBase item = currentlyAvailableTier3[roll];
        currentlyAvailableTier3.RemoveAt(roll);
        return item;
    }








    // STARO

    //private void OldMethod()
    //{
    //    currentlyAvailableItems.Clear();

    //    for (int i = 0; i < allItems.Count; i++)
    //    {
    //        if (allItems[i] == null)
    //        {
    //            Debug.LogError("Item at index " + i + " is null!");
    //            continue;
    //        }


    //        ItemBase.ItemKind kind = allItems[i].WhatKindOfItem;
    //        Debug.Log("Item Kind: " + kind);



    //        if (kind == ItemBase.ItemKind.Cannon)
    //        {
    //            Debug.Log("Item is Cannon");
    //            if (allItems[i].GetComponent<ItemCannon>().Type != playerEquipment.EquippedCannons.Type)
    //            {
    //                currentlyAvailableItems.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    currentlyAvailableItems.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (kind == ItemBase.ItemKind.Armor)
    //        {
    //            Debug.Log("Item is Armor");
    //            if (allItems[i].GetComponent<ItemArmor>().Type != playerEquipment.EquippedArmor.Type)
    //            {
    //                currentlyAvailableItems.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    currentlyAvailableItems.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (kind == ItemBase.ItemKind.Sails)
    //        {
    //            Debug.Log("Item is Sails");
    //            if (allItems[i].GetComponent<ItemSails>().Type != playerEquipment.EquippedSails.Type)
    //            {
    //                currentlyAvailableItems.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    currentlyAvailableItems.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (kind == ItemBase.ItemKind.Rudder)
    //        {
    //            Debug.Log("Item is Rudder");
    //            if (allItems[i].GetComponent<ItemRudder>().Type != playerEquipment.EquippedRudder.Type)
    //            {
    //                currentlyAvailableItems.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    currentlyAvailableItems.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (kind == ItemBase.ItemKind.Crew)
    //        {
    //            Debug.Log("Item is Crew");
    //            if (allItems[i].GetComponent<ItemCrew>().Type != playerEquipment.EquippedCrew.Type)
    //            {
    //                currentlyAvailableItems.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    currentlyAvailableItems.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (kind == ItemBase.ItemKind.Flag)
    //        {
    //            Debug.Log("Item is Flag, nothing for now");
    //        }


    //        else
    //        {
    //            Debug.Log("Item itemType not recognized??");
    //        }
    //    }
    //}


    //private List<ItemBase> AvailableItemsForUpgradeOrChange()
    //{
    //    List<ItemBase> newList = new();

    //    for (int i = 0; i < allItems.Count; i++)
    //    {
    //        if (allItems[i].GetComponent<ItemCannon>() != null)
    //        {
    //            Debug.Log("Item is Cannon");
    //            if(allItems[i].GetComponent<ItemCannon>().Type != playerEquipment.EquippedCannons.Type)
    //            {
    //                newList.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    newList.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (allItems[i].GetComponent<ItemArmor>() != null)
    //        {
    //            Debug.Log("Item is Armor");
    //            if (allItems[i].GetComponent<ItemArmor>().Type != playerEquipment.EquippedArmor.Type)
    //            {
    //                newList.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    newList.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (allItems[i].GetComponent<ItemSails>() != null)
    //        {
    //            Debug.Log("Item is Sails");
    //            if (allItems[i].GetComponent<ItemSails>().Type != playerEquipment.EquippedSails.Type)
    //            {
    //                newList.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    newList.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (allItems[i].GetComponent<ItemRudder>() != null)
    //        {
    //            Debug.Log("Item is Rudder");
    //            if (allItems[i].GetComponent<ItemRudder>().Type != playerEquipment.EquippedRudder.Type)
    //            {
    //                newList.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    newList.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (allItems[i].GetComponent<ItemCrew>() != null)
    //        {
    //            Debug.Log("Item is Crew");
    //            if (allItems[i].GetComponent<ItemCrew>().Type != playerEquipment.EquippedCrew.Type)
    //            {
    //                newList.Add(allItems[i]);
    //                Debug.Log("Item is different Type, Adding");
    //            }
    //            else
    //            {
    //                if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //                {
    //                    newList.Add(allItems[i]);
    //                    Debug.Log("Item is same Type but higher Tier, Adding");
    //                }
    //                else
    //                {
    //                    Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //                }
    //            }
    //        }

    //        else if (allItems[i].GetComponent<ItemFlag>() != null)
    //        {
    //            Debug.Log("Item is Flag, nothing for now");
    //            //if (allItems[i].GetComponent<ItemCrew>().Type != playerEquipment.EquippedCrew.Type)
    //            //{
    //            //    newList.Add(allItems[i]);
    //            //    Debug.Log("Item is different Type, Adding");
    //            //}
    //            //else
    //            //{
    //            //    if (allItems[i].GetComponent<ItemBase>().ItemTier > playerEquipment.EquippedArmor.ItemTier)
    //            //    {
    //            //        newList.Add(allItems[i]);
    //            //        Debug.Log("Item is same Type but higher Tier, Adding");
    //            //    }
    //            //    else
    //            //    {
    //            //        Debug.Log("Item is same Type and equal or lower Tier, Skipping");
    //            //    }
    //            //}
    //        }


    //        else
    //        {
    //            Debug.Log("Item itemType not recognized??");
    //        }
    //    }

    //    return newList;
    //}
}
