using System.Collections;
using UnityEngine;
using static PlayerEvents;

public class PlayerReferenceEvent : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerEvents.PlayerGO?.Invoke(new PlayerGOReference(this.gameObject));
        PlayerEvents.NeedPlayerReference += PlayerRefUpdate;
    }
    private void OnDisable()
    {
        PlayerEvents.NeedPlayerReference -= PlayerRefUpdate;
    }

    //private void Awake()
    //{
    //    PlayerRefUpdate();
    //}
    private void Start()
    {
        PlayerRefUpdate();
    }

    private void PlayerRefUpdate()
    {
        StartCoroutine(PlayerRefTimerTest());
    }


    IEnumerator PlayerRefTimerTest()
    {
        PlayerEvents.PlayerGO?.Invoke(new PlayerGOReference(this.gameObject));
        Debug.Log("PlayerReferenceUpdate Ping 1");
        yield return new WaitForSeconds(0.1f);
        PlayerEvents.PlayerGO?.Invoke(new PlayerGOReference(this.gameObject));
        Debug.Log("PlayerReferenceUpdate Ping 2");
    }
}
