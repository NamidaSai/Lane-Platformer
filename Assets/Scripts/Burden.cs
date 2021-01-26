using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burden : MonoBehaviour
{
    [SerializeField] float fxDelay = 1f;
    [SerializeField] bool isDrop = false;

    bool isCollected = false;

    private void Start()
    {
        GetComponent<Animator>().SetBool("isDrop", isDrop);
        
        if (isDrop)
        {
            StartCoroutine(ActivateCollider());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected)
        {
            return; 
        }

        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>().CanTakeBurden())
        {
            isCollected = true;
            GetComponent<CircleCollider2D>().enabled = false;
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

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(fxDelay);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
