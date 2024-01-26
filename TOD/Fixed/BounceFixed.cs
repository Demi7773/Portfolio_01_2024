using System.Collections;
using UnityEngine;

public class BounceFixed : MonoBehaviour
{
    [SerializeField] float HPDamage=10f;
    [SerializeField] float PlayerDrag = 10f;
    [SerializeField] float PlayerAngDrag = 10f;
    [SerializeField] GameObject HitVFX;

    public float bounce = 6f;
    public bool dragApply = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            collision.gameObject.GetComponent<PlayerHPScript>().LoseHP(HPDamage);

            Instantiate(HitVFX, collision.GetContact(0).point, Quaternion.identity);
            PlayerEvents.PlayerHit?.Invoke();


            rb.AddForce(-collision.GetContact(0).normal * bounce, ForceMode.Impulse);
            StartCoroutine(ApplyDrag(rb));

        }
    }

    IEnumerator ApplyDrag(Rigidbody player)
    {
        if (!dragApply)
        {
            float tempV = player.drag;
            float tempAV = player.angularDrag;

            player.drag = PlayerAngDrag;
            player.angularDrag = PlayerAngDrag;

            dragApply = true;
            yield return new WaitForSeconds(2f);
            dragApply = false;

            player.drag = tempV;
            player.angularDrag = tempAV;
        }
    }
}
