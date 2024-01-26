using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeCannon : MonoBehaviour,ICannon
{
    [SerializeField] private ItemCannon cannonStats;
    [SerializeField] float timeBetweenShots = 0.8f;


    [SerializeField] private float numberOfShots=1;

    [SerializeField] float cooldowntimer;

    public void Start()
    {
        cooldowntimer = cannonStats.ItemReloadSpeedMod;
        numberOfShots = cannonStats.ItemTier;
    }

    public void ActivateCannon(Vector3 position, Transform shootPosition)
    {
        Debug.Log("RADI");
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
            var ball = ObjectPool.Instance.FetchLongRAngePool(temp);

            ball.GetComponent<PlayerCannonball>().damage = cannonStats.ItemDmgMod;

            ball.GetComponent<Rigidbody>().velocity = position;
            yield return new WaitForSeconds(timeBetweenShots);

        }
    }

    public float Cooldown()
    {
        return cannonStats.ItemReloadSpeedMod;
    }


}

