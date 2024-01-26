using UnityEngine;
using static PlayerEvents;

public class SpecialAttack : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected SpecialAttackCondition condition;

    //protected bool canUseSpecialAttack = true;
    //public bool CanUseSpecialAttack => canUseSpecialAttack;


    protected virtual void OnEnable()
    {
        InitializeMe();
        PlayerStatsChange += UpdateStatsFromPlayerStats;
        //PauseGame += PauseMe;
        //UnPauseGame += UnPauseMe;
    }
    protected virtual void OnDisable()
    {
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
        //PauseGame -= PauseMe;
        //UnPauseGame -= UnPauseMe;
    }
    //protected virtual void PauseMe()
    //{
    //    isPaused = true;
    //}
    //protected virtual void UnPauseMe()
    //{
    //    isPaused = false;
    //}

    public virtual void SetPlayerStatsReference(PlayerStats stats)
    {
        playerStats = stats;
    }
    protected virtual void UpdateStatsFromPlayerStats()
    {
        
    }


    protected virtual void InitializeMe()
    {

    }



    //public virtual void RefreshSpecialAttack()
    //{
    //    canUseSpecialAttack = true;
    //}

    public virtual void UseSpecialAttack()
    {
        condition.SpecialAttackUsed();
        //canUseSpecialAttack = false;
    }
}
