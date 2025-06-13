using UnityEngine;
using UnityEngine.SceneManagement;

public class Pipes : MonoBehaviour
{
    public static float speed = 5f;
    public float speedLimit = 25f;
    private float leftEdge;
    private static int pipeCount = 0; 
    private const int pipesToIncreaseSpeed = 10;
    private const float speedIncrement = 2f; 
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
        randomSpeed = Random.value;
        randomDir = Random.Range(0, 2) == 0 ? -1.0f : 1.0f; // This will randomly pick -1 or +1 floating number
    }

    public static void ResetSpeed()
    {
        speed = 5f; // Resets the speed to the initial value
        pipeCount = 0; // Resets the pipe counter to 0
        isMovable = false;
}

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        
        // Only move up and down if isMovable is true
        float newY = transform.position.y;
        if (isMovable)
        {
            timeMoved += 1.0f * Time.deltaTime;
            newY = initialY + Mathf.Sin( timeMoved * verticalFrequency * randomSpeed) * verticalAmplitude * randomDir;
            // Debug.Log("New Y: " + newY);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
        else
        {
            // Keep Y position fixed
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
        }



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


            if (pipeCount % pipesToIncreaseSpeed == 0)
            {
                if (speed < speedLimit) // If speed is less than speed limit
                {
                    speed += speedIncrement; // Increase speed (adjust the value as needed)
                    // Debug.Log("Speed increased to: " + speed);
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
    }

