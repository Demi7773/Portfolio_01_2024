using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgrades : MonoBehaviour
{

    [Header("Dependencies")]
    /*[SerializeField]*/ protected GameObject player;
    /*[SerializeField]*/ protected UpgradesPopulator upgradesPopulator;
    [SerializeField] protected List<Upgrade> allUpgradesOfType = new List<Upgrade>();

    [Space(20)]
    [Header("Rolled List")]
    [SerializeField] protected List<Upgrade> rolledUpgradeList = new List<Upgrade>();




    protected void OnEnable()
    {

    }
    protected void OnDisable()
    {
        StopAllCoroutines();
    }



    public void InitializeMe(GameObject playerRef, UpgradesPopulator populator)
    {
        player = playerRef;
        upgradesPopulator = populator;
    }
        // Test when Upgrades integrated
    public void RollUpgrades()
    {
        rolledUpgradeList.Clear();


        int roll1 = Random.Range(0, allUpgradesOfType.Count);

        Upgrade rolledUpgrade1 = allUpgradesOfType[roll1];
        //Debug.Log("Upgrade roll1: " + roll1);
        rolledUpgradeList.Add(rolledUpgrade1);



        int roll2 = Random.Range(0, allUpgradesOfType.Count);
        if (roll2 == roll1)
        {
            roll2++;
            if (roll2 == allUpgradesOfType.Count)
            {
                roll2 = 0;
            }
            //Debug.Log("Same roll for roll2, roll2++ -> roll2: " + roll2);
        }

        Upgrade rolledUpgrade2 = allUpgradesOfType[roll2];
        //Debug.Log("Upgrade roll2: " + roll2);
        rolledUpgradeList.Add(rolledUpgrade2);



        int roll3 = Random.Range(0, allUpgradesOfType.Count);
        if (roll3 == roll1 || roll3 == roll2)
        {
            roll3++;
            if (roll3 == allUpgradesOfType.Count)
            {
                roll3 = 0;
            }
            //Debug.Log("Same roll for roll3, roll3++ -> roll3: " + roll3);

            if (roll3 == roll1 || roll3 == roll2)
            {
                roll3++;
                if (roll3 == allUpgradesOfType.Count)
                {
                    roll3 = 0;
                }
                //Debug.Log("Same roll for roll3 AGAIN, roll3++ -> roll3: " + roll3);
            }
        }

        Upgrade rolledUpgrade3 = allUpgradesOfType[roll3];
        //Debug.Log("Upgrade roll3: " + roll3);
        rolledUpgradeList.Add(rolledUpgrade3);

        StartCoroutine(DelayBeforeRepopulatingLevelUpUI());








            // this is causing an Infinity Loop, for now switched with static system for always 3 upgrades
        //for (int i = 0; i < 3; i++)
        //{
        //int roll = Random.Range(0, allUpgradesOfType.Count);
        //Upgrade rolledUpgrade = allUpgradesOfType[roll];
        //Debug.Log("Upgrade roll " + roll);



        //if (rolledUpgradeList.Count > 0)
        //{

        //for (int j = 0; j < rolledUpgradeList.Count; j++)
        //{
        //    if (rolledUpgrade != rolledUpgradeList[j])
        //    {
        //        j++;
        //        rolledUpgradeList.Add(rolledUpgrade);
        //        Debug.Log("Added Upgrade: " + rolledUpgrade.name + " to rolledUpgrades, roll was: " + roll);
        //    }
        //    else
        //    {
        //        i--;
        //        Debug.Log("Upgrade duplicate:" + rolledUpgrade.name + " NOT ading to rolledUpgrades, roll was: " + roll + ". ***DECREASING i TEST***");
        //    }
        //}
        //}
        //else
        //{
        //    rolledUpgradeList.Add(rolledUpgrade);
        //    Debug.Log("First Roll, Added Upgrade: " + rolledUpgrade.name + " to rolledUpgrades, roll was: " + roll);
        //}
        //    rolledUpgradeList.Add(rolledUpgrade);
        //}
    }




    protected IEnumerator DelayBeforeRepopulatingLevelUpUI()
    {
        yield return new WaitForSeconds(1.0f);
        PopulateUI();
    }
    protected void PopulateUI()
    {
        upgradesPopulator.NewUpgrades(rolledUpgradeList);
    }
}
