using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterDelay : MonoBehaviour
{
    public void StartDelete(float delay)
    {
        StartCoroutine(Delay(delay));
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }

}
