using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel; 
    public GameObject settingsPanel; 
   
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false); 
        settingsPanel.SetActive(true);  
    }

    
    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);  
        settingsPanel.SetActive(false); 
    }
}
