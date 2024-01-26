using System;
using System.Collections.Generic;

public static class ShopEvents
{
    public static Action<SendShopItemsData> ShopItemsEvent;
    public static Action<SendRewardsItemData> LevelRewardItemsEvent;
    //public static Action<PlayerHPReference> PlayerEntersShopHPReference;
}

public class SendShopItemsData
{
    public List<ItemBase> Items = new List<ItemBase>(); //ovo mi saljes kroz event, tu listu skriptablesa

    public SendShopItemsData(List<ItemBase> items)
    {
        Items = items;
    }
}

public class SendRewardsItemData
{
    public List<ItemBase> Items = new List<ItemBase>(); //ovo mi saljes kroz event, tu listu skriptablesa

    public SendRewardsItemData(List<ItemBase> items)
    {
        Items = items;
    }
}

//public class PlayerHPReference
//{
//    public PlayerHPScript playerHPScript;

//    public PlayerHPReference(PlayerHPScript playerHP)
//    {
//        playerHPScript = playerHP;  
//    }
//}