using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour, IEXP
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int expValue;



    private void OnDisable()
    {
        levelManager.ReturnEXPPickupToPool(gameObject);
    }


    public void SetMyPool(LevelManager lvlManager)
    {
        levelManager = lvlManager;
    }
    public void SetMyValue(int value)
    {
        expValue = value;
    }
    public int PickMeUp()
    {
        Debug.Log("Picked up EXP, setting active false and sending " + expValue);
        this.gameObject.SetActive(false);
        return expValue;
    }
}
