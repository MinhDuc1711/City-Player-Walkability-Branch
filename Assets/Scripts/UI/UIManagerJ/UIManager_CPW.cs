using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;

public class UIManager_CPW : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;   // Reference to Main Menu Panel
    public GameObject settingsPanel;   // Reference to Settings Panel

    [Header("Buttons")]
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    [Header("Settings UI Elements")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public TMP_Dropdown graphicsDropdown;
    public Slider volumeSlider;
    public AudioMixer audioMixer; // Reference to Audio Mixer for volume control

    [Header("Graphics Settings (URP)")]
    public UniversalRenderPipelineAsset lowQualityURP;
    public UniversalRenderPipelineAsset mediumQualityURP;
    public UniversalRenderPipelineAsset highQualityURP;

    private Resolution[] resolutions;

    void Start()
    {
        // ✅ Ensure Panels are Correctly Managed
        if (mainMenuPanel == null || settingsPanel == null)
        {
            Debug.LogError("UIManager_CPW: Missing UI panel references. Assign them in the Inspector!");
            return;
        }

        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        // ✅ Initialize Resolution Options
        InitializeResolutions();

        // ✅ Load User Preferences
        LoadSettings();
    }

    public void ShowOptions()
    {
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(true);
        settingsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        settingsPanel.SetActive(false);
    }

    // 📌 Handle Resolution Settings
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
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }

    // 📌 Handle Fullscreen Toggle
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    // 📌 Fix Graphics Quality (Now Works and Saves)
    public void SetQuality(int qualityIndex)
{
    // Apply Unity's Built-in Quality Setting
    QualitySettings.SetQualityLevel(qualityIndex);

    // ✅ Apply the Correct URP Render Pipeline Asset
    switch (qualityIndex)
    {
        case 0: // Low Quality
            GraphicsSettings.defaultRenderPipeline = lowQualityURP;
            Debug.Log("Graphics set to LOW");
            break;

        case 1: // Medium Quality
            GraphicsSettings.defaultRenderPipeline = mediumQualityURP;
            Debug.Log("Graphics set to MEDIUM");
            break;

        case 2: // High Quality
            GraphicsSettings.defaultRenderPipeline = highQualityURP;
            Debug.Log("Graphics set to HIGH");
            break;

        default:
            Debug.LogError("Invalid graphics quality index!");
            return;
    }

    // ✅ Save Setting for Future Sessions
    PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    PlayerPrefs.Save();
}


    // 📌 Fix Volume Control (Now Prevents Log(0) Error)
    public void SetVolume(float volume)
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20); // Prevent log(0) issue
            PlayerPrefs.SetFloat("Volume", volume);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("UIManager_CPW: Audio Mixer not assigned!");
        }
    }

    // 📌 Load User Preferences
    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("ResolutionIndex"))
        {
            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex");
            resolutionDropdown.value = resolutionIndex;
        }

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
            fullscreenToggle.isOn = isFullscreen;  // Sync toggle state
            Screen.fullScreen = isFullscreen;  // Apply fullscreen mode
        }

        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int qualityIndex = PlayerPrefs.GetInt("GraphicsQuality");
            graphicsDropdown.value = qualityIndex;
            SetQuality(qualityIndex); // ✅ Ensures URP Assets are Applied
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = volume;
            SetVolume(volume);
        }
    }
}
