using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] private Image hpCurrentImg;
    [SerializeField] private Image shieldsCurrentImg;

    [SerializeField] private Image equippedGunImg;
    [SerializeField] private TextMeshProUGUI equippedGunName;
    [SerializeField] private TextMeshProUGUI equippedGunAmmoTxt;



    // Health
    public void PlayerHPCurrentUI(float hpRatio)
    {
        hpCurrentImg.fillAmount = Mathf.Clamp01(hpRatio);
    }
    public void PlayerShieldsCurrentUI(float shieldsRatio)
    {
        shieldsCurrentImg.fillAmount = Mathf.Clamp01(shieldsRatio);
    }

    // Guns
    public void SetAmmoUI(int ammoCurrent, int ammoMax)
    {
        equippedGunAmmoTxt.text = ammoCurrent + "/" + ammoMax;
    }
    public void SetGunNameAndSprite(string name, Sprite newSprite)
    {
        equippedGunName.text = name;
        equippedGunImg.sprite = newSprite;
    }
}
