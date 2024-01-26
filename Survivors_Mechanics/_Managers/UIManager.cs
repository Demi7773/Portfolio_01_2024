using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEvents;

public class UIManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    [Header("HUD")]
    [SerializeField] private GameObject inGameHUD;

    [SerializeField] private Image hpBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private Image specialAttackFill;

    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private TextMeshProUGUI timerText;
    [Space(20)]

    [Header("Level Up")]
    [SerializeField] private GameObject levelUPPanel;
    [Space(20)]

    [Header("Set from PlayerEvents")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHP playerHPScript;
    [SerializeField] private PlayerXP playerXPScript;
    [SerializeField] private SpecialAttackCondition playerSpecialAttackCondition;

    // need system to reference this at level start
    [SerializeField] private LevelManager levelManager;
    




        // References, Events integration and other necessary processes
    private void OnEnable()
    {
        PlayerGO += PlayerRefrence;
        SetHUD += HUDActive;

        PlayerHPChange += PlayerHPUI;
        PlayerXPChange += PlayerXPUI;
        PlayerSpecialAttackTick += SpecialAttackUI;
        TimerTick += TimerUI;

        PlayerLevelUp += PlayerLevelUpUI;
        PlayerChooseUpgrade += RefreshWholeIngameHUD;

        //CurrentWeaponReference += WeaponReference;
    }
    private void OnDisable()
    {
        PlayerGO -= PlayerRefrence;
        SetHUD -= HUDActive;

        PlayerHPChange -= PlayerHPUI;
        PlayerXPChange -= PlayerXPUI;
        PlayerSpecialAttackTick -= SpecialAttackUI;
        TimerTick -= TimerUI;

        PlayerLevelUp -= PlayerLevelUpUI;
        PlayerChooseUpgrade -= RefreshWholeIngameHUD;

        //CurrentWeaponReference -= WeaponReference;
    }

    private void PlayerRefrence(PlayerGOReference Player)
    {
        player = Player.playerGO;

        if (player.GetComponent<PlayerHP>() != null)
        {
            playerHPScript = player.GetComponent<PlayerHP>();
        }
        else
        {
            Debug.LogError("Players HPScript null!");
        }

        if (player.GetComponent<PlayerXP>() != null)
        {
            playerXPScript = player.GetComponent<PlayerXP>();
        }
        else
        {
            Debug.LogError("Players XPScript null!");
        }

        if (player.GetComponent<SpecialAttackCondition>() != null)
        {
            playerSpecialAttackCondition = player.GetComponent<SpecialAttackCondition>();
        }
        else
        {
            Debug.LogError("Players SpecialAttackCondition null!");
        }
    }


        // Activate / Deactivate UI Panels
    private void HUDActive(OnOrOff value)
    {
        inGameHUD.SetActive(value.IsTrueOrFalse);
    }
    private void PlayerLevelUpUI()
    {
        levelUPPanel.SetActive(true);
    }



        // Refresh Player UI
    private void RefreshWholeIngameHUD()
    {
        PlayerHPUI();
        PlayerXPUI();
        SpecialAttackUI();
    }
    private void PlayerHPUI()
    {
        if (playerHPScript == null)
        {
            NeedPlayerReference?.Invoke();
        }

        hpBar.fillAmount = playerHPScript.CurrentHP / playerHPScript.MaxHPTotal;
        hpText.text = playerHPScript.CurrentHP + "/" + playerHPScript.MaxHPTotal;
    }
    private void PlayerXPUI()
    {
        if (playerXPScript == null)
        {
            NeedPlayerReference?.Invoke();
        }

        xpBar.fillAmount = playerXPScript.CurrentLevelXP / playerXPScript.CurrentLevelThresholdToLevelUp;
        xpText.text = playerXPScript.CurrentLevelXP + "/" + playerXPScript.CurrentLevelThresholdToLevelUp;
    }
        // Only cooldown system for now
    private void SpecialAttackUI()
    {
        specialAttackFill.fillAmount = playerSpecialAttackCondition.ConditionMetRatio();
    }
    private void TimerUI()
    {
        int timer = (int) levelManager.CurrentLevelTimer;

        int minutes = (int) (timer / 60f);
        int seconds = timer % 60;
        timerText.text = minutes + ":" + seconds;
    }



        // Unused
    //private void WeaponReference(PlayerWeaponReference weapon)
    //{
    //    useWeapon = weapon.EquippedUseWeapon;
    //    AmmoChange?.Invoke();
    //}

}
