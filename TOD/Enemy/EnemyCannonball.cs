using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannonball : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] float deactivateTimer;
    [SerializeField] ReturnToPool ReturnHelper;
    IEnumerator DeactivateObject;

    private void OnEnable()
    {
        //Invoke(nameof(DeactivateSelfSAFE), 6f);
       // ReturnHelper.SetActiveTime(deactivateTimer);
    }

    private void OnDisable()
    {
       // ReturnHelper.SetActiveTime(0);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Sea"))
        {
            //gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(DeactivateSelf(deactivateTimer));
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHPScript>().LoseHP(damage);
            //gameObject.GetComponent<Collider>().enabled = false;
            PlayerEvents.PlayerHit?.Invoke();
            StartCoroutine(DeactivateSelf(0f));
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

    //void DeactivateSelfSAFE()
    //{
    //    this.gameObject.SetActive(false);
    //}
}
