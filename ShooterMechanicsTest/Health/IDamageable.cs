public interface IDamageable
{
    public void LoseHP(float dmgAmount);
    public void HealHP(float healAmount);


    public void LoseHPPercentCurrent(float dmgPercent);
    public void LoseHPPercentMax(float dmgPercent);
    public void HealHPPercentCurrent(float healPercent);
    public void HealHPPercentMax(float healPercent);


    public void InstaKill();
    public void HealToFull();
}
