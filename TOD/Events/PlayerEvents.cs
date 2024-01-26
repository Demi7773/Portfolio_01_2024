using System;
using UnityEngine;

public static class PlayerEvents
{
    // player ref
    public static Action<PlayerGOReference> PlayerGO;
    public static Action NeedPlayerReference;

    // run / level
    public static Action RunStart;
    public static Action LevelStart;
    public static Action GameOver;

    // shop
    public static Action ShopLevelStart;

    // new equipment
    public static Action NewEquipment;

    // for vfx and audio
    public static Action PlayerHit;


    public class PlayerGOReference
    {
        public GameObject playerGO;
        public PlayerGOReference(GameObject player)
        {
            playerGO = player;
        }
    }
}
