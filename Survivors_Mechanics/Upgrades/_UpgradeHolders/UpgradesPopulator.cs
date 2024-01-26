using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesPopulator : MonoBehaviour
{
    [SerializeField] private UpgradeHolder upgradeHolder1;
    [SerializeField] private UpgradeHolder upgradeHolder2;
    [SerializeField] private UpgradeHolder upgradeHolder3;



    public void NewUpgrades(List<Upgrade> newUpgrades)
    {
        upgradeHolder1.SetUpgradeIntoSlotAndUpdateUI(newUpgrades[0]);
        upgradeHolder2.SetUpgradeIntoSlotAndUpdateUI(newUpgrades[1]);
        upgradeHolder3.SetUpgradeIntoSlotAndUpdateUI(newUpgrades[2]);
    }
}
