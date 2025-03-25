using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIManager_CPW : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;   
    public GameObject settingsPanel;  
    public GameObject PanelMain; // Reference to PanelMain
 

    [Header("Buttons")]
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;
    public GameObject BackBtn;


    [Header("Settings UI Elements")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public  TMP_Dropdown Graphics;
    public Slider volumeSlider;
    public AudioMixer audioMixer; 

    [Header("Graphics Settings (URP)")]
    public UniversalRenderPipelineAsset lowQualityURP;
    public UniversalRenderPipelineAsset mediumQualityURP;
    public UniversalRenderPipelineAsset highQualityURP;

    private Resolution[] resolutions;



    void Start()
    {
        
        if (mainMenuPanel == null || settingsPanel == null)
        {
            Debug.LogError("UIManager_CPW: Missing UI panel references. Assign them in the Inspector!");
            return;
        }
        
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    
        InitializeResolutions();

    }

    public void KeepPanelMainActive()
{
    Debug.Log("KeepPanelMainActive() called!"); 

    // Force PanelMain to stay active
    if (PanelMain != null && !PanelMain.activeSelf)
    {
        PanelMain.SetActive(true);
        Debug.Log("PanelMain was disabled, reactivating...");
    }
    // Deselect dropdown to prevent UI conflicts
    EventSystem.current.SetSelectedGameObject(null);
}

    public void ShowOptions()
    {
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        settingsPanel.SetActive(true);
        BackBtn.SetActive(true);
        
         if (BackBtn != null) BackBtn.SetActive(true); 
    }

    public void ShowMainMenu()
    {
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
        quitButton.SetActive(true);
        BackBtn.SetActive(false);
    }

    void InitializeResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionString = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(resolutionString);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
         BackBtn.SetActive(true);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            Debug.LogError("UIManager_CPW: Invalid resolution index!");
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        BackBtn.SetActive(true);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20); 
            PlayerPrefs.SetFloat("Volume", volume);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("UIManager_CPW: Audio Mixer not assigned!");
        }
    }
}