using UnityEngine;

public class EndTimesSwitch : MonoBehaviour
{
    private void Start()
    {
        bool timerEnabled = FindObjectOfType<SettingsHolder>().isTimerEnabled();

        if (!timerEnabled)
        {
            this.gameObject.SetActive(false);
        }
    }
}