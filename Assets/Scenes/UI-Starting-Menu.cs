using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method for Play Button
    public void PlayGame()
    {
        // Replace "GameScene" with the name of the scene you want to load
        SceneManager.LoadScene("GameScene");
    }

    // Method for Settings Button
    public void OpenSettings()
    {
        Debug.Log("Settings menu opened."); // Replace with actual settings implementation
    }

    // Method for Quit Button
    public void QuitGame()
    {
        Debug.Log("Game is quitting.");
        Application.Quit(); // Will only work in a built application
    }
}
