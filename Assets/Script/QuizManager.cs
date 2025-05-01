using System.Runtime.CompilerServices;
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
    public string[] cheatAnswer = { "yaya" };
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
        bool correct = false;
        // All the possible answers are split with '/'
        string[] answers = answerString.Split('/');
        Debug.Log(answers);
        // This is for cheat answer
        foreach (string cheatans in cheatAnswer)
        {
            if (answer.text.TrimEnd().ToLower() == cheatans){
                correct = true;
            }
        }
        
        foreach (string actualAnswer in answers)
        {
            // Remove empty spaces at the end and change all capital to lowercase on user answer and answer string
            Debug.Log("One of the answer that gets checked: " + actualAnswer);
            if (answer.text.TrimEnd().ToLower() == actualAnswer.TrimEnd().ToLower()) // The user answered quiz correctly
            {
                Debug.Log("The user's answer matches this answer");

                correct = true;
                break;
            } else
            {
                Debug.Log("The user's answer doesn't match");
                continue;
            }
        }

        if (correct == true)
        {
            Debug.Log("We've checked through all the answers and found a match");
            scriptManager.QuizSuccess = true;
            scriptManager.isQuizzing = false;

            SceneManager.UnloadSceneAsync("Quiz");
            game.CorrectQuizContinue();
        }
        
        else if (correct == false)                     // The user answered quiz incorrectly
        {
            scriptManager.QuizSuccess = false;
            scriptManager.isQuizzing = false;
            scriptManager.QuizAnswer = answerString;

            SceneManager.UnloadSceneAsync("Quiz");
            game.IncorrectQuizContinue();
        }

        
    }
}
