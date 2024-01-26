using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ovu skriptu pospremi u projekt pod _Scripts/Events/UIEvents
/// </summary>

public static class RaisePanelsFromLevelsEvents
{
    public static Action RaiseShopPanelEvent; //kopiraj ovo: "RaisePanelsFromLevels.RaiseShopPanelEvent?.Invoke();" tam na trigger s kornjacom
    public static Action RaiseVictoryPanelEvent; //isto kao i gore
    public static Action RaiseDefeatPanelEvent; //isto kao i gore
}
