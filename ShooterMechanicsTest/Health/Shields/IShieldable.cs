public interface IShieldable
{
    public void LoseShield(float dmgAmount);
    public void HealShield(float healAmount);


    public void LoseShieldPercentCurrent(float dmgPercent);
    public void LoseShieldPercentMax(float dmgPercent);
    public void HealShieldPercentCurrent(float healPercent);
    public void HealShieldPercentMax(float healPercent);


    public void InstaLoseAllShields();
    public void HealShieldsToFull();
}
