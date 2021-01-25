using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burden : MonoBehaviour
{
    [SerializeField] float fxDelay = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>() != null)
        {
            other.GetComponent<BurdenManager>().AddBurden();
            StartCoroutine(TriggerFX());
        }
    }

    private IEnumerator TriggerFX()
    {
        GetComponent<Animator>().SetTrigger("Collected");
        yield return new WaitForSeconds(fxDelay);
        Destroy(gameObject);
    }
}
