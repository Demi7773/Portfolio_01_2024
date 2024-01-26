using System.Collections;
using UnityEngine;

public class VolleyCannon : MonoBehaviour, ICannon
{
    [SerializeField] private ItemCannon cannonStats;
    [SerializeField] float timeBetweenShots = 0.8f;

    [SerializeField] private float numberOfShots = 1;
    [SerializeField] float timeBetweenVolley = 0.3f;

    public void ActivateCannon(Vector3 position, Transform shootPosition)
    {
        Debug.Log("RADI");
        StartCoroutine(FireCannonBall(shootPosition, position));
    }

    public void InitializeCannon(ItemCannon stats)
    {
        cannonStats = stats;
    }

    public float Cooldown()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator FireCannonBall(Transform shootPosition, Vector3 position)
    {

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {

                Vector3 temp = new Vector3(shootPosition.position.x, shootPosition.position.y, shootPosition.position.z);

                var smoke = ObjectPool.Instance.FetchPooledSmoke(temp);
                var ball = ObjectPool.Instance.FetchVolleyPool(temp);

                ball.GetComponent<PlayerCannonball>().damage = cannonStats.ItemDmgMod;

                ball.GetComponent<Rigidbody>().velocity = position;
                yield return new WaitForSeconds(timeBetweenShots);

            }

        }
        yield return new WaitForSeconds(timeBetweenVolley);

    }
}



