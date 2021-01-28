using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPoint : MonoBehaviour
{
    [SerializeField] float countdownDelay = 5f;
    [SerializeField] float cleanseFXDuration = 1f;
    [SerializeField] string nextOSTName = null;
    [SerializeField] bool isStart = false;
    float restCountdown = 0f;
    bool playerOnRestPoint = false;
    bool isActivated = false;

    private void Start() 
    {
        if (isStart)
        {
            FindObjectOfType<AudioManager>().Play("LevelStart");
        }
    }

    private void Update()
    {
        if (playerOnRestPoint && restCountdown < countdownDelay)
        {
            restCountdown += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<BurdenManager>().GetBurdenNumber() > 0)
        {
            playerOnRestPoint = true;
            FindObjectOfType<Timer>().StopTimer();
            FindObjectOfType<AudioManager>().Play("Rest");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<BurdenManager>().GetBurdenNumber() == 0)
            {
                GetComponent<Animator>().SetBool("isResting", false);
                return;
            }

            if (restCountdown >= countdownDelay && !isActivated)
            {
                StartCoroutine(CleanseFX(other.gameObject));
                isActivated = true;
            }
            else
            {
                RestFX();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
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
        FindObjectOfType<MusicPlayer>().Play(nextOSTName);
        FindObjectOfType<AudioManager>().Play("LevelEnd2");
        FindObjectOfType<AudioManager>().Play("LevelEnd");
        yield return new WaitForSeconds(cleanseFXDuration);
        FindObjectOfType<SceneLoader>().LoadNextScene();
    }

    private void RestFX()
    {
        GetComponent<Animator>().SetBool("isResting", true);
    }
}
