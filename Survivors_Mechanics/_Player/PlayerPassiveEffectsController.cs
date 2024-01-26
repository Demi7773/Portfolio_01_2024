using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerEvents;

public class PlayerPassiveEffectsController : MonoBehaviour
{
    [SerializeField] private bool isPaused = false;
    [SerializeField] private List<PassiveEffect> equippedPlayerPassiveEffects = new List<PassiveEffect>();



    private void OnEnable()
    {
        PauseGame += PauseMe;
        UnPauseGame += UnPauseMe;
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        PauseGame -= PauseMe;
        UnPauseGame -= UnPauseMe;
    }
    private void PauseMe()
    {
        isPaused = true;
    }
    private void UnPauseMe()
    {
        isPaused = false;
    }


    
    public void AddEffectToList(PassiveEffect newEffect)
    {
        equippedPlayerPassiveEffects.Add(newEffect);
    }
    private void Update()
    {
        if (!isPaused)
        {
            foreach(PassiveEffect effect in equippedPlayerPassiveEffects)
            {
                effect.TimeTick();
            }
        }
    }
}
