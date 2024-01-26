using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private UIManager uiManager;
    [Space(20)]
    [Header("Timer")]
    [SerializeField] private float timeBetweenTicks = 0.1f;
    [SerializeField] private float timer = 0.0f;
    [Space(10)]
    [Header("Stats")]
    [SerializeField] private float currentStamina = 0.0f;
    [SerializeField] private float maxStamina => stats.MaxStamina;
    [SerializeField] private float staminaRegenPerTick => stats.StaminaRegen;



    public float StaminaRatio()
    {
        return currentStamina / maxStamina;
    }
    public bool HasEnoughStaminaForAction(float actionCost)
    {
        if (currentStamina >= actionCost)
            return true;

        return false;
    }
    public void UseStaminaForAction(float actionCost)
    {
        ChangeStaminaValue(-actionCost);
    }



    private void OnEnable()
    {
        timer = 0.0f;
    }
    private void Start()
    {
        ChangeStaminaValue(maxStamina);
    }



    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenTicks)
        {
            RegenStaminaTick();
            timer -= timeBetweenTicks;
        }
    }
    private void RegenStaminaTick()
    {
        ChangeStaminaValue(staminaRegenPerTick);
    }

        // add UI
    private void ChangeStaminaValue(float amount)
    {
        float newStamina = currentStamina + amount;
        newStamina = Mathf.Clamp(newStamina, 0.0f, maxStamina);

        currentStamina = newStamina;

        uiManager.UpdateStaminaUI();
    }

}
