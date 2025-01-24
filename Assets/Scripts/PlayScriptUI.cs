using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Main Street");
    }

    public void ssttings()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
