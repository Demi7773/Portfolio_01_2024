using UnityEngine;

public static class RepairShipStatic
{
    private static int PriceForRepairToFull(PlayerHPScript hpScript, float pricePerHP)
    {
        float hpDiff = hpScript.GetPlayerMaxHP - hpScript.GetPlayerCurrentHP;
        int repairCost = (int)(hpDiff * pricePerHP);
        return repairCost;
    }

    public static void TryRepairToFull(PlayerHPScript hpScript, PlayerMoneyScript moneyScript, float pricePerHP)
    {
        if (moneyScript != null && hpScript != null)
        {
            if (hpScript.GetPlayerCurrentHP < hpScript.GetPlayerMaxHP)
            {
                int currentMoney = moneyScript/*PlayerMoneyStatic*/.PlayerMoney;
                int repairCost = PriceForRepairToFull(hpScript, pricePerHP);

                if (currentMoney >= repairCost)
                {
                    moneyScript.LoseMoney(repairCost);
                    //PlayerMoneyStatic.LoseMoney(repairCost);
                    hpScript.SetToFullHP();
                }
                else
                {
                    float healAmount = currentMoney / pricePerHP;
                    moneyScript.LoseMoney(currentMoney);
                    //PlayerMoneyStatic.LoseMoney(currentMoney);
                    hpScript.HealHP(healAmount);
                    Debug.Log("Not enough money (Current money: " + currentMoney + ") Healing for: " + healAmount + " instead");
                }
            }
            else
            {
                Debug.Log("Full HP!");
            }
        }
        else
        {
            Debug.Log("MoneyScript or HPScript null");
        }
    }
}
