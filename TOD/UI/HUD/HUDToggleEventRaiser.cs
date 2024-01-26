using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ovu skriptu stavi na SVAKI level na neki game object
/// </summary>
public class HUDToggleEventRaiser : MonoBehaviour
{
    private bool isHUDEnabled = true; 
  

    private void Start() //u level manageru ovo invokeati sa false vrijednoscu kad enemy count dode do nule (jer ce se tad raiseati victory panel)
                         // u shop levelu na trigger s kornjacom ovo invokeati sa false vrijednoscu
    {
        HUDEvents.HudToggleEvent?.Invoke(new HUDToggleData(isHUDEnabled)); //enableanje HUD panela na pocetku levela
    }    
}
