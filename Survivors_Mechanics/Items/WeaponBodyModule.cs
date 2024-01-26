using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponBodyModule", menuName = "Modules/WeaponBodyModule")]
public class WeaponBodyModule : Module
{
    [Header("Model and Type")]
    [SerializeField] private BodyType type;
    [SerializeField] private GameObject weaponGOPrefab;
    [SerializeField] private List<GameObject> allWeaponTypePrefabs = new List<GameObject>();

    [Header("Stats")]
    [SerializeField, Range(1f, 100000f)] private float damageBase = 1f;
    [SerializeField, Range(0.1f, 30f)] private float shotsPerSecondBase = 1f;
    [SerializeField, Range(0f, 180f)] private float ammoSpreadBase = 1f;
    [SerializeField, Range(0f, 10f)] private float reloadDurationBase = 1f;

    [SerializeField] private int ammoMax = 10;
    [SerializeField] private int ammoTotalCapacity = 100;
    [SerializeField, Range(0.01f, 10f)] private float ammoTotalRechargePerSecBase = 0.01f;


    public BodyType Type => type;
    public float DamageBase => damageBase;
    public float ShotsPerSecondBase => shotsPerSecondBase;
    public float AmmoSpreadBase => ammoSpreadBase;
    public float ReloadDurationBase => reloadDurationBase;
    public int AmmoMax => ammoMax;
    public int AmmoTotalCapacity => ammoTotalCapacity;
    public float AmmoTotalRechargePerSecBase => ammoTotalRechargePerSecBase;
    public GameObject WeaponGOPrefab => CurrentWeaponTypeGO();

    private GameObject CurrentWeaponTypeGO()
    {
        weaponGOPrefab = allWeaponTypePrefabs[(int)type];
        return weaponGOPrefab;

        //switch (type)
        //{
        //    case BodyType.UsePistol:

        //        break;
        //    case BodyType.UseShotgun:
        //        break;
        //    case BodyType.UseSemiAutoRifle:
        //        break;
        //    case BodyType.AutoRifle:
        //        break;
        //    case BodyType.Sniper:
        //        break;
        //    case BodyType.ExplosiveWeapon:
        //        break;
        //    case BodyType.SpecialWeapon:
        //        break;
        //}
    }

    public enum BodyType
    {
        Pistol = 0,
        Shotgun = 1,
        SemiAutoRifle = 2,
        AutoRifle = 3,
        Sniper = 4,
        ExplosiveWeapon = 5,
        SpecialWeapon = 6
    }
}
