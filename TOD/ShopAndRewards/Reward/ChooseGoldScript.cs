using UnityEngine;

public class ChooseGoldScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] int gainAmountBase = 100;
    [SerializeField] int gainAmountPerLevel = 50;



    public void GetGoldAmount()
    {
        //PlayerMoneyStatic.GetMoney(ThisLevelGoldAmount());
        if (gameManager.Player.GetComponent<PlayerMoneyScript>() != null)
        {
            gameManager.Player.GetComponent<PlayerMoneyScript>().GetMoney(ThisLevelGoldAmount());
        }
        else
        {
            Debug.Log("Player MoneyScript Null");
        }
    }

    private int ThisLevelGoldAmount()
    {
        int newAmount = gainAmountBase + gameManager.CurrentLevel * gainAmountPerLevel;
        return newAmount;
    }
}
