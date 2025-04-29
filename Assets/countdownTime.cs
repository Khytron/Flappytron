using UnityEngine;

public class countdownTime : MonoBehaviour
{
    public float countdown = 3.99f;

    public void decreaseCountdown()
    {
        countdown -= 0.02f;
    }
}
