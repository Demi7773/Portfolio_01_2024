using System.Collections;
using UnityEngine;

public class SlashBehaviour : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float duration;
    [SerializeField] protected float forwardSpeed;



    protected void OnEnable()
    {
        StartCoroutine(DeactivationTimer());
        StartCoroutine(MoveForwardOverTime());
    }
    protected void OnDisable()
    {
        StopAllCoroutines();
    }



    public void SetMyStats(float dmg, float dur, float fwdSpeed)
    {
        damage = dmg;
        duration = dur;
        forwardSpeed = fwdSpeed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyStupid>() != null)
        {
            other.gameObject.GetComponent<EnemyStupid>().TakeDamage(damage);
            Debug.Log("Enemy hit for " + damage);
        }
        else
        {
            Debug.Log("Trigger Object has no EnemyStupid Component attached!");
        }
    }



    protected IEnumerator MoveForwardOverTime()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return null;
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        }
    }
    protected IEnumerator DeactivationTimer()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
