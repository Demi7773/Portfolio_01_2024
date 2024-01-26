using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDDeactivator : MonoBehaviour
{
    private bool isHUDEnabled = false;


    public void DeactivateHUD()  // u shop levelu na trigger s kornjacom ovo invokeati sa false vrijednoscu (doslovno takoreci mozes prekopirati ovu skriptu)

    {
        HUDEvents.HudToggleEvent?.Invoke(new HUDToggleData(isHUDEnabled)); //enableanje HUD panela na pocetku levela
    }
}
