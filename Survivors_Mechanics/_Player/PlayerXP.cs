using UnityEngine;
using static PlayerEvents;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private int playerTotalXP = 0;
    [SerializeField] private int currentLevelXP = 0;
    [SerializeField] private int currentLevelThresholdToLevelUp = 10;
    [SerializeField] private int currentLevel = 1;

    [SerializeField, Range(1.0f, 3.0f)] private float thresholdMultiplier = 2.0f;

    public int PlayerTotalXP => playerTotalXP;
    public int CurrentLevelXP => currentLevelXP;
    public int CurrentLevelThresholdToLevelUp => currentLevelThresholdToLevelUp;
    public int CurrentLevel => currentLevel;



        // temp
    private void Awake()
    {
        OnLevelStart();
    }
    private void OnLevelStart()
    {
        PlayerXPChange?.Invoke();
    }



    public void GetXP(int xpGain)
    {
        playerTotalXP += xpGain;

        UpdateCurrentLevelXP(xpGain);
    }
    private void UpdateCurrentLevelXP(int xpGain)
    {
        int newXP = currentLevelXP + xpGain;

        if (!ReachedLevelUpThreshold(newXP, CurrentLevelThresholdToLevelUp))
        {
            currentLevelXP = newXP;
            PlayerXPChange?.Invoke();
        }
        else
        { 
            LevelUp(newXP, currentLevelThresholdToLevelUp, currentLevel);
        }
    }
    private bool ReachedLevelUpThreshold(int xp, int threshold)
    {
        if (xp < threshold)
            return false;
        else
            return true;
    }
    private void LevelUp(int newXP, int threshold, int currentLvl)
    {
        newXP = newXP - threshold;
        SetNewLevelUpThreshold(threshold, currentLvl);
        currentLevel++;

        PlayerXPChange?.Invoke();
        PlayerLevelUp?.Invoke();
        PauseGame?.Invoke();

        Debug.Log("Level Up! New level: " + currentLevel + " currentXP: " + currentLevelXP + ", new threshold: " + currentLevelThresholdToLevelUp);
    }



    // Expand later
    private void SetNewLevelUpThreshold(int threshold, int currentLvl)
    {
        threshold = (int)(threshold * thresholdMultiplier);
        currentLevelThresholdToLevelUp = threshold;
    }
}
