using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class SpecialAttackVFXController : MonoBehaviour
{
    [SerializeField] protected bool isPaused = false;

    // temp only for smite, myb add empty method to SpecialAttack
    [Header("Debug")]
    [SerializeField] protected Smite specialAttackScript;
    [SerializeField] protected float currentTimer = -100.0f;
    [SerializeField] protected float disableTimer = 1.0f;



    public void InitializeMe(Smite specialAtk, float vfxDuration)
    {
        specialAttackScript = specialAtk;
        disableTimer = vfxDuration;
    }

    private void OnEnable()
    {
        currentTimer = 0;
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
        //StartCoroutine(DisableTimer(disableTimer));
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
        specialAttackScript.AddSpecialEffectToQueue(gameObject);
    }
    protected void PauseMe()
    {
        isPaused = true;
    }
    protected void UnPauseMe()
    {
        isPaused = false;
    }



    private void Update()
    {
        if (!isPaused)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer < disableTimer)
            {
                return;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }


    //IEnumerator DisableTimer(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    gameObject.SetActive(false);
    //    StopAllCoroutines();
    //}
}
