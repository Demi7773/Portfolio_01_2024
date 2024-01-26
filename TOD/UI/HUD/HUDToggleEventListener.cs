using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToggleEventListener : MonoBehaviour
{

    [SerializeField] private GameObject _HUDPanel;
    private void OnEnable()
    {
        HUDEvents.HudToggleEvent += HUDPanelToggler;
    }

    private void OnDisable()
    {
        HUDEvents.HudToggleEvent -= HUDPanelToggler;
    }

    private void HUDPanelToggler(HUDToggleData hUDToggleDataInstance)
    {
        _HUDPanel.gameObject.SetActive(hUDToggleDataInstance.HudToggle);
    }
}
