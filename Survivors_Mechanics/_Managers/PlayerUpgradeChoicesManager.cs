using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class PlayerUpgradeChoicesManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject player;
    [SerializeField] private UpgradesPopulator upgradesPopulator;
    //[SerializeField] private PlayerXP playerXPScript;
    //[SerializeField] private PlayerHP playerHPScript;
    [Space(20)]
    [Header("Upgrade Types")]
    [SerializeField] private BasicUpgrades basicUpgrades;

    //[SerializeField] private List<UpgradeType> upgradeTypeList = new List<UpgradeType>();
    //[SerializeField] private List<Upgrade> type0UpgradeList = new List<Upgrade>();

    //[Space(20)]
    //[Header("Rolled List")]
    //[SerializeField] private List<Upgrade> rolledUpgradeList = new List<Upgrade>();




    private void OnEnable()
    {
        LevelStart += OnLevelStart;
        PlayerChooseUpgrade += OnUpgradeChoice;
        InitializeMeAndDependants();
    }
    private void OnDisable()
    {
        LevelStart -= OnLevelStart;
        PlayerChooseUpgrade -= OnUpgradeChoice;
        StopAllCoroutines();
    }
    private void InitializeMeAndDependants()
    {
        if (player != null)
        {
            basicUpgrades.InitializeMe(player, upgradesPopulator);
        }
        else
        {
            Debug.Log("Player null!!!");
        }
    }




    private void OnLevelStart()
    {
        RollUpgradeType();
    }
    public void OnUpgradeChoice()
    {
        RollUpgradeType();
    }

        // Expand this with other test types
    private void RollUpgradeType()
    {
        //int roll = Random.Range(0, upgradeTypeList.Count);
        int roll = 0;

        switch (roll)
        {
            case 0:
                basicUpgrades.RollUpgrades();
                Debug.Log("Rolled BasicUpgrades");
                break;
        }
    }
}
