using UnityEngine;
using static PlayerEvents;

public class TryRepairButton : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHPScript hpScript;
    [SerializeField] private PlayerMoneyScript moneyScript;
    [SerializeField] private int pricePerHP = 1;

    private void OnEnable()
    {
        PlayerEvents.PlayerGO += UpdatePlayerReference;
        //ShopEvents.PlayerEntersShopHPReference += OnPlayerWalkIntoShop;
    }
    private void UpdatePlayerReference(PlayerGOReference Player)
    {
        player = Player.playerGO;
        hpScript = player.GetComponent<PlayerHPScript>();
        moneyScript = player.GetComponent<PlayerMoneyScript>();
    }

    //private void OnPlayerWalkIntoShop(PlayerHPReference reference)
    //{
    //    hpScript = reference.playerHPScript;
    //    Debug.Log("Repair recieved HPReference, test if heals correclty or is creating new instance");
    //}

    public void TryRepairToFull()
    {
        if (player = null)
        {
            PlayerEvents.NeedPlayerReference?.Invoke();
        }
        RepairShipStatic.TryRepairToFull(hpScript, moneyScript, pricePerHP);
    }
}
