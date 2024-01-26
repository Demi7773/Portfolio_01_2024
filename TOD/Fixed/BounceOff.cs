using System.Collections;
using UnityEngine;

public class BounceOff : MonoBehaviour
{
    //[SerializeField] float HPDamage=10f;
    [SerializeField] float PlayerDrag = 10f;
    [SerializeField] float PlayerAngDrag = 10f;

    public float bounce = 6f;
    public bool dragApply = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

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
