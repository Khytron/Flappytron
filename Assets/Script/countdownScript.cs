using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class countdownScript : MonoBehaviour
{
    public ScriptManager scriptManager;
    public Text countdownText;

    private void Start()
    {
        scriptManager = GameObject.FindGameObjectWithTag("Script Manager").GetComponent<ScriptManager>();
        scriptManager.isDoingCountdown = true;
        countdownText.text = scriptManager.countdownText;
    }

    private void Update()
    {
        if (countdownText.text != scriptManager.countdownText)
        {
            countdownText.text = scriptManager.countdownText;
        }
    }

}
