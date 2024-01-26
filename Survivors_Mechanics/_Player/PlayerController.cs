using UnityEngine;
using static PlayerEvents;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Attack attackScript;
    [SerializeField] private SpecialAttackCondition specialAttackCondition;
    [SerializeField] private SpecialAttack specialAttack;
    //[SerializeField] protected SpecialAttackCondition equippedSpecialAttackCondition;

    [Space(30)]
    [Header("Movement Settings")]
    [SerializeField, Range(1.0f, 30.0f)] private float speedBase = 1.0f;
    [SerializeField, Range(1.0f, 30.0f)] private float speedTotal;
    //[SerializeField] float acceleration = 10f;
    //[SerializeField] float deceleration = 15f;
    //[SerializeField] float maxSpeed = 10f;

    [Space(30)]
    [Header("Attack Basic Settings")]
    [SerializeField] private bool autoAttack = false;

    [Space(30)]
    [Header("Debug")]
    [SerializeField] private Vector3 inputTotal;
    [SerializeField] private bool isPaused = false;



    private void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
        PlayerStatsChange += UpdateStatsFromPlayerStats;
    }
    private void OnDisable()
    {
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
    }
    private void PauseMe()
    {
        isPaused = true;
    }
    private void UnPauseMe()
    {
        isPaused = false;
    }

    public void SetPlayerStatsReference(PlayerStats stats)
    {
        playerStats = stats;
    }
    private void UpdateStatsFromPlayerStats()
    {
        attackScript = playerStats.EquippedBasicAttack;
        speedTotal = speedBase * playerStats.SpeedMultiplier;
        specialAttackCondition = playerStats.EquippedSpecialAttackCondition;
        specialAttack = playerStats.EquippedSpecialAttack;
    }



        // figure out better Input System
    private void Update()
    {
        if (!isPaused)
        {
            if (attackScript.IsAttacking)
            {
                return;
            }
            else
            {
                if (attackScript.IsAttackOnCooldown)
                {
                    Movement();
                }
                else
                {
                    if (autoAttack || Input.GetButton("Fire1"))
                    {
                        Attack();
                    }
                    else
                    {
                        Movement();
                    }
                }
            }

            if (Input.GetButton("Fire2"))
            {
                if(specialAttackCondition.CanUseSpecial)
                {
                    UseSpecialAttack();
                }
                else
                {
                    Debug.Log("Special attack !CanUse in SpecialAttack script. See Condition");
                }
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Player pressed P, game Paused");
                PauseGame?.Invoke();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Player pressed P, game UnPaused");
                UnPauseGame?.Invoke();
            }
        }
    }

    private void Attack()
    {
        attackScript.CreateAttack();
    }
    private void UseSpecialAttack()
    {
        specialAttack.UseSpecialAttack();
        Debug.Log("Used Special Attack!");
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputTotal = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        transform.position += inputTotal * speedTotal * Time.deltaTime;
    }
}
