using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlotLevelController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private SkillSlotsManager skillSlotsManager;
    [Space(20)]
    [Header("Skill")]
    [SerializeField] private Skill heldSkill;
    [Header("UI Elements")]
    [SerializeField] private Button skillButton;
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillLevelNumber;
    [SerializeField] private TextMeshProUGUI skillCostNumber;
    [SerializeField] private GameObject skillMaxedSymbol;



    private void OnEnable()
    {
        UpdateSkillUI();
    }

    public void InitializeMe(SkillSlotsManager manager, PlayerXP playerXPScript)
    {
        skillSlotsManager = manager;
        playerXP = playerXPScript;
        skillImage.sprite = heldSkill.SkillSprite;
        UpdateSkillUI();
    }

    public void SetLockedStatus(bool isAvailable)
    {
        skillButton.interactable = isAvailable;

        //if (isAvailable)
        //{
        //    // normal color, branch for can upgrade / maxed color?
        //}
        //else
        //{
        //    // gray out
        //}
    }


    public void UpdateSkillUI()
    {
        skillLevelNumber.text = heldSkill.SkillLevel + "/" + heldSkill.SkillMaxLevel;

        if (heldSkill.IsMaxed())
        {
            skillCostNumber.gameObject.SetActive(false);
            skillMaxedSymbol.SetActive(true);
        }
        else
        {
            skillCostNumber.text = "Cost: " + heldSkill.CurrentLevelUpCost();
        }
    }

    public void TryToLevelUpSkill()
    {
        if (!heldSkill.IsMaxed())
        {
            if (playerXP.PlayerLevelUpPoints >= heldSkill.CurrentLevelUpCost())
            {
                LevelUpSkill();
            }
        }
        else
        {
            Debug.Log("Skill already maxed!");
        }
    }

        // test execution order
    private void LevelUpSkill()
    {
        playerXP.SpendSkillPoints(heldSkill.CurrentLevelUpCost());
        heldSkill.LevelUpSkill();

        skillSlotsManager.CheckAndSetTierLocks();
        UpdateSkillUI();
    }
}
