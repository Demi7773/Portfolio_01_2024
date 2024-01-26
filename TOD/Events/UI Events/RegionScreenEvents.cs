using System;
using UnityEngine;

public static class RegionScreenEvents
{
    public static Action<RegionScreenButtonEventData> RegionScreenButtonEvent;
}

public class RegionScreenButtonEventData
{
    public LevelName LevelName;

    public RegionScreenButtonEventData(LevelName levelName)
    {
        LevelName = levelName;
    }
}

public enum LevelName
{
    MainMenu = 1,
    Level_01 = 2,
    Level_02 = 3,
    Level_03 = 4,
    Level_04 = 5,
    Level_05 = 6,
    Level_06 = 7,
    Level_07 = 8,
    Level_08 = 9,
    Level_09 = 10,
    Level_10 = 11,
    Level_11 = 12,
    Level_12 = 13
}
