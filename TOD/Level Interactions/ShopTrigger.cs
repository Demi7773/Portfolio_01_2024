using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    //[SerializeField] ItemDisplayController itemDisplayController;
    [SerializeField] private AvailableItems availableItems;



    private void Start()
    {
        PlayerEvents.ShopLevelStart?.Invoke();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered Shop");
            AudioEvents.PlayShopEnterSoundEvent?.Invoke();
            RaisePanelsFromLevelsEvents.RaiseShopPanelEvent?.Invoke();



            ShopEvents.ShopItemsEvent?.Invoke(new SendShopItemsData(availableItems.shopContents));



            //if (other.GetComponent<PlayerHPScript>() != null)
            //{
            //    ShopEvents.PlayerEntersShopHPReference?.Invoke(new PlayerHPReference(other.GetComponent<PlayerHPScript>()));
            //    Debug.Log("PlayerReference Sent test");
            //}
        }
    }
}
