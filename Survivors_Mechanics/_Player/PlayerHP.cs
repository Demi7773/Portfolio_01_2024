using System.Collections;
using UnityEngine;
using static PlayerEvents;


public class PlayerHP : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float currentHP;
    [SerializeField, Range(10f, 1000f)] private float maxHPBase = 50.0f;
    [SerializeField, Range(10f, 1000f)] private float maxHPTotal/* = 100.0f*/;
    [SerializeField, Range(0.0f, 99.0f)] private float armor/* = 10.0f*/;

    [SerializeField, Range(0.0f, 10.0f)] private float invulnerableDuration/* = 0.1f*/;
    [SerializeField] private bool isInvulnerable = false;

    public float CurrentHP => currentHP;
    public float MaxHPTotal => maxHPTotal;
    public float Armor => armor;

    public float InvulnerableDuration => invulnerableDuration;



        // Initialization
    private void OnEnable()
    {
        isInvulnerable = false;
        LevelStart += InitializePlayerHPOnLevelStart;
        PlayerStatsChange += UpdateStatsFromPlayerStats;
    }
    private void OnDisable()
    {
        LevelStart -= InitializePlayerHPOnLevelStart;
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
        StopAllCoroutines();
    }
    private void InitializePlayerHPOnLevelStart()
    {
        HealHP(maxHPTotal);
        PlayerHPChange?.Invoke();
    }
    private void UpdateStatsFromPlayerStats()
    {
        maxHPTotal = maxHPBase * playerStats.MaxHPMultiplier; 
        armor = playerStats.Armor;
        invulnerableDuration = playerStats.InvulnerableDuration;
    }



        // Take Damage and Invulnerability system
    public void GetHitFor(float dmgAmount)
    {
        if (!isInvulnerable)
        {
            StartCoroutine(Invulnerability(invulnerableDuration));
            dmgAmount -= ArmorDamageReduction(dmgAmount, armor);
            LoseHP(dmgAmount);
        }
        else
        {
            Debug.Log("Player is invulnerable");
        }
    }
    private IEnumerator Invulnerability(float duration)
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(duration);
        isInvulnerable = false;
    }
    private float ArmorDamageReduction(float dmgAmount, float armorForCalculation)
    {
        // Diminishing returns on armor values closer to 100
        // Current system should be:
        // armor = 1 -> dmgReduction = 10%
        // armor = 10 -> dmgReduction = 31.62277660168379%
        // armor = 20 -> dmgReduction = 44.72135954999579%
        // armor = 30 -> dmgReduction = 54.77225575051661%
        // armor = 40 -> dmgReduction = 63.24555320336759%
        // armor = 50 -> dmgReduction = 70.71067811865475%
        // armor = 60 -> dmgReduction = 77.45966692414834%
        // armor = 70 -> dmgReduction = 83.66600265340755%
        // armor = 80 -> dmgReduction = 89.44271909999159%
        // armor = 90 -> dmgReduction = 94.86832980505138%
        // armor = 99 -> dmgReduction = 99.498743710662%
        // armor = 100 -> dmgReduction = 100%

        float dmgReductionPercent = Mathf.Sqrt(armorForCalculation * 100f);
        float reducedAmount = dmgReductionPercent * dmgAmount * 0.01f;
        return reducedAmount;
    }
    private void LoseHP(float amount)
    {
        //amount = ReduceDamageAmountByArmor(amount);

        float newHP = currentHP - amount;
        Debug.Log("Player was hit for " + amount + "dmg, new HP " + newHP);

        if (newHP <= 0f)
        {
            currentHP = 0f;
            Death();
        }
        else
        {
            currentHP = newHP;
        }

        PlayerEvents.PlayerHPChange?.Invoke();
    }



        // Death
    private void Death()
    {
        Debug.Log("Player died");
    }



        // Healing system
    public bool CheckIfHealable_HealIfTrue(float healAmount)
    {
        if (currentHP < maxHPTotal)
        {
            HealHP(healAmount);
            return true;
        }

        else return false;
    }
    public void HealHP(float amount)
    {
        float newHP = currentHP + amount;
        newHP = Mathf.Clamp(newHP, currentHP, maxHPTotal);

        currentHP = newHP;

        PlayerEvents.PlayerHPChange?.Invoke();
    }
}
