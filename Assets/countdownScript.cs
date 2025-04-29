using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class countdownScript : MonoBehaviour
{
    public float countdownTime = 3f;
    public Text countdownText;
    public float currentTime = 3f;


    public void StartCountdown()
    {
        Debug.Log("Initiating countdown loop");
        while (currentTime > 0)
        {
            
            if (float.TryParse(countdownText.text, out float countdownFloat))
            {
                if (countdownFloat - currentTime > 0.1)
                {
                    countdownText.text = Mathf.CeilToInt(currentTime).ToString();
                    Debug.Log("Updating the countdown text to: " + Mathf.CeilToInt(currentTime).ToString());
                }
            }
            Debug.Log("Current time: " + currentTime);
            currentTime -= Time.deltaTime;
        }

        Debug.Log("Countdown finished!");
        // Dont draw countdown text
        countdownText.enabled = false;
        // This variable keeps track of when countdown is over
        currentTime = 0f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Countdown started!");
        // Draw countdown text
        countdownText.enabled = true;
    }

}
