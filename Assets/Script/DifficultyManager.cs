using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
     public float pipeSpeedIncrement = 0.5f;
    public float gapSizeDecrement = 0.5f;
    public float minGapSize = 3.0f;
    public float gapSize = 4.5f;

    private Pipes pipes;
    void Start()
    {
        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
    }

      public void IncreaseDifficulty()
    {
         Pipes.speed += pipeSpeedIncrement;
         

        // Decrease gap size, ensuring it doesn't go below the minimum
        if (pipes != null)
        {
        pipes.gapSize = Mathf.Max(pipes.gapSize - gapSizeDecrement, minGapSize);
          Debug.Log("Difficulty Increased! Speed: " + Pipes.speed + ", Gap Size Adjusted.");
          }

        
    }
}
