using System;
using UnityEngine;

public static class PlayerEvents
{
    public static Action<PlayerGOReference> PlayerGO;
    public static Action<PlayerEquipmentReference> EquipmentReference;
    //public static Action<PlayerWeaponReference> CurrentWeaponReference;
    public static Action<OnOrOff> SetHUD;
    public static Action NeedPlayerReference;
    public static Action NeedPlayerEquipmentReference;
    public static Action NeedPlayerWeaponReference;


    public static Action PauseGame;
    public static Action UnPauseGame;


    //public static Action RunStart;
    public static Action LevelStart;
    public static Action LevelEnd;

    public static Action PlayerHPChange;
    public static Action AmmoChange;
    public static Action PlayerXPChange;
    public static Action PlayerLevelUp;
    public static Action PlayerSpecialAttackTick;
    public static Action TimerTick;

    public static Action PlayerChooseUpgrade;
    public static Action PlayerStatsChange;
    //public static Action NewEquipment;


    // Player reference subscribe
    //private void OnEnable()
    //{
    //    PlayerGO += PlayerRefrence;
    //}
    //private void OnDisable()
    //{
    //    PlayerGO -= PlayerRefrence;
    //}
    //private void PlayerRefrence(PlayerGOReference Player)
    //{
    //    player = Player.playerGO;
    //}



    // Player reference request
    //if (player == null)
    //{
    //    NeedPlayerReference?.Invoke();
    //}

    public class PlayerGOReference
    {
        public GameObject playerGO;
        public PlayerGOReference(GameObject player)
        {
            playerGO = player;
        }
    }




    public class PlayerEquipmentReference
    {
        //public WeaponBodyModule WeaponBodyModule;
        //public AmmoModule AmmoModule;
        //public AimModule AimModule;
        //public MuzzleModule MuzzleModule;

        public EquipmentController PlayerEquipmentController;

        public PlayerEquipmentReference(EquipmentController playerEquipmentController/*WeaponBodyModule newWeaponBodyModule, AmmoModule newAmmoModule, AimModule newAimModule, MuzzleModule newMuzzleModule*/)
        {
            PlayerEquipmentController = playerEquipmentController;

            //WeaponBodyModule = newWeaponBodyModule;
            //AmmoModule = newAmmoModule;
            //AimModule = newAimModule;
            //MuzzleModule = newMuzzleModule;
        }
    }



    //public class PlayerWeaponReference
    //{
    //    public UseWeapon EquippedUseWeapon;

    //    public PlayerWeaponReference(UseWeapon equippedUseWeapon)
    //    {
    //        EquippedUseWeapon = equippedUseWeapon;
    //    }
    //}



    public class OnOrOff
    {
        public bool IsTrueOrFalse;

        public OnOrOff(bool active)
        {
            IsTrueOrFalse = active;
        }
    }
}
