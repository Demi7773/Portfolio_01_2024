using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHP playerHP;
    [SerializeField] private PlayerStamina playerStamina;
    [SerializeField] private PlayerXP playerXP;


    [SerializeField] private Image hpBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image xpBar;

    [SerializeField] private TextMeshProUGUI levelText;




    private void Start()
    {
        UpdateFullIngameHUD();
    }

    public void UpdateFullIngameHUD()
    {
        UpdateHPUI();
        UpdateStaminaUI();
        UpdateXPUI();
        UpdateLevelTextUI();
    }
    public void UpdateHPUI()
    {
        hpBar.fillAmount = playerHP.HPRatio();
    }
    public void UpdateStaminaUI()
    {
        staminaBar.fillAmount = playerStamina.StaminaRatio();
    }
    public void UpdateXPUI()
    {
        xpBar.fillAmount = playerXP.XPRatio();
    }
    public void UpdateLevelTextUI()
    {
        levelText.text = playerXP.PlayerLevel.ToString();
    }
}
