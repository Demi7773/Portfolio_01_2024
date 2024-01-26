using UnityEngine;
using static PlayerEvents;

public class PlayerPickUpEXP : MonoBehaviour
{
    [SerializeField] protected bool isPaused = false;
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected PlayerXP playerXPScript;
    [Space(20)]
    [Header("Settings")]
    [SerializeField] protected LayerMask pickUpsLayer;
    [SerializeField, Range(1.0f, 10.0f)] protected float pickUpRangeBase = 1.0f;
    [SerializeField] protected float pickUpRangeTotal;



    protected void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
        PlayerStatsChange += UpdateStatsFromPlayerStats;
    }
    protected void OnDisable()
    {
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
        PlayerStatsChange -= UpdateStatsFromPlayerStats;
    }
    protected void PauseMe()
    {
        isPaused = true;
    }
    protected void UnPauseMe()
    {
        isPaused = false;
    }
    protected void UpdateStatsFromPlayerStats()
    {
        playerXPScript = playerStats.PlayerXP;
        pickUpRangeTotal = pickUpRangeBase * playerStats.PickUpEXPRangeMultiplier;
    }



    private void Update()
    {
        if (!isPaused)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, pickUpRangeTotal, pickUpsLayer);

            foreach (Collider hit in hits)
            {
                if (hit.GetComponent<IEXP>() != null)
                {
                    Debug.Log("PickUpEXP tick");
                    playerXPScript.GetXP(hit.GetComponent<IEXP>().PickMeUp());
                }
                else
                {
                    Debug.LogError("IEXP script is null!");
                }
            }
        }
    }
}
