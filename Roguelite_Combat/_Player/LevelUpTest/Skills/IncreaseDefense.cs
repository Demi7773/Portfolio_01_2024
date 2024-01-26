using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseDefense", menuName = "Skills/IncreaseDefense")]
public class IncreaseDefense : PassiveSkill
{
    [SerializeField] protected float[] increaseMaxHPForLevel;
    [SerializeField] protected float[] increaseArmorForLevel;


    protected override void ApplySkillEffectsToPlayer(/*PlayerStats stats*/)
    {
        float hpIncrease = increaseMaxHPForLevel[skillLevel];
        playerStats.ModifyMaxHP(hpIncrease);
        Debug.Log("Player MaxHP increased by: " + hpIncrease);

        float armorIncrease = increaseArmorForLevel[skillLevel];
        playerStats.ModifyArmor(armorIncrease);
        Debug.Log("Player Armor inceased by: " + armorIncrease);
    }
}
