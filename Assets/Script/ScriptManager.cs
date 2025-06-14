using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour
{
    public bool QuizSuccess = false;
    public bool isQuizzing = false;
    public string QuizAnswer;
    public bool isDoingCountdown = false;
    public string countdownText = "3";
    public int countdownNumber = 3;

    public void UpdateCountdown()
    {
        countdownNumber -= 1;
        countdownText = countdownNumber.ToString();
    }


    public void ResetCountdown()
    {
        isDoingCountdown = false;
        countdownNumber = 3;
        countdownText = "3";
    }
}
