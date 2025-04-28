using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public bool QuizSuccess = false;
    public bool isQuizzing = false;
    public string QuizAnswer;
    public GameManager game;
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
    }

}
