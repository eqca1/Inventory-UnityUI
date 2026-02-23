using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Awake() { DontDestroyOnLoad(this.gameObject); }
    public void StartGame()
    {

        SceneManager.LoadScene("Game1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}