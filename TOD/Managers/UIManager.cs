using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HUD elements")]
    [SerializeField] Image _flagIcon;
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] Image _bossHealthBar;
    [SerializeField] TextMeshProUGUI _enemiesLeftText;
    [SerializeField] Image _playerHealthBar;
    [SerializeField] Image _specialAttackIcon;
    [SerializeField] Image _specialAttackBar;
    [SerializeField] Transform _windIndicator;

    [Header("HUD / Inventory Holder")]
    [SerializeField] Image _cannons;
    [SerializeField] Image _armor;
    [SerializeField] Image _sails;
    [SerializeField] Image _rudder;
    [SerializeField] Image _crew;


    private void OnEnable()
    {
        HUDEvents.PlayerHealthUpdateEvent += OnPlayerHealthUpdateEvent;
        HUDEvents.HUDInventoryUpdateEvent += OnHUDInventoryUpdateEvent;
        HUDEvents.WindDirectionEvent += OnWindDIrectionEventMethod;
        HUDEvents.GoldUpdateEvent+= OnGoldUpdateEvent;
        HUDEvents.EnemyCountEvent += OnEnemyCountEvent;
    }

    private void OnDisable()
    {
        HUDEvents.PlayerHealthUpdateEvent -= OnPlayerHealthUpdateEvent;
        HUDEvents.HUDInventoryUpdateEvent -= OnHUDInventoryUpdateEvent;
        HUDEvents.WindDirectionEvent -= OnWindDIrectionEventMethod;
        HUDEvents.GoldUpdateEvent -= OnGoldUpdateEvent;
        HUDEvents.EnemyCountEvent += OnEnemyCountEvent;
    }



    private void OnPlayerHealthUpdateEvent(PlayerHealthUpdateEventData healthUpdateEventData)
    {
        _playerHealthBar.fillAmount = healthUpdateEventData.PlayerHealth;
    }

    private void OnHUDInventoryUpdateEvent(HUDInventoryUpdateEventData myHUDInventoryData)
    {
        _cannons.sprite = myHUDInventoryData.Cannon;
        _armor.sprite = myHUDInventoryData.Armor;
        _sails.sprite = myHUDInventoryData.Sails;
        _rudder.sprite= myHUDInventoryData.Rudder;
        _crew.sprite = myHUDInventoryData.Crew;
    }


    private void OnWindDIrectionEventMethod(WindDirectionData myWindDirectionData)
    {
        _windIndicator.localEulerAngles = myWindDirectionData.WindDirection;
    }

    private void OnGoldUpdateEvent(GoldUpdateEventData myGoldUpdateData)
    {
        _goldText.text = myGoldUpdateData.GoldAmount.ToString();
    }

    // dodati FlagIconChangeEvent
    // dodati BossHealthUpdateEvent
    // dodati BossHealthUpdateEvent

    private void OnEnemyCountEvent(EnemyCountData myEnemyCountData)
    {
        _enemiesLeftText.text = myEnemyCountData.EnemyCount.ToString();
    }

    //dodati dva listenera na SpecialAttackEvent

    //private void OnHUDUpdateEvent(HUDUpdateEventData myHUDUpdateEventData)
    //{
    //    _flagIcon.sprite = myHUDUpdateEventData.FlagIcon;
    //    _goldText.text = myHUDUpdateEventData.GoldText.ToString();
    //    _bossHealthBar.fillAmount = myHUDUpdateEventData.BossHealth;
    //    _enemiesLeftText.text = myHUDUpdateEventData.EnemiesLeftText.ToString();
    //    _playerHealthBar.fillAmount = myHUDUpdateEventData.PlayerHealth;
    //    _specialAttackIcon.sprite = myHUDUpdateEventData.SpecialAttackIcon;
    //    _specialAttackBar.fillAmount = myHUDUpdateEventData.SpecialAttackBar;
    //}










    //public void HPUIUpdate(float hpCurrent)
    //{
    //    hpImg.fillAmount = hpCurrent / hpMax; ==> pvo prebaciti u healthpoints skriptu
    //}

}
