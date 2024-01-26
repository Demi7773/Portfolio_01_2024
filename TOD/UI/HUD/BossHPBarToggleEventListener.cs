using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBarToggleEventListener : MonoBehaviour
{
    [SerializeField] private GameObject _bossHPBar;

    private void OnEnable()
    {
        HUDEvents.BossHPBarToggleEvent += BossHPBarToggler;
    }

    private void OnDisable()
    {
        HUDEvents.BossHPBarToggleEvent -= BossHPBarToggler;
    }

    private void BossHPBarToggler(BossHPBarToggleData eventData)
    {
        _bossHPBar.gameObject.SetActive(eventData.BossHPBarToggle);
    }
}
