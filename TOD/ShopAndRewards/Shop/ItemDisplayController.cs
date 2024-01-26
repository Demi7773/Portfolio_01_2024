using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEvents;

public class ItemDisplayController : MonoBehaviour
{
    private const string NOT_AVAILABLE = "n/A";

    public GameObject StatsScreen;
    public Button ItemButton;
    public Button BuyItemButton;
    public Image ItemImage;

    public TextMeshProUGUI ItemPrice;
    public TextMeshProUGUI ItemName;
    //public TextMeshProUGUI ItemDescription;


    public TextMeshProUGUI HealthCurrent;
    public TextMeshProUGUI HealthModifier;

    public TextMeshProUGUI ArmorCurrent;
    public TextMeshProUGUI ArmorModifier;

    public TextMeshProUGUI SpeedCurrent;
    public TextMeshProUGUI SpeedModifier;

    public TextMeshProUGUI MobilityCurrent;
    public TextMeshProUGUI MobilityModifier;

    public TextMeshProUGUI DamageCurrent;
    public TextMeshProUGUI DamageModifier;

    public TextMeshProUGUI ReloadSpeedCurrent;
    public TextMeshProUGUI ReloadSpeedModifier;

    public TextMeshProUGUI RangeCurrent;
    public TextMeshProUGUI RangeModifier;

    private ItemBase _shopItem;


    // ***Ja dodao, povezan event koji ih updatea po potrebi***
    [Header("Dependencies")]
    [SerializeField] private GameObject player;
    [SerializeField] private EquipmentController equipmentController;
    [SerializeField] private PlayerMoneyScript moneyScript;



    private void OnEnable()
    {
        PlayerEvents.PlayerGO += SetPlayerReferences;
    }
    private void OnDisable()
    {
        PlayerEvents.PlayerGO -= SetPlayerReferences;
    }

    private void SetPlayerReferences(PlayerGOReference Player)
    {
        player = Player.playerGO;
        equipmentController = player.GetComponent<PlayerHPScript>().Equipment;
        moneyScript = player.GetComponent<PlayerMoneyScript>();
    }



    public void SetShopItem(ItemBase shopItem)
    {
        _shopItem = shopItem;

        ItemButton.interactable = true;
        BuyItemButton.interactable = true;

        UpdateVisuals();
    }

    public void ToggleStatScreen(bool setActive)
    {
        StatsScreen.SetActive(setActive);
    }



    public void TryToBuyItem()
    {
        if (moneyScript == null)
        {
            Debug.Log("Money Script Null");
            PlayerEvents.NeedPlayerReference?.Invoke();
        }
        else if (/*PlayerMoneyStatic.PlayerMoney < _rewardItem.ItemPrice*/
            moneyScript.PlayerMoney < _shopItem.ItemPrice)
        {   
            Debug.Log("Not enough Money");
        }
        else
        {
            BuyItem();
        }
    }
    public void BuyItem()
    {
        /*PlayerMoneyStatic.LoseMoney(_rewardItem.ItemPrice);*/
        moneyScript.LoseMoney(_shopItem.ItemPrice);
        equipmentController.DetermineItemTypeAndEquipNewItem(_shopItem);
    }

    private void UpdateVisuals()
    {
        ItemImage.sprite = _shopItem.ItemSprite;
        ItemPrice.text = _shopItem.ItemPrice.ToString();
        ItemName.text = _shopItem.ItemName;
       // ItemDescription.text = _rewardItem.ItemDescription;

        float startingHP = equipmentController.PlayerHPStat;
        float startingArmor = equipmentController.PlayerDmgReducStat;
        float startingSpeed = equipmentController.PlayerSpeedStat;
        float startingMobility = equipmentController.PlayerStoppingStat + equipmentController.PlayerTurnStat;
        float startingDamage = equipmentController.PlayerDmgStat;
        float startingReload = equipmentController.PlayerReloadSpeedStat;
        float startingRange = equipmentController.PlayerRangeStat;

        HealthCurrent.text = startingHP.ToString();
        ArmorCurrent.text = startingArmor.ToString();
        SpeedCurrent.text = startingSpeed.ToString();
        MobilityCurrent.text = startingMobility.ToString();
        DamageCurrent.text = startingDamage.ToString();
        ReloadSpeedCurrent.text = startingReload.ToString();
        RangeCurrent.text = startingRange.ToString();

        float newHealth = startingHP;
        float newArmor = startingArmor;
        float newSpeed = startingSpeed;
        float newMobiliy = startingMobility;
        float newDamage = startingDamage;
        float newReloadSpeed = startingReload;
        float newRange = startingRange;

        if (_shopItem is ItemCannon)
        {
            newHealth -= equipmentController.EquippedCannons.ItemHPMod;
            if (equipmentController.EquippedCannons.Type == ItemCannon.CannonType.Glass)
                newArmor *= 2f;
            newArmor -= equipmentController.EquippedCannons.ItemDmgReducMod;
            if ((_shopItem as ItemCannon).Type == ItemCannon.CannonType.Glass)
            {
                newArmor *= 0.5f;
            }
            newSpeed -= equipmentController.EquippedCannons.ItemSpeedMod;
            newMobiliy -= (equipmentController.EquippedCannons.ItemStoppingMod + equipmentController.EquippedCannons.ItemTurnRateMod);
            newDamage -= equipmentController.EquippedCannons.ItemDmgMod;
            newReloadSpeed -= equipmentController.EquippedCannons.ItemReloadSpeedMod;
            newArmor -= equipmentController.EquippedCannons.ItemRangeMod;
        }
        else if (_shopItem is ItemArmor)
        {
            newHealth -= equipmentController.EquippedArmor.ItemHPMod;
            newArmor -= equipmentController.EquippedArmor.ItemDmgReducMod;
            newSpeed -= equipmentController.EquippedArmor.ItemSpeedMod;
            newMobiliy -= (equipmentController.EquippedArmor.ItemStoppingMod + equipmentController.EquippedArmor.ItemTurnRateMod);
            newDamage -= equipmentController.EquippedArmor.ItemDmgMod;
            newReloadSpeed -= equipmentController.EquippedArmor.ItemReloadSpeedMod;
            newRange -= equipmentController.EquippedArmor.ItemRangeMod;
        }
        else if (_shopItem is ItemSails)
        {
            newHealth -= equipmentController.EquippedSails.ItemHPMod;
            newArmor -= equipmentController.EquippedSails.ItemDmgReducMod;
            newSpeed -= equipmentController.EquippedSails.ItemSpeedMod;
            newMobiliy -= (equipmentController.EquippedSails.ItemStoppingMod + equipmentController.EquippedSails.ItemTurnRateMod);
            newDamage -= equipmentController.EquippedSails.ItemDmgMod;
            newReloadSpeed -= equipmentController.EquippedSails.ItemReloadSpeedMod;
            newRange -= equipmentController.EquippedSails.ItemRangeMod;
        }
        else if (_shopItem is ItemRudder)
        {
            newHealth -= equipmentController.EquippedRudder.ItemHPMod;
            newArmor -= equipmentController.EquippedRudder.ItemDmgReducMod;
            newSpeed -= equipmentController.EquippedRudder.ItemSpeedMod;
            newMobiliy -= (equipmentController.EquippedRudder.ItemStoppingMod + equipmentController.EquippedRudder.ItemTurnRateMod);
            newDamage -= equipmentController.EquippedRudder.ItemDmgMod;
            newReloadSpeed -= equipmentController.EquippedRudder.ItemReloadSpeedMod;
            newRange -= equipmentController.EquippedRudder.ItemRangeMod;
        }
        else if (_shopItem is ItemCrew)
        {
            newHealth -= equipmentController.EquippedCrew.ItemHPMod;
            newArmor -= equipmentController.EquippedCrew.ItemDmgReducMod;
            newSpeed -= equipmentController.EquippedCrew.ItemSpeedMod;
            newMobiliy -= (equipmentController.EquippedCrew.ItemStoppingMod + equipmentController.EquippedCrew.ItemTurnRateMod);
            newDamage -= equipmentController.EquippedCrew.ItemDmgMod;
            newReloadSpeed -= equipmentController.EquippedCrew.ItemReloadSpeedMod;
            newRange -= equipmentController.EquippedCrew.ItemRangeMod;
        }

        HealthModifier.text = (newHealth + _shopItem.ItemHPMod).ToString();
        ArmorCurrent.text = (newArmor + _shopItem.ItemDmgReducMod).ToString();
        SpeedModifier.text = (newSpeed + _shopItem.ItemSpeedMod).ToString();
        MobilityModifier.text = (newMobiliy + _shopItem.ItemStoppingMod + _shopItem.ItemTurnRateMod).ToString();
        DamageModifier.text = (newDamage + _shopItem.ItemDmgMod).ToString();
        ReloadSpeedModifier.text = (newReloadSpeed + _shopItem.ItemReloadSpeedMod).ToString();
        RangeModifier.text = (newRange + _shopItem.ItemRangeMod).ToString();


        // Nez jel nam ovo treba, ali ako da onda za svaki item
        // if (_rewardItem.Speed == 0f)
        // {
        //     SpeedModifier.text = NOT_AVAILABLE;
        // }
        // *****************************************************************
    }
}
