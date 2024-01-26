using System.Collections;
using UnityEngine;

public class UnstableCannon : MonoBehaviour, ICannon
{
    [SerializeField] private ItemCannon cannonStats;
    [SerializeField] float timeBetweenShots = 0.8f;

    [SerializeField] private float numberOfShots = 1;

    [SerializeField] float cooldowntimer;

    public void Start()
    {
        cooldowntimer = cannonStats.ItemReloadSpeedMod;
        numberOfShots = cannonStats.ItemTier;
    }

    public void ActivateCannon(Vector3 position, Transform shootPosition)
    {
        Debug.Log("RADI");

        RadnomEffects();

        StartCoroutine(FireCannonBall(shootPosition, position));

    }

    public void InitializeCannon(ItemCannon stats)
    {
        cannonStats = stats;
    }


    IEnumerator FireCannonBall(Transform shootPosition, Vector3 position)
    {


        for (int i = 0; i < numberOfShots; i++)
        {
            Vector3 temp = new Vector3(shootPosition.position.x, shootPosition.position.y, shootPosition.position.z);

            var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
            var ball = ObjectPool.Instance.FetchUnstablePool(temp);

            ball.GetComponent<PlayerCannonball>().damage = cannonStats.ItemDmgMod;

            ball.GetComponent<Rigidbody>().velocity = position;
            yield return new WaitForSeconds(timeBetweenShots);

        }
    }

    public float Cooldown()
    {
        return cooldowntimer;
    }


    void RadnomEffects()
    {
        int RNG = Random.Range(0, 100);

        if (RNG > 0 && RNG < 30)
        {
            numberOfShots = 0;
            cooldowntimer = cannonStats.ItemReloadSpeedMod;
        }
        else if (RNG > 30 && RNG < 60)
        {
            numberOfShots = cannonStats.ItemTier;
            cooldowntimer = cannonStats.ItemReloadSpeedMod;
        }
        else if (RNG > 60 && RNG < 90)
        {
            numberOfShots = cannonStats.ItemTier;
            cooldowntimer = Random.Range(0, 3f);
        }
        else if (RNG > 90 && RNG < 100)
        {
            numberOfShots = cannonStats.ItemTier;
            cooldowntimer = 0;
        }

    }
}
