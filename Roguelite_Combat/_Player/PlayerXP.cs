using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SkillsUIActivator levelUpUI;
    [Space(20)]
    [SerializeField] private int xpTotal = 0;
    [SerializeField] private int xpThisLevel = 0;
    [SerializeField] private int playerLevel = 1;
    [SerializeField] private int lastLevelThreshold = 0;
    [SerializeField] private int thresholdForNextLevel = 100;
    [Space(20)]
    [SerializeField] private int playerSkillPoints = 0;



    public int XpTotal => xpTotal;
    public int XpThisLevel => xpThisLevel;
    public int PlayerLevel => playerLevel;
    public int ThresholdForNextLevel => thresholdForNextLevel;
    public int PlayerLevelUpPoints => playerSkillPoints;



    public float XPRatio()
    {
        float ratio = xpThisLevel / (thresholdForNextLevel - lastLevelThreshold);
        return ratio;
    }

        // add UI
    public void GetXP(int xpGain)
    {
        int newXP = xpTotal + xpGain;
        xpTotal = newXP;

        if (xpTotal >= thresholdForNextLevel)
        {
            LevelUpReached();
        }

        xpThisLevel = xpTotal - lastLevelThreshold;

        uiManager.UpdateXPUI();
    }

        // add UI
    private void LevelUpReached()
    {
        //xpThisLevel = xpTotal - thresholdForNextLevel;

        lastLevelThreshold = thresholdForNextLevel;
        thresholdForNextLevel = NextLevelUpThreshold();

        playerLevel++;
        playerSkillPoints++;

        uiManager.UpdateXPUI();
        uiManager.UpdateLevelTextUI();

        levelUpUI.ToggleSkillsPanel(true);
    }

        // temp
    private int NextLevelUpThreshold()
    {
        
        return thresholdForNextLevel + 100;
    }


        // add UI
    public void SpendSkillPoints(int amountSpent)
    {
        playerSkillPoints -= amountSpent;
    }
}
