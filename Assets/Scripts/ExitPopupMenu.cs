using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ExitPopup;    
    public Button yesButton;        
    public Button noButton;         

    private bool isPaused = false;
        private CursorLockMode previousLockMode; // Stores the cursor state before pausing

    private void Start()
    {
            
        if (ExitPopup != null)
            ExitPopup.SetActive(false);

            
        if (yesButton != null)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(GoToMainMenu);
        }

        if (noButton != null)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(ClosePopup);
        }
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!isPaused)
                PauseGame();
            else
                UnpauseGame();
        }
        Debug.Log($"Cursor: lockState={Cursor.lockState}, visible={Cursor.visible}");
    }

    private void PauseGame()
    {
        isPaused = true;
        
            
        previousLockMode = Cursor.lockState;
        
            
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
            
        Time.timeScale = 0f;
        
        
        if (ExitPopup != null)
            ExitPopup.SetActive(true);
    }

    private void UnpauseGame()
    {
        isPaused = false;
        
            
        Cursor.lockState = previousLockMode;
        Cursor.visible = (previousLockMode != CursorLockMode.Locked);
        
            
        Time.timeScale = 1f;
        
            
        if (ExitPopup != null)
            ExitPopup.SetActive(false);
    }

    private void GoToMainMenu()
    {
            
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("MainMenu");
    }

    private void ClosePopup()
    {
        UnpauseGame();
    }
}