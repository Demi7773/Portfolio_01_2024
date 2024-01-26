using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerEvents;

public class RewardItemDisplayController : MonoBehaviour
{
    private const string NOT_AVAILABLE = "n/A";

    //public GameObject StatsScreen;
    //public Button ItemButton;
    //public Button BuyItemButton;
    public Image ItemImage;

    public TextMeshProUGUI ItemName;
    //public TextMeshProUGUI ItemDescription;

    //public TextMeshProUGUI HealthCurrent;
    //public TextMeshProUGUI HealthModifier;

    //public TextMeshProUGUI ArmorCurrent;
    //public TextMeshProUGUI ArmorModifier;

    //public TextMeshProUGUI SpeedCurrent;
    //public TextMeshProUGUI SpeedModifier;

    //public TextMeshProUGUI MobilityCurrent;
    //public TextMeshProUGUI MobilityModifier;

    //public TextMeshProUGUI DamageCurrent;
    //public TextMeshProUGUI DamageModifier;

    //public TextMeshProUGUI ReloadSpeedCurrent;
    //public TextMeshProUGUI ReloadSpeedModifier;

    //public TextMeshProUGUI RangeCurrent;
    //public TextMeshProUGUI RangeModifier;

    private ItemBase _rewardItem;

    [Header("Dependencies")]
    [SerializeField] private GameObject player;
    [SerializeField] private EquipmentController equipmentController;



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
    }

    public void SetReward(ItemBase reward)
    {
        _rewardItem = reward;

        //ItemButton.interactable = true;
        //BuyItemButton.interactable = true;

        UpdateVisuals();
    }

    //public void ToggleStatScreen(bool setActive)
    //{
    //    StatsScreen.SetActive(setActive);
    //}

    public void ChooseItem()
    {
        equipmentController.DetermineItemTypeAndEquipNewItem(_rewardItem);
    }


    private void UpdateVisuals()
    {
        ItemImage.sprite = _rewardItem.ItemSprite;
        ItemName.text = _rewardItem.ItemName;
        // ItemDescription.text = _rewardItem.ItemDescription;

        //float startingHP = equipmentController.PlayerHPStat;
        //float startingArmor = equipmentController.PlayerDmgReducStat;
        //float startingSpeed = equipmentController.PlayerSpeedStat;
        //float startingMobility = equipmentController.PlayerStoppingStat + equipmentController.PlayerTurnStat;
        //float startingDamage = equipmentController.PlayerDmgStat;
        //float startingReload = equipmentController.PlayerReloadSpeedStat;
        //float startingRange = equipmentController.PlayerRangeStat;

        //HealthCurrent.text = startingHP.ToString();
        //ArmorCurrent.text = startingArmor.ToString();
        //SpeedCurrent.text = startingSpeed.ToString();
        //MobilityCurrent.text = startingMobility.ToString();
        //DamageCurrent.text = startingDamage.ToString();
        //ReloadSpeedCurrent.text = startingReload.ToString();
        //RangeCurrent.text = startingRange.ToString();

        //float newHealth = startingHP;
        //float newArmor = startingArmor;
        //float newSpeed = startingSpeed;
        //float newMobiliy = startingMobility;
        //float newDamage = startingDamage;
        //float newReloadSpeed = startingReload;
        //float newRange = startingRange;

        //if (_rewardItem is ItemCannon)
        //{
        //    newHealth -= equipmentController.EquippedCannons.ItemHPMod;
        //    if (equipmentController.EquippedCannons.Type == ItemCannon.CannonType.Glass)
        //        newArmor *= 2f;
        //    newArmor -= equipmentController.EquippedCannons.ItemDmgReducMod;
        //    if ((_rewardItem as ItemCannon).Type == ItemCannon.CannonType.Glass)
        //    {
        //        newArmor *= 0.5f;
        //    }
        //    newSpeed -= equipmentController.EquippedCannons.ItemSpeedMod;
        //    newMobiliy -= (equipmentController.EquippedCannons.ItemStoppingMod + equipmentController.EquippedCannons.ItemTurnRateMod);
        //    newDamage -= equipmentController.EquippedCannons.ItemDmgMod;
        //    newReloadSpeed -= equipmentController.EquippedCannons.ItemReloadSpeedMod;
        //    newArmor -= equipmentController.EquippedCannons.ItemRangeMod;
        //}
        //else if (_rewardItem is ItemArmor)
        //{
        //    newHealth -= equipmentController.EquippedArmor.ItemHPMod;
        //    newArmor -= equipmentController.EquippedArmor.ItemDmgReducMod;
        //    newSpeed -= equipmentController.EquippedArmor.ItemSpeedMod;
        //    newMobiliy -= (equipmentController.EquippedArmor.ItemStoppingMod + equipmentController.EquippedArmor.ItemTurnRateMod);
        //    newDamage -= equipmentController.EquippedArmor.ItemDmgMod;
        //    newReloadSpeed -= equipmentController.EquippedArmor.ItemReloadSpeedMod;
        //    newRange -= equipmentController.EquippedArmor.ItemRangeMod;
        //}
        //else if (_rewardItem is ItemSails)
        //{
        //    newHealth -= equipmentController.EquippedSails.ItemHPMod;
        //    newArmor -= equipmentController.EquippedSails.ItemDmgReducMod;
        //    newSpeed -= equipmentController.EquippedSails.ItemSpeedMod;
        //    newMobiliy -= (equipmentController.EquippedSails.ItemStoppingMod + equipmentController.EquippedSails.ItemTurnRateMod);
        //    newDamage -= equipmentController.EquippedSails.ItemDmgMod;
        //    newReloadSpeed -= equipmentController.EquippedSails.ItemReloadSpeedMod;
        //    newRange -= equipmentController.EquippedSails.ItemRangeMod;
        //}
        //else if (_rewardItem is ItemRudder)
        //{
        //    newHealth -= equipmentController.EquippedRudder.ItemHPMod;
        //    newArmor -= equipmentController.EquippedRudder.ItemDmgReducMod;
        //    newSpeed -= equipmentController.EquippedRudder.ItemSpeedMod;
        //    newMobiliy -= (equipmentController.EquippedRudder.ItemStoppingMod + equipmentController.EquippedRudder.ItemTurnRateMod);
        //    newDamage -= equipmentController.EquippedRudder.ItemDmgMod;
        //    newReloadSpeed -= equipmentController.EquippedRudder.ItemReloadSpeedMod;
        //    newRange -= equipmentController.EquippedRudder.ItemRangeMod;
        //}
        //else if (_rewardItem is ItemCrew)
        //{
        //    newHealth -= equipmentController.EquippedCrew.ItemHPMod;
        //    newArmor -= equipmentController.EquippedCrew.ItemDmgReducMod;
        //    newSpeed -= equipmentController.EquippedCrew.ItemSpeedMod;
        //    newMobiliy -= (equipmentController.EquippedCrew.ItemStoppingMod + equipmentController.EquippedCrew.ItemTurnRateMod);
        //    newDamage -= equipmentController.EquippedCrew.ItemDmgMod;
        //    newReloadSpeed -= equipmentController.EquippedCrew.ItemReloadSpeedMod;
        //    newRange -= equipmentController.EquippedCrew.ItemRangeMod;
        //}

        //HealthModifier.text = (newHealth + _rewardItem.ItemHPMod).ToString();
        //ArmorCurrent.text = (newArmor + _rewardItem.ItemDmgReducMod).ToString();
        //SpeedModifier.text = (newSpeed + _rewardItem.ItemSpeedMod).ToString();
        //MobilityModifier.text = (newMobiliy + _rewardItem.ItemStoppingMod + _rewardItem.ItemTurnRateMod).ToString();
        //DamageModifier.text = (newDamage + _rewardItem.ItemDmgMod).ToString();
        //ReloadSpeedModifier.text = (newReloadSpeed + _rewardItem.ItemReloadSpeedMod).ToString();
        //RangeModifier.text = (newRange + _rewardItem.ItemRangeMod).ToString();
    }
}
