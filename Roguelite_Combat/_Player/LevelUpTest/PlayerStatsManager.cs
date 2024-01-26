using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;



    private void Awake()
    {
        stats.SetStartingStats();
        Debug.Log("Calling staring stats reset");
    }
}
