using System;
using UnityEngine;

public static class HUDEvents 
{
    public static Action<PlayerHealthUpdateEventData> PlayerHealthUpdateEvent;
    public static Action<HUDInventoryUpdateEventData> HUDInventoryUpdateEvent;
    public static Action<WindDirectionData> WindDirectionEvent;
    public static Action<GoldUpdateEventData> GoldUpdateEvent;
    public static Action<BossHealthData> BossHealthUpdateEvent;
    public static Action<EnemyCountData> EnemyCountEvent;
    public static Action<HUDToggleData> HudToggleEvent;
    public static Action<BossHPBarToggleData> BossHPBarToggleEvent;
   //public static Action<FlagIconsData> FlagIconChangeEvent;
   //public static Action<SpecialAttackData> SpecialAttackEvent;

}

public class PlayerHealthUpdateEventData
{
    public float PlayerHealth;

    public PlayerHealthUpdateEventData(float playerHealth)
    {
        PlayerHealth = playerHealth;
    }
}

public class HUDInventoryUpdateEventData
{
    public Sprite Cannon; 
    public Sprite Armor; 
    public Sprite Sails; 
    public Sprite Rudder; 
    public Sprite Crew;

    public HUDInventoryUpdateEventData(Sprite cannon, Sprite armor, Sprite sails, Sprite rudder, Sprite crew)
    {
        Cannon = cannon;
        Armor = armor;
        Sails = sails;
        Rudder = rudder;
        Crew = crew;
    }
}

public class WindDirectionData
{
    public Vector3 WindDirection;

    public WindDirectionData(Vector3 windDirection)
    {
        WindDirection = windDirection;
    }
}

public class GoldUpdateEventData
{
    public int GoldAmount;

    public GoldUpdateEventData(int goldAmount)
    {
        GoldAmount = goldAmount;
    }
}


public class BossHealthData
{
    public float BossHealth;

    public BossHealthData(float bossHealth)
    {
        BossHealth = bossHealth;
    }
}

public class EnemyCountData
{
    public int EnemyCount;

    public EnemyCountData(int enemyCount)
    {
        EnemyCount = enemyCount;
    }
}


public class HUDToggleData
{
    public bool HudToggle;

    public HUDToggleData(bool hudToggle)
    {
        HudToggle = hudToggle;
    }
}

public class BossHPBarToggleData
{
    public bool BossHPBarToggle;

    public BossHPBarToggleData(bool bossHPBarToggle)
    {
        BossHPBarToggle = bossHPBarToggle;
    }
}

//public class FlagIconsData
//{
//    public Sprite FlagIcon;

//    public FlagIconsData(Sprite flagIcon)
//    {
//        FlagIcon = flagIcon;
//    }
//}


//public class SpecialAttackData
//{
//    public Sprite SpecialAttackIcon;
//    public float SpecialAttackBarAmount;

//    public SpecialAttackData(Sprite specialAttackIcon, float specialAttackBarAmount)
//    {
//        SpecialAttackIcon = specialAttackIcon;
//        SpecialAttackBarAmount = specialAttackBarAmount;
//    }
//}