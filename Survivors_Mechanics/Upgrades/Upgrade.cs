using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] protected PlayerStats playerStats;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;

    public Sprite _Sprite => _sprite;
    public string _Name => _name;
    public string _Description => _description;


    public  virtual void ApplyThisUpgradeToPlayer()
    {

    }
}
