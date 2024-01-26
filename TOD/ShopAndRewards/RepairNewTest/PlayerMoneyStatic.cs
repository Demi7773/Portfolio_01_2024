using UnityEngine;

public static class PlayerMoneyStatic
{
    [SerializeField] private static int playerMoney = 500;
    public static int PlayerMoney => playerMoney;


    public static void GetMoney(int amount)
    {
        ChangeMoneyAmount(amount);
    }

    public static void LoseMoney(int amount)
    {
        ChangeMoneyAmount(-amount);
    }

    private static void ChangeMoneyAmount(int difference)
    {
        playerMoney += difference;
        HUDEvents.GoldUpdateEvent?.Invoke(new GoldUpdateEventData(playerMoney));
        Debug.Log("New money: " +  playerMoney);
    }
}
