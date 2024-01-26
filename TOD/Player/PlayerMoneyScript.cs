using UnityEngine;

public class PlayerMoneyScript : MonoBehaviour
{
    [SerializeField] private int playerMoney = 500;
    public int PlayerMoney => playerMoney;



    private void OnEnable()
    {
        HUDEvents.GoldUpdateEvent?.Invoke(new GoldUpdateEventData(playerMoney));
        PlayerEvents.LevelStart += GoldUIUpdate;
    }

    private void GoldUIUpdate()
    {
        HUDEvents.GoldUpdateEvent?.Invoke(new GoldUpdateEventData(playerMoney));
    }

    public void GetMoney(int amount)
    {
        ChangeMoneyAmount(amount);
    }
    public void LoseMoney(int amount)
    {
        ChangeMoneyAmount(-amount);
    }
    private void ChangeMoneyAmount(int difference)
    {
        playerMoney += difference;
        HUDEvents.GoldUpdateEvent?.Invoke(new GoldUpdateEventData(playerMoney));
        Debug.Log("New money: " + playerMoney);
    }
}
