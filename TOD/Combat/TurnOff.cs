using System.Collections;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(TurnOffObject());
    }

    IEnumerator TurnOffObject()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }
}
