using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCannon : MonoBehaviour,ICannon
{
    [SerializeField] private ItemCannon cannonStats;
    [SerializeField] float timeBetweenShots = 0.8f;

    [SerializeField] private float numberOfShots = 1;

    [SerializeField] float cooldownTimer;

    public void Start()
    {
        cooldownTimer = cannonStats.ItemReloadSpeedMod;
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
        //if (numberOfShots == 0)
        //{
        //    Vector3 temp = new Vector3(shootPosition.transform.position.x, shootPosition.transform.position.y, shootPosition.transform.position.z);

        //    var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
        //}

        for (int i = 0; i < numberOfShots; i++)
        {
            Vector3 temp = new Vector3(shootPosition.position.x, shootPosition.position.y, shootPosition.position.z);

            var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
            var ball = ObjectPool.Instance.FetchSpeedPool(temp);

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
