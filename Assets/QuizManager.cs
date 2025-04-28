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


    private void chooseRandomQuestion()
    {
        float indexf = Random.Range(0.0f, amount);
        int index = Mathf.FloorToInt(indexf);
        question.text = questions[index];
        // if the question is the same as before
        if (answers[index].ToLower() == answerString)
        {
            Debug.Log("Repeated a question");
            chooseRandomQuestion(); // repeat
        } else
        {
            answerString = answers[index].ToLower();
        }
            

    }

    public void checkAnswerCorrect()
    {

        if (answer.text.ToLower() == answerString) // The user answered quiz correctly
        {
            scriptManager.QuizSuccess = true;
            scriptManager.isQuizzing = false;

            SceneManager.UnloadSceneAsync("Quiz");
            game.CorrectQuizContinue();
        }
        else                                       // The user answered quiz incorrectly
        {
            scriptManager.QuizSuccess = false;
            scriptManager.isQuizzing = false;
            scriptManager.QuizAnswer = answerString;

            SceneManager.UnloadSceneAsync("Quiz");
            game.IncorrectQuizContinue();
        }

        
    }
}
