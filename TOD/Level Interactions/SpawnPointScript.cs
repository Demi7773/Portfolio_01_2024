using UnityEngine;
using static PlayerEvents;

public class SpawnPointScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void OnEnable()
    {
        PlayerEvents.PlayerGO += SetPlayerReference;
        PlayerEvents.LevelStart += TeleportPlayerToMe;
    }
    private void OnDisable()
    {
        PlayerEvents.PlayerGO -= SetPlayerReference;
        PlayerEvents.LevelStart -= TeleportPlayerToMe;
    }
    private void SetPlayerReference(PlayerGOReference Player)
    {
        player = Player.playerGO;
    }
    private void OnLevelStart()
    {
        TeleportPlayerToMe();
    }
    private void TeleportPlayerToMe()
    {
        player.transform.position = transform.position;
        player.transform.forward = transform.forward;
        Debug.Log("Player Teleported to Level Start: " + player.transform.position);
    }
}
