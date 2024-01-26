using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns")]
public class GunScriptable : ItemScriptable
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDmg;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    [SerializeField] private float aimBloom;
    [SerializeField] private float recoilIntensityX;
    [SerializeField] private float recoilIntensityY;

    public GameObject ProjectilePrefab => projectilePrefab;
    public float TimeBetweenShots => timeBetweenShots;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileDmg => projectileDmg;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;
    public float AimBloom => aimBloom;
    public float RecoilIntensityX => recoilIntensityX;
    public float RecoilIntensityY => recoilIntensityY;
}
