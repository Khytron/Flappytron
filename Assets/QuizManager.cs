using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class QuizManager : MonoBehaviour
{
    public float amount = 5.0f;
    public string[] questions;
    public string[] answers;
    public Text question;
    public InputField answer;
    public string answerString = " ";
    public ScriptManager scriptManager;
    public GameManager game;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        chooseRandomQuestion();
        scriptManager = GameObject.FindGameObjectWithTag("Script Manager").GetComponent<ScriptManager>();
        scriptManager.isQuizzing = true;
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
    }

    private void chooseRandomQuestion()
    {
        float indexf = Random.Range(0.0f, amount);
        int index = Mathf.FloorToInt(indexf);
        question.text = questions[index];
        answerString = answers[index].ToLower();

    }

    public void checkAnswerCorrect()
    {

        if (answer.text.ToLower() == answerString)
        {
            scriptManager.QuizSuccess = true;
            scriptManager.isQuizzing = false;
            Debug.Log("Quiz Manager thinks its correct");

            SceneManager.UnloadSceneAsync("Quiz");
            game.CorrectQuizContinue();
        } else
        {
            scriptManager.QuizSuccess = false;
            scriptManager.isQuizzing = false;
            Debug.Log("Quiz Manager thinks its incorrect");
            SceneManager.UnloadSceneAsync("Quiz");
            game.IncorrectQuizContinue();
        }

        
    }
}
