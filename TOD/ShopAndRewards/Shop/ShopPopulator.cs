using System.Collections.Generic;
using UnityEngine;

public class ShopPopulator : MonoBehaviour
{
    public List<ItemDisplayController> ItemDisplayControllers = new List<ItemDisplayController>();



    private void OnEnable()
    {
        ShopEvents.ShopItemsEvent += OnShopItemsListReceived;
    }
    private void OnDisable()
    {
        ShopEvents.ShopItemsEvent += OnShopItemsListReceived;
    }
    private void OnShopItemsListReceived(SendShopItemsData eventData) //napisati taj event negdje u events klasi nekoj 
    {
        for (int i = 0; i < ItemDisplayControllers.Count; i++)
        {
            ItemDisplayControllers[i].SetShopItem(eventData.Items[i]);
            Debug.Log("Set in ShopPop");
        }
    }



    public void HideAllStatsScreens()
    {
        foreach (var controller in ItemDisplayControllers)
        {
            controller.ToggleStatScreen(false);
        }
    }
}
