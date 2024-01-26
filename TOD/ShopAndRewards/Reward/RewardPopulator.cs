using System.Collections.Generic;
using UnityEngine;

public class RewardPopulator : MonoBehaviour
{
    public List<RewardItemDisplayController> ItemDisplayControllers = new List<RewardItemDisplayController>();

    private void OnEnable()
    {
        ShopEvents.LevelRewardItemsEvent += OnRewardsListReceived;
    }

    private void OnDisable()
    {
        ShopEvents.LevelRewardItemsEvent += OnRewardsListReceived;
    }
    private void OnRewardsListReceived(SendRewardsItemData eventData) //napisati taj event negdje u events klasi nekoj 
    {
        Debug.Log("Rewards List Recieved by Pop");
        for (int i = 0; i < ItemDisplayControllers.Count; i++)
        {
            ItemDisplayControllers[i].SetReward(eventData.Items[i]);
            //Debug.Log("Set in RewardPopulator");
        }
    }



    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    //public void HideAllStatsScreens()
    //{
    //    foreach (var controller in ItemDisplayControllers)
    //    {
    //        controller.ToggleStatScreen(false);
    //    }
    //}
}
