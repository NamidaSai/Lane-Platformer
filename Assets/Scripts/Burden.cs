using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burden : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>() != null)
        {
            other.GetComponent<BurdenManager>().AddBurden();
            TriggerFX();
        }
    }

    private void TriggerFX()
    {
        Destroy(gameObject);
    }
}
