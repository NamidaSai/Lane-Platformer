﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPoint : MonoBehaviour
{
    [SerializeField] float countdownDelay = 5f;
    [SerializeField] float cleanseFXDuration = 1f;
    float restCountdown = 0f;
    bool playerOnRestPoint = false;

    private void Update()
    {
        if (playerOnRestPoint && restCountdown < countdownDelay)
        {
            restCountdown += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>() != null)
        {
            playerOnRestPoint = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>() != null)
        {
            if (other.GetComponent<BurdenManager>().GetBurdenNumber() == 0)
            {
                GetComponent<Animator>().SetBool("isResting", false);
                return;
            }

            if (restCountdown >= countdownDelay)
            {
                StartCoroutine(CleanseFX(other.gameObject));
            }
            else
            {
                RestFX();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>() != null)
        {
            restCountdown = 0f;
            playerOnRestPoint = false;
            GetComponent<Animator>().SetBool("isResting", false);
            GetComponent<Animator>().ResetTrigger("Cleanse");
        }
    }

    private IEnumerator CleanseFX(GameObject other)
    {
        GetComponent<Animator>().SetTrigger("Cleanse");
        yield return new WaitForSeconds(cleanseFXDuration);
        // other.GetComponent<BurdenManager>().RemoveAllBurdens();
    }

    private void RestFX()
    {
        GetComponent<Animator>().SetBool("isResting", true);
    }
}
