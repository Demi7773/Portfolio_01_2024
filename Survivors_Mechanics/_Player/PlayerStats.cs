using UnityEngine;
using static PlayerEvents;

public class PlayerStats : MonoBehaviour
{
    [Header("EXP system")]
    [SerializeField] protected PlayerXP playerXP;
    [SerializeField] protected PlayerPickUpEXP playerPickUpEXP;
    [SerializeField, Range(0.1f, 10.0f)] protected float pickUpEXPRangeMultiplier = 1.0f;
    [Header("Defense stats")]
    [SerializeField, Range(0.1f, 10.0f)] protected float maxHPMultiplier = 1.0f;
    [SerializeField, Range(0.0f, 99.0f)] protected float armor = 10.0f;
    [SerializeField, Range(0.01f, 10.0f)] protected float invulnerableDuration = 0.1f;
    [Space(20)]

    [Header("Movement Stats")]
    [SerializeField] protected float speedMultiplier = 1.0f;
    [Space(20)]

    [Header("Equipped Basic Attack and Stats")]
    [SerializeField] protected Attack equippedBasicAttack;
    [SerializeField, Range(0.1f, 10.0f)] protected float attackDamageMultiplier = 1.0f;
    [SerializeField, Range(0.0f, 1.0f)] protected float attackLockActionsDuration = 0.1f;
    [SerializeField, Range(0.1f, 3.0f)] protected float attackInstanceDuration = 0.3f;
    [SerializeField, Range(0.01f, 100.0f)] protected float attackCooldownDurationMultiplier = 1.0f;
    [SerializeField, Range(0.1f, 100.0f)] protected float attackSlashForwardSpeed = 1.0f;
    [Space(20)]

    [Header("Equipped Special Attack and Stats")]
    [SerializeField] protected SpecialAttackCondition equippedSpecialAttackCondition;
    [SerializeField] protected SpecialAttack equippedSpecialAttack;
    [SerializeField, Range(0.1f, 10.0f)] protected float specialAttackDamageMultiplier = 1.0f;
    [SerializeField, Range(0.1f, 5.0f)] protected float specialAttackConditionReduceMultiplier = 1.0f;
    [Space(10)]
    [Header("Set in Inspector")]
    [SerializeField, Range(0.1f, 0.5f)] protected float specialAttackConditionCapMin = 0.5f;
    [SerializeField, Range(2.0f, 5.0f)] protected float specialAttackConditionCapMax = 30f;



    // Old system

    //[Header("References to other scripts")]
    //[SerializeField] private GameObject player;
    //[SerializeField] private PlayerHP playerHPScript;
    //[SerializeField] private PlayerXP playerXPScript;
    //[Space(20)]


    // EXP
    public PlayerXP PlayerXP => playerXP;
    public PlayerPickUpEXP PlayerPickUpEXP => playerPickUpEXP;
    public float PickUpEXPRangeMultiplier => pickUpEXPRangeMultiplier;

        // Defense stats
    public float MaxHPMultiplier => maxHPMultiplier;
    public float Armor => armor;
    public float InvulnerableDuration => invulnerableDuration;

        // Movement
    public float SpeedMultiplier => speedMultiplier;

        // Attack
    public Attack EquippedBasicAttack => equippedBasicAttack;
    public float AttackDamageMultiplier => attackDamageMultiplier;
    public float AttackLockActionsDuration => attackLockActionsDuration;
    public float AttackInstanceDuration => attackInstanceDuration;
    public float AttackCooldownDurationMultiplier => attackCooldownDurationMultiplier;
    public float AttackSlashForwardSpeed => attackSlashForwardSpeed;


        // SpecialAttack
    public SpecialAttackCondition EquippedSpecialAttackCondition => equippedSpecialAttackCondition;
    public SpecialAttack EquippedSpecialAttack => equippedSpecialAttack;
    public float SpecialAttackDamageMultiplier => specialAttackDamageMultiplier;
    public float SpecialAttackConditionReduceMultiplier => specialAttackConditionReduceMultiplier;





    // !!!NEEDS TESTING!!!

    // NEW SYSTEM
    // In the new system, an event is sent out anytime stats change.
    // The listener methods on PlayerHP, PlayerController and Attack use the public get the updated values.

    protected void Start()
    {
        InitializeMe();
    }
    protected void InitializeMe()
    {
        Debug.Log("PlayerSats Initialized");
        PlayerStatsChange?.Invoke();
    }



        // Defensive stats

    public void ChangeMaxHPMultiplier(float amountChange)
    {
        float newHPMuliplier = maxHPMultiplier + amountChange;
        maxHPMultiplier = newHPMuliplier;
        Debug.Log("MaxHPMultiplier changed in PlayerStats, new MaxHPMultiplier: " + maxHPMultiplier);
        PlayerStatsChange?.Invoke();
    }
    public void ChangeArmor(float amountChange)
    {
        float newArmor = armor + amountChange;
        newArmor = Mathf.Clamp(newArmor, 0f, 99f);
        armor = newArmor;
        Debug.Log("Armor changed in PlayerStats, new Armor: " + armor);
        PlayerStatsChange?.Invoke();
    }



        // Movement
    public void ChangeSpeedMultipier(float amountChange)
    {
        float newSpeedMultiplier = speedMultiplier + amountChange;
        speedMultiplier = newSpeedMultiplier;
        Debug.Log("SpeedMultiplier changed in PlayerStats, new SpeedMultiplier: " + speedMultiplier);
        PlayerStatsChange?.Invoke();
    }



        // Attack

    public void ChangeAttack(Attack newAttack)
    {
        equippedBasicAttack.enabled = false;
        equippedBasicAttack = newAttack;
        equippedBasicAttack.enabled = true;
        equippedBasicAttack.SetPlayerStatsReference(this);
        Debug.Log("Test for enable and disable Attack script on ChangeAttack");
        PlayerStatsChange?.Invoke();
    }
    public void ChangeAttackDamageMultiplier(float amountChange)
    {
        float newDamage = attackDamageMultiplier + amountChange;
        attackDamageMultiplier = newDamage;
        Debug.Log("AttackDamageMultiplier changed in PlayerStats, new AttackDamageMultiplier: " + attackDamageMultiplier);
        PlayerStatsChange?.Invoke();
    }



        // Special Attack

    public void ChangeSpecialAttack(SpecialAttack newSpecialAttack)
    {
        equippedSpecialAttack.enabled = false;
        equippedSpecialAttack = newSpecialAttack;
        equippedSpecialAttack.enabled = true;
        equippedSpecialAttack.SetPlayerStatsReference(this);
        Debug.Log("Test for enable and disable Attack script on ChangeAttack");
        PlayerStatsChange?.Invoke();
    }
    public void ChangeSpecialAttackDamageMultiplier(float amountChange)
    {
        float newMultiplier = specialAttackDamageMultiplier + amountChange;
        specialAttackDamageMultiplier = newMultiplier;
        Debug.Log("SpecialAtk Multiplier changed in PlayerStats, new SpecialAtk Multiplier: " + specialAttackDamageMultiplier);
        PlayerStatsChange?.Invoke();
    }

    // Linearly reduces value, logic for scaling should be handled elsewhere
    // Clamps value by settings in inspector

    public void ChangeSpecialAttackConditionReduceMultiplier(float amountChange)
    {
        float newMultiplier = specialAttackConditionReduceMultiplier + amountChange;
        newMultiplier = Mathf.Clamp(newMultiplier, specialAttackConditionCapMin, specialAttackConditionCapMax);
        specialAttackConditionReduceMultiplier = newMultiplier;
        Debug.Log("SpecialCondition Multiplier changed in PlayerStats, new SpecialCondition Multiplier: " + specialAttackDamageMultiplier);
        PlayerStatsChange?.Invoke();
    }






    // OLD SYSTEM

    // References, Events integration and sending out info to other scripts
    //private void OnEnable()
    //{
    //    PlayerGO += PlayerRefrence;
    //    InitializeMe();
    //}
    //private void OnDisable()
    //{
    //    PlayerGO -= PlayerRefrence;
    //}
    //private void PlayerRefrence(PlayerGOReference Player)
    //{
    //    player = Player.playerGO;

    //    if (player.GetComponent<PlayerHP>() != null)
    //    {
    //        playerHPScript = player.GetComponent<PlayerHP>();
    //    }
    //    else
    //    {
    //        Debug.LogError("Players HPScript null!");
    //    }

    //    if (player.GetComponent<PlayerXP>() != null)
    //    {
    //        playerXPScript = player.GetComponent<PlayerXP>();
    //    }
    //    else
    //    {
    //        Debug.LogError("Players XPScript null!");
    //    }
    //}
    //private void InitializeMe()
    //{

    //}
}
