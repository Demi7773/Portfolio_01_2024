using UnityEngine;

public static class ExtensionMethods
{
    public static float CalculateNumberFromPercentage(float percentage, float value)
    {
        float finalNumber = 0.01f * percentage * value;
        return finalNumber;
    }
}
