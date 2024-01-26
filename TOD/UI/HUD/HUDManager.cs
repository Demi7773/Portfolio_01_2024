using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("HUD elements")]
    [SerializeField] TextMeshProUGUI _goldText;
    [SerializeField] TextMeshProUGUI _shopGoldText;
    [SerializeField] Image _bossHealthBar;
    [SerializeField] TextMeshProUGUI _enemiesLeftText;
    [SerializeField] Image _playerHealthBar;
    [SerializeField] TextMeshProUGUI _shopHealthText;
    [SerializeField] Transform _windIndicator;
   // [SerializeField] Image _flagIcon;
   // [SerializeField] Image _specialAttackBar;
   // [SerializeField] Image _specialAttackIcon;

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
        HUDEvents.WindDirectionEvent += OnWindDIrectionEvent;
        HUDEvents.GoldUpdateEvent+= OnGoldUpdateEvent;
        HUDEvents.EnemyCountEvent += OnEnemyCountEvent;
        HUDEvents.BossHealthUpdateEvent+= OnBossHealthUpdateEvent;
        //HUDEvents.FlagIconChangeEvent+= OnFlagIconChangeEvent;
       // HUDEvents.SpecialAttackEvent+= OnSpecialAttackEvent;
    }

    private void OnDisable()
    {
        HUDEvents.PlayerHealthUpdateEvent -= OnPlayerHealthUpdateEvent;
        HUDEvents.HUDInventoryUpdateEvent -= OnHUDInventoryUpdateEvent;
        HUDEvents.WindDirectionEvent -= OnWindDIrectionEvent;
        HUDEvents.GoldUpdateEvent -= OnGoldUpdateEvent;
        HUDEvents.EnemyCountEvent -= OnEnemyCountEvent;
        HUDEvents.BossHealthUpdateEvent -= OnBossHealthUpdateEvent;
       // HUDEvents.FlagIconChangeEvent -= OnFlagIconChangeEvent;
       // HUDEvents.SpecialAttackEvent -= OnSpecialAttackEvent;



    }



    private void OnPlayerHealthUpdateEvent(PlayerHealthUpdateEventData healthUpdateEventData)
    {
        _playerHealthBar.fillAmount = healthUpdateEventData.PlayerHealth; //u metodi gdje raiseamo event, podijeliti vrijednost sa 100 da bude u formatu za 0-1 bar
        _shopHealthText.text = (healthUpdateEventData.PlayerHealth * 100) + " / 100"; 
    }

    private void OnHUDInventoryUpdateEvent(HUDInventoryUpdateEventData myHUDInventoryData)
    {
        _cannons.sprite = myHUDInventoryData.Cannon;
        _armor.sprite = myHUDInventoryData.Armor;
        _sails.sprite = myHUDInventoryData.Sails;
        _rudder.sprite= myHUDInventoryData.Rudder;
        _crew.sprite = myHUDInventoryData.Crew;
    }


    private void OnWindDIrectionEvent(WindDirectionData myWindDirectionData)
    {
        _windIndicator.localEulerAngles = myWindDirectionData.WindDirection;
    }

    private void OnGoldUpdateEvent(GoldUpdateEventData myGoldUpdateData)
    {
        _goldText.text = myGoldUpdateData.GoldAmount.ToString() + "g";
        _shopGoldText.text = myGoldUpdateData.GoldAmount.ToString() + "g";
    }



    private void OnBossHealthUpdateEvent(BossHealthData myBossHealthData) //raiseati di treba
    {
        _bossHealthBar.fillAmount = myBossHealthData.BossHealth; //podijelit sa 100 da stane u scale fillera
    }
    

    private void OnEnemyCountEvent(EnemyCountData myEnemyCountData) //raiseat di treba(negdje gdje se racuna enemyhp/enemycount)
    {
        _enemiesLeftText.text = "Targets left: " + myEnemyCountData.EnemyCount.ToString();
    }

    //private void OnSpecialAttackEvent(SpecialAttackData mySpecialAttackData) //raiseat di treba (negdje u special attack skripti)
    //{
    //    // Zakomentirano zasad jer baca error. Pitanje jel imamo uopce Special Attack
    //    //_specialAttackBar.fillAmount = mySpecialAttackData.SpecialAttackBarAmount; //podijelit sa 100 da stane u scale fillera
    //    _specialAttackIcon.sprite = mySpecialAttackData.SpecialAttackIcon;
    //}

    //private void OnFlagIconChangeEvent(FlagIconsData myFlagIconData) //raiseati gdje treba
    //{
    //    _flagIcon.sprite = myFlagIconData.FlagIcon;
    //}
}
