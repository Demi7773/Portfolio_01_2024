using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class Skill : ScriptableObject
{
    [Header("Skill Values")]
    [SerializeField] protected int skillLevel = 0;
    [SerializeField] protected int skillMaxLevel = 1;
    [SerializeField] protected int[] levelUpCosts;
    [Space(20)]
    [Header("UI Only")]
    [SerializeField] private Sprite skillSprite;

    public int SkillLevel => skillLevel;
    public int SkillMaxLevel => skillMaxLevel;
    //public int[] LevelUpCosts => levelUpCosts;
    public Sprite SkillSprite => skillSprite;



    public bool IsMaxed()
    {
        if (skillLevel < skillMaxLevel)
            return false;
        return true;
    }
    public int CurrentLevelUpCost()
    {
        if (!IsMaxed())
        {
            return levelUpCosts[skillLevel];
        }
        else
        {
            Debug.Log("Skill already maxed, this shouldn't happen. Returning 0");
            return 0;
        }
    }



    public virtual void LevelUpSkill(/*PlayerStats stats*/)
    {
        skillLevel++;
        ApplySkillEffectsToPlayer(/*stats*/);
    }

        // add funciontality
    protected virtual void ApplySkillEffectsToPlayer(/*PlayerStats stats*/)
    {

    }
}
