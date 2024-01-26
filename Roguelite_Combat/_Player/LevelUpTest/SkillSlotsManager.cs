using System.Collections.Generic;
using UnityEngine;

public class SkillSlotsManager : MonoBehaviour
{
    [SerializeField] private PlayerXP playerXP;

    [SerializeField] private List<SkillSlotLevelController> tier1Slots = new List<SkillSlotLevelController>();
    [SerializeField] private List<SkillSlotLevelController> tier2Slots = new List<SkillSlotLevelController>();
    [SerializeField] private List<SkillSlotLevelController> tier3Slots = new List<SkillSlotLevelController>();
    [SerializeField] private List<SkillSlotLevelController> tier4Slots = new List<SkillSlotLevelController>();
    [SerializeField] private List<SkillSlotLevelController> tier5Slots = new List<SkillSlotLevelController>();

    [SerializeField] private int requirementForTier1 = 0;
    [SerializeField] private int requirementForTier2 = 5;
    [SerializeField] private int requirementForTier3 = 10;
    [SerializeField] private int requirementForTier4 = 15;
    [SerializeField] private int requirementForTier5 = 20;


    private void Start()
    {
        CheckAndSetTierLocks();
    }

    public void CheckAndSetTierLocks()
    {
        Debug.Log("Checking and Setting Skill Tiers");

        bool tier1Avaiblable = Tier1Available();
        bool tier2Avaiblable = Tier2Available();
        bool tier3Avaiblable = Tier3Available();
        bool tier4Avaiblable = Tier4Available();
        bool tier5Avaiblable = Tier5Available();

        foreach (SkillSlotLevelController skillSlot in tier1Slots)
        {
            skillSlot.InitializeMe(this, playerXP);
            skillSlot.SetLockedStatus(tier1Avaiblable);
        }
        foreach (SkillSlotLevelController skillSlot in tier2Slots)
        {
            skillSlot.InitializeMe(this, playerXP);
            skillSlot.SetLockedStatus(tier2Avaiblable);
        }
        foreach (SkillSlotLevelController skillSlot in tier3Slots)
        {
            skillSlot.InitializeMe(this, playerXP);
            skillSlot.SetLockedStatus(tier3Avaiblable);
        }
        foreach (SkillSlotLevelController skillSlot in tier4Slots)
        {
            skillSlot.InitializeMe(this, playerXP);
            skillSlot.SetLockedStatus(tier4Avaiblable);
        }
        foreach (SkillSlotLevelController skillSlot in tier5Slots)
        {
            skillSlot.InitializeMe(this, playerXP);
            skillSlot.SetLockedStatus(tier5Avaiblable);
        }
    }


    private bool Tier1Available()
    {
        if (playerXP.PlayerLevel >= requirementForTier1)
            return true;
        return false;
    }
    private bool Tier2Available()
    {
        if (playerXP.PlayerLevel >= requirementForTier2)
            return true;
        return false;
    }
    private bool Tier3Available()
    {
        if (playerXP.PlayerLevel >= requirementForTier3)
            return true;
        return false;
    }
    private bool Tier4Available()
    {
        if (playerXP.PlayerLevel >= requirementForTier4)
            return true;
        return false;
    }
    private bool Tier5Available()
    {
        if (playerXP.PlayerLevel >= requirementForTier5)
            return true;
        return false;
    }
}

