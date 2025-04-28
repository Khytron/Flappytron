using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{


    //private Spawner spawner;
    public static float speed = 5f;
    //private static int pipeCount = 0;
    //private float leftEdge;
    public Player player;
    public Text scoreText;
    public Text highScoreText; // high score
    public string highScoreKey = "high score";
    public GameObject playButton;
    public GameObject quitButton;
    public ScriptManager scriptManager;
    public Spawner[] spawner;
    public bool immune; // player immunity
    public float immuneTimer = 0.6f;
    public GameObject gameOver;
    public GameObject answer;
    public Text answerText;
    private int score;

    AudioManager audioManager;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Application.targetFrameRate = 60;
        Pause();
    }
    private void Start()
    {
        spawner = FindObjectsByType<Spawner>(FindObjectsSortMode.None);
        scriptManager = GameObject.FindGameObjectWithTag("Script Manager").GetComponent<ScriptManager>();
        answerText = answer.GetComponent<Text>();
    }

    private void Update()
    {
        // If bird jumps too high, dead
        if (player.transform.position.y > 8.0f && !scriptManager.isQuizzing)
        {
            GameOver();
        }
        // If immune timer is ticking
        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        } else
        {
            
            immune = false;
        }
    }
    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }


    public void Play()
    {
        audioManager.PlaySFX(audioManager.swooshing);
        score = 0;
        UpdateScoreText();
        displayHighScore();
        

        playButton.SetActive(false);
        quitButton.SetActive(false);

        answer.SetActive(false);
        gameOver.SetActive(false);
            

        

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Time.timeScale = 1f;
        player.enabled = true;
        //answer.enabled = false;
    }


    public void Pause()
    {

        Time.timeScale = 0f;
        player.enabled = false;
    }
    public static void ResetSpeed()
    {
        speed = 5f; // Resets the speed to the initial value
        //pipeCount = 0; // Resets the pipe counter to 0
    }

    public void GameOver()
    {
        if (!immune)
        {
            Pause();
            resetImmuneTimer();
            audioManager.PlaySFX(audioManager.hit);
            // Second chances with a quiz
            SceneManager.LoadScene("Quiz", LoadSceneMode.Additive);
        }   
    }

    public void IncreaseScore()
    {
        // Can only increase score when game is not paused
        if (!playButton.activeSelf)
        {
            audioManager.PlaySFX(audioManager.point);
            score++;
            scoreText.text = score.ToString();
        }
    }


    // Implementing high scores
    public int getHighScore()
    {
        return PlayerPrefs.GetInt(highScoreKey, 0);
    }
    public void updateHighScore()
    {
        int highscore = getHighScore();

        if (score > highscore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
            PlayerPrefs.Save();
        }
    }

    public void displayHighScore()
    {
        int highscore = getHighScore();
        highScoreText.text = "High Score: " + highscore.ToString();
    }

    // This runs after i get a quiz correct
    public void CorrectQuizContinue()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        immune = true;
        
    }

    // This runs after i get a quiz incorrect
    public void IncorrectQuizContinue()
    {
        updateHighScore();
        Pipes.ResetSpeed();
        audioManager.PlaySFX(audioManager.death);
        gameOver.SetActive(true);
        answer.SetActive(true);
        playButton.SetActive(true);
        quitButton.SetActive(true);
        answerText.text = "Answer: " + scriptManager.QuizAnswer;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void resetImmuneTimer()
    {
        immuneTimer = 0.6f;
    }
}




