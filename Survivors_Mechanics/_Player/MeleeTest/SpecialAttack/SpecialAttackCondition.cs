using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class SpecialAttackCondition : MonoBehaviour
{
    [SerializeField] protected bool isPaused = false;
    [Header("Dependencies")]
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected SpecialAttack specialAttack;
    [Header("Different types of conditions (Cooldown, Rage...")]
    [SerializeField] protected ConditionType type;

    [SerializeField] protected bool canUseSpecial = true;

    [SerializeField] protected float timeSinceLastUse = 0.0f;
    [SerializeField] protected float cooldownBase = 5.0f;
    [SerializeField] protected float cooldownTotal;

    [SerializeField] protected float currentRage = 0.0f;
    [SerializeField] protected float targetRage = 100.0f;
    [SerializeField] protected float rageGainMultiplier = 1.0f;

        // public ref
    public bool CanUseSpecial => canUseSpecial;



    public enum ConditionType
    {
        Cooldown,
        Rage
    }



    protected void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
        PlayerStatsChange += UpdateStatsFromPlayerStats;
    }
    protected void OnDisable()
    {
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
    }
    protected void PauseMe()
    {
        isPaused = true;
    }
    protected void UnPauseMe()
    {
        isPaused = false;
    }
    protected void UpdateStatsFromPlayerStats()
    {
        specialAttack = playerStats.EquippedSpecialAttack;
        cooldownTotal = cooldownBase * playerStats.SpecialAttackConditionReduceMultiplier;

        // Currently adjusted so it fits well with Cooldowns settings
        // if in Stats, ReduceMultiplier = 0.5f (current best stat), rage gain = 2x. If it is 3f (current worst stat), rage gain is 0.33f;
        // apply this when making Rage system
        rageGainMultiplier = (1f / playerStats.SpecialAttackConditionReduceMultiplier);
        Debug.Log("New SpecialAttack Cooldown: " + cooldownTotal);
    }
    public virtual void SetPlayerStatsReference(PlayerStats stats)
    {
        playerStats = stats;
    }





    // whole thing needs ui integration

        // Setup
    protected void Awake()
    {
        if (type == ConditionType.Cooldown)
        {
            timeSinceLastUse = 0.0f;
            canUseSpecial = true;
        }
        else if (type == ConditionType.Rage)
        {
            currentRage = 0.0f;
            canUseSpecial = false;
        } 
    }


        // Cooldown
    protected void Update()
    {
        if (!isPaused)
        {
            if (type == ConditionType.Cooldown)
            {
                if (!canUseSpecial)
                {
                    PlayerSpecialAttackTick?.Invoke();
                    timeSinceLastUse += Time.deltaTime;
                    CheckIfConditionMet();
                }
            }
        }
    }
        // Rage - needs to be called somewhere by Event
    protected void GainRage(float rageGained)
    {
        float newRage = currentRage + (rageGained * rageGainMultiplier);
        newRage = Mathf.Clamp(newRage, currentRage, targetRage);
        currentRage = newRage;
        CheckIfConditionMet();
    }


        // Shared
    public void CheckIfConditionMet()
    {
        if (ConditionMetRatio() == 1)
        {
            canUseSpecial = true;
            //RefreshAttackCanUse();
            PlayerSpecialAttackTick?.Invoke();
            Debug.Log("Condition met! Can use Special Attack");
        }
    }
    public float ConditionMetRatio()
    {
        if (type == ConditionType.Cooldown)
        {
            return Mathf.Clamp01(timeSinceLastUse / cooldownTotal);
        }
        else if (type == ConditionType.Rage)
        {
            return Mathf.Clamp01(currentRage / targetRage);
        }
        else
        {
            Debug.LogError("Condition type not set");
            return 0;
        }
    }

    


    //protected void RefreshAttackCanUse()
    //{
    //    specialAttack.RefreshSpecialAttack();
    //}



    public void SpecialAttackUsed()
    {
        canUseSpecial = false;
        timeSinceLastUse = 0;
        currentRage = 0;
    }
    public void CancelSpecialAttackUse()
    {
        canUseSpecial = true;
        timeSinceLastUse = cooldownTotal;
        currentRage = targetRage;
    }
}
