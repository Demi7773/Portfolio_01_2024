using System.Collections;
using UnityEngine;

public class PlayerCannonball : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] float deactivateTimer;
    IEnumerator DeactivateObject;

    private void OnEnable()
    {
        Invoke(nameof(DeactivateSelfSAFE), 3f);

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            //gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(DeactivateSelf(deactivateTimer));
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent</*EnemyHPTemp*//*EnemyBehaviour*/IEnemy>().LoseHP(damage);
            //gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(DeactivateSelf(0));
            AudioEvents.PlayShipDamagedSoundsEvent?.Invoke();
        }
        else if (collision.gameObject.CompareTag("Terrain"))
        {
            // gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(DeactivateSelf(deactivateTimer));
        }
        else
        {
            StartCoroutine(DeactivateSelf(deactivateTimer));
        }
    }

    IEnumerator DeactivateSelf(float deactivateTimer)
    {
        yield return new WaitForSeconds(deactivateTimer);
        this.gameObject.SetActive(false);
        //StopCoroutine(DeactivateObject);

    }

    void DeactivateSelfSAFE()
    {
        this.gameObject.SetActive(false);
    }
}
