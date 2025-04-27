using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public bool QuizSuccess = false;
    public bool isQuizzing = false;
    public GameManager game;
    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("Game").GetComponent<GameManager>();
    }

}
