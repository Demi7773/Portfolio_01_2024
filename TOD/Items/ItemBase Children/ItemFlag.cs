using UnityEngine;

[CreateAssetMenu(fileName = "Flag", menuName = "Items/Flags")]
public class ItemFlag : ItemBase
{
    [SerializeField] private int diplomacyLvl;
    [SerializeField] private int randomEncountersAvailable;

    public int DiplomacyLvl => diplomacyLvl;
    public int RandomEncountersAvailable => randomEncountersAvailable;
}
