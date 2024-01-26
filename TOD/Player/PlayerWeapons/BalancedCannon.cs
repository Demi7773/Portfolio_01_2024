using System.Collections;
using UnityEngine;

public class BalancedCannon : MonoBehaviour, ICannon
{
    [SerializeField] private ItemCannon cannonStats;
    [SerializeField] float timeBetweenShots = 0.8f;

    [SerializeField] private float numberOfShots = 1;

     [SerializeField] float cooldowntimer = 1;



    public void Start()
    {
        cooldowntimer = cannonStats.ItemReloadSpeedMod;
        numberOfShots = cannonStats.ItemTier;
    }

    public void ActivateCannon(Vector3 position, Transform shootPosition)
    {
        //Debug.Log("RADI");
        StartCoroutine(FireCannonBall(shootPosition, position));
    }

    public void InitializeCannon(ItemCannon stats)
    {
        cannonStats = stats;
        numberOfShots = cannonStats.ItemTier;
        cooldowntimer = cannonStats.ItemReloadSpeedMod;
    }

    IEnumerator FireCannonBall(Transform shootPosition, Vector3 velocity)
    {
        //if (numberOfShots == 0)
        //{
        //    Vector3 temp = new Vector3(shootPosition.transform.velocity.x, shootPosition.transform.velocity.y, shootPosition.transform.velocity.z);

        //    var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
        //}

        for (int i = 0; i < numberOfShots; i++)
        {
            Vector3 temp = new Vector3(shootPosition.position.x, shootPosition.position.y, shootPosition.position.z);

            var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
            var ball = ObjectPool.Instance.FetchBalancedPool(temp);

            ball.GetComponent<PlayerCannonball>().damage = cannonStats.ItemDmgMod;

            ball.GetComponent<Rigidbody>().velocity = velocity;
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public float Cooldown()
    {
        return cannonStats.ItemReloadSpeedMod;
    }
}

