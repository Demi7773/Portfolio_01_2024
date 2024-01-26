using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Player/Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Starting Stats")]
    [SerializeField] private float startingMaxHP = 50.0f;
    [SerializeField] private float startingArmor = 10.0f;
    [SerializeField] private float startingDamage = 10.0f;
    [SerializeField] private float startingCritChancePercent = 1.0f;
    [SerializeField] private float startingCritDamageMod = 1.5f;
    [SerializeField] private float startingStamina = 100.0f;
    [SerializeField] private float startingStaminaRegen = 1.0f;
    [SerializeField] private float startingMoveSpeed = 5.0f;
    [Space(20)]
    [Header("Current Player Stats")]
    [SerializeField] private float maxHP;
    [SerializeField] private float armor;
    [SerializeField] private float damage;
    [SerializeField] private float critChance;
    [SerializeField] private float critDamageMod;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegen;
    [SerializeField] private float moveSpeed;

    // expand if adding gear
    //[Space(20)]
    //[Header("Current Stats with Gear")]
 

    public float MaxHP => maxHP;
    public float Armor => armor;
    public float Damage => damage;
    public float CritChance => critChance;
    public float CritDamageMod => critDamageMod;
    public float MaxStamina => maxStamina;
    public float StaminaRegen => staminaRegen;
    public float MoveSpeed => moveSpeed;



    // Call somewhere to initialize
    public void SetStartingStats()
    {
        maxHP = startingMaxHP;
        armor = startingArmor;
        damage = startingDamage;
        critChance = startingCritChancePercent;
        critDamageMod = startingCritDamageMod;
        maxStamina = startingStamina;
        staminaRegen = startingStaminaRegen;
        moveSpeed = startingMoveSpeed;
    }

        // add updates to other scripts if necessary
        // add clamps if necessary
    public void ModifyMaxHP(float amount)
    {
        maxHP += amount;
    }
    public void ModifyArmor(float amount)
    {
        armor += amount;
    }
    public void ModifyDamage(float amount)
    {
        damage += amount;
    }
    public void ModifyCritChance(float amount)
    {
        critChance += amount;
    }
    public void ModifyCritDamage(float amount)
    {
        critDamageMod += amount;
    }
    public void ModifyMaxStamina(float amount)
    {
        maxStamina += amount;
    }
    public void ModifyStaminaRegen(float amount)
    {
        staminaRegen += amount;
    }
    public void ModifyMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }
}
