using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEvents;

public class UpgradeHolder : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] private GameObject chooseUpgradePanel;
    //[SerializeField] private PlayerStats playerStats;

    [SerializeField] private Image upgradeImageDisplay;
    [SerializeField] private TextMeshProUGUI upgradeNameText;
    [SerializeField] private TextMeshProUGUI upgradeDescriptionText;

    [Space(20)]
    [Header("Debug")]
    [SerializeField] private Upgrade heldUpgrade;



    public void SetUpgradeIntoSlotAndUpdateUI(Upgrade newUpgrade)
    {
        heldUpgrade = newUpgrade;

        upgradeImageDisplay.sprite = heldUpgrade._Sprite;
        upgradeNameText.text = heldUpgrade._Name;
        upgradeDescriptionText.text = heldUpgrade._Description;
    }

        // Check if racecondition error here or just cause List empty in Populator
    public void ChooseThisUpgrade()
    {
        EquipUpgradeToPlayer();
        chooseUpgradePanel.SetActive(false);
        PlayerChooseUpgrade?.Invoke();
        UnPauseGame?.Invoke();
    }



        // Add system
    private void EquipUpgradeToPlayer()
    {
        heldUpgrade.ApplyThisUpgradeToPlayer();
        Debug.Log("Add equip to player system here");
    }
}
