using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipes : MonoBehaviour
{
    public static float speed = 5f;
    public static float speedUpperLimit = 10f;
    public static float speedLowerLimit = 5f;
    private static bool increasingSpeed = true;
    private float leftEdge;
    private static int pipeCount = 1; 
    private const int pipesToIncreaseSpeed = 10;
    private const float speedIncrement = 1f; 
    public float gapSize = 4.5f;
    public float minGapSize = 3.0f;
    public Transform topPipe;
    public Transform bottomPipe;
    public float verticalAmplitude = 1.8f; // How far up and down the pipe moves
    public float verticalFrequency = 3.0f; // How fast the pipe moves up and down
    private float initialY;
    private float timeMoved;
    private float randomDir;
    private float randomSpeed;
    private static bool isMovable = false;

    public void SetGapSize(float newGapSize)
    {
        float minGapSize = 3.0f;
        gapSize = Mathf.Max(newGapSize, minGapSize);

        

    }
    private void Start()
    {
        SetGapSize(gapSize);
        leftEdge = -12.5f;
        initialY = transform.position.y; // Store the starting Y position
        timeMoved = 0.0f;
        randomSpeed = Mathf.Clamp(Random.value, 0.3f, 0.7f); // Randomly pick between 0.2 to 0.8
        randomDir = Random.Range(0, 2) == 0 ? -1.0f : 1.0f; // Randomly pick -1 or +1 

        
    }

    public static void ResetSpeed()
    {
        speed = 5f; // Resets the speed to the initial value
        pipeCount = 0; // Resets the pipe counter to 0
        isMovable = false;
}

    private void Update()
    {
        // Move pipe horizontally to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        
        // Only move up and down if isMovable is true
        if (isMovable)
        {
            movePipeVertical();
        }
        else
        {
            // Keep Y position fixed
        }


        // If a pipe went off screen
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);

            pipeCount++;

            // Toggle isMovable every 15 pipe
            if (pipeCount % 15 == 0)
            {
                isMovable = !isMovable;
                Debug.Log("Pipe movement toggled. isMovable: " + isMovable);
            }


            if (pipeCount % pipesToIncreaseSpeed == 0) // Increase speed every (refer pipesToIncreaseSpeed var)
            {
                if (increasingSpeed)
                {
                    increaseSpeed(speedIncrement);
                } else
                {
                    decreaseSpeed(speedIncrement);
                }
                
            }

            if (pipeCount % 10 == 0) // Decrease gap size every 5 pipes
            {
                SetGapSize(gapSize - 0.5f); // Adjust the decrement value as needed
                // Debug.Log("Gap size decreased to: " + gapSize);

                if (topPipe != null && bottomPipe != null)
                {
                    topPipe.localPosition = new Vector3(topPipe.localPosition.x, gapSize / 2f, topPipe.localPosition.z);
                    bottomPipe.localPosition = new Vector3(bottomPipe.localPosition.x, -gapSize / 2f, bottomPipe.localPosition.z);
                }
            }

        }    
    }


    private void movePipeVertical()
    {
        float newY = transform.position.y;
        timeMoved += 1.0f * Time.deltaTime;
        newY = initialY + Mathf.Sin(timeMoved * verticalFrequency * randomSpeed) * verticalAmplitude * randomDir;
        // Debug.Log("New Y: " + newY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void increaseSpeed(float speedIncrement)
    {
        if (speed < speedUpperLimit) // If speed is less than speed upper limit
        {
            speed += speedIncrement; // Increase speed (adjust the value as needed)
            Debug.Log("Speed increased to: " + speed);
        } else
        {
            increasingSpeed = false;
        }
    }

    private void decreaseSpeed(float speedIncrement)
    {
        if (speed > speedLowerLimit) // If speed is more than speed lower limit
        {
            speed -= speedIncrement; // Decrease speed (adjust the value as needed)
            Debug.Log("Speed decreased to: " + speed);
        } else
        {
            increasingSpeed = true;
        }
    }

}


   

