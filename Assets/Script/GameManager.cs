using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject leaderboardButton;
    public ScriptManager scriptManager;
    public Spawner[] spawner;
    public bool immune; // player immunity
    public float immuneTimer = 0.5f;
    public float countdownTimer = 3.0f;
    public GameObject gameOver;
    public GameObject answer;
    public countdownTime countdown;
    public Text answerText;
    public static int score;

    AudioManager audioManager;

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
        countdown = GameObject.FindGameObjectWithTag("Countdown").GetComponent<countdownTime>();
        Debug.Log("Initialized countdown objects");
        Debug.Log(scriptManager.countdownText);
    }

    private void Update()
    {
        // If bird jumps too high, dead
        if (player.transform.position.y > 8.0f && !scriptManager.isQuizzing)
        {
            GameOver();
        }
        // Immune timer is ticking
        if (immuneTimer > 0)
        {
            immuneTimer -= Time.deltaTime;
        } else
        {
            
            immune = false;
        }

        //Debug.Log(countdownScript.countdownText);

        // -- Countdown Logic --
        if (scriptManager.isDoingCountdown)
        {
            // Debug.Log("Countdown timer: " + countdown.countdown);
            countdown.decreaseCountdown();
            
            // Debug.Log("Countdown digit: " + scriptManager.countdownNumber);

            // Updating countdown
            if (Mathf.FloorToInt(countdown.countdown) != scriptManager.countdownNumber)
            {
                scriptManager.UpdateCountdown();
                //Debug.Log("Updating countdown");
            }

            // Exiting countdown
            else if (countdown.countdown <= 1.0f)
            {   
                //Debug.Log("Countdown finished");

                SceneManager.UnloadSceneAsync("Countdown");
                
                scriptManager.ResetCountdown(); // Resetting countdown variables
                countdown.countdown = 3.99f;
                Unpause();
                player.GetComponent<Player>().teleportMiddle(); // Teleports player to the middle
                immune = true; // Immune for a split second
                pauseButton.SetActive(true); // Activate pause button
            }

        }

        // IF we are quizzing
        if (scriptManager.isQuizzing)
        { 
            
            QuizManager quizManager = FindObjectOfType<QuizManager>();
            // If user clicks enter, then 
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // click the submit button
                quizManager.checkAnswerCorrect();
            }
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
        
        // Buttons
        playButton.SetActive(false);
        quitButton.SetActive(false);
        leaderboardButton.SetActive(false);

        pauseButton.SetActive(true);

        answer.SetActive(false);
        gameOver.SetActive(false);

        // Teleport player to middle
        player.teleportMiddle();
        

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        Unpause();
 
    }


    public void Pause()
    {

        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        player.enabled = true;
    }
    public static void ResetSpeed()
    {
        speed = 5f; // Resets the speed to the initial value
        //pipeCount = 0; // Resets the pipe counter to 0
    }

    public void GameOver() // This runs after bird splat something
    {
        if (!immune)
        {
            Pause();
            resetImmuneTimer();
            audioManager.PlaySFX(audioManager.hit);
            // Deactivate pause and resume button
            pauseButton.SetActive(false);
            resumeButton.SetActive(false);
            // Second chances with a quiz
            SceneManager.LoadScene("Quiz", LoadSceneMode.Additive);
            // Dont display score
            scoreText.text = "";

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
        // --- COUNTDOWN STARTING ---
        scriptManager.isDoingCountdown = true;
        SceneManager.LoadScene("Countdown", LoadSceneMode.Additive);
        
       
                
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
        leaderboardButton.SetActive(true);
        answerText.text = "Answer: " + scriptManager.QuizAnswer.Split('/')[0];
        UpdateScoreText();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void resetImmuneTimer()
    {
        immuneTimer = 0.6f;
    }

    public void pauseButtonClicked()
    {
        
        Pause();
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        // Fix bug where clicking pause button makes the bird jump 
        player.canJump = false;


    }

    public void resumeButtonClicked()
    {
        Unpause();
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        player.canJump = true;

    }

    public void leaderboardButtonClicked()
    {
        // Load the leaderboard scene
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Additive);
    }

}




