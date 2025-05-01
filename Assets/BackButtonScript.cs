using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour
{
    private Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CloseLeaderboardScene);
    }

    public void CloseLeaderboardScene() // When the user clicks the back button on the leaderboard scene
    {
        SceneManager.UnloadSceneAsync("Leaderboard");
    }
}
