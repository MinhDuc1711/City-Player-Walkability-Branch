using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
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
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Dropdown graphicsDropdown;
    public Slider volumeSlider;
    public AudioMixer audioMixer; // Reference to Audio Mixer for volume control

    private Resolution[] resolutions;

    void Start()
    {
        // Ensure only the Main Menu is visible at the start
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);

        // Initialize resolution options
        InitializeResolutions();

        // Load saved settings
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

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();

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
    }

    // 📌 Handle Fullscreen Toggle
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    // 📌 Handle Graphics Quality
    public void SetGraphicsQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
    }

    // 📌 Handle Volume Control
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); // Convert to logarithmic scale for better control
        PlayerPrefs.SetFloat("Volume", volume);
    }

    // 📌 Load saved settings when the game starts
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
            fullscreenToggle.isOn = isFullscreen;
            Screen.fullScreen = isFullscreen;
        }

        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int qualityIndex = PlayerPrefs.GetInt("GraphicsQuality");
            graphicsDropdown.value = qualityIndex;
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        if (PlayerPrefs.HasKey("Volume"))
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = volume;
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        }
    }
}
