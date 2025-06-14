using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionOption : MonoBehaviour
{
    FullScreenMode screenMode;
    readonly bool is16v9 = true;
    readonly bool hasHz;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] Toggle fullScreenToggle;
    List<Resolution> resolutions = new List<Resolution>();

    public int ResolutionIndex
    {
        get => PlayerPrefs.GetInt("ResolutionIndex", 0);
        set => PlayerPrefs.SetInt("ResolutionIndex", value);
    }
    public bool IsFullScreen
    {
        get => PlayerPrefs.GetInt("IsFullScreen", 1) == 1;
        set => PlayerPrefs.SetInt("IsFullScreen", value ? 1 : 0);
    }

    void Start()
    {
        resolutionDropdown.onValueChanged.AddListener(DropboxOptionChange);
        fullScreenToggle.onValueChanged.AddListener(FullScreenButton);
        Invoke("SetResolution", 0.001f);
    }

    public void SetResolution()
    {
        Debug.Log("SetResolution");
        resolutionDropdown.ClearOptions();
        resolutions.AddRange(Screen.resolutions);

        resolutions.Reverse();

        // only 16:9
        if (is16v9)
        {
            float tolerance = 0.01f;
            resolutions = resolutions.FindAll(x => Mathf.Abs((float)x.width / x.height - 16f / 9) < tolerance);
        }
        // Hz Visibility
        if (!hasHz && resolutions.Count > 0)
        {
            List<Resolution> tempResolutions = new List<Resolution>();
            int currentWidth = resolutions[0].width;
            int currentHeight = resolutions[0].height;

            tempResolutions.Add(resolutions[0]);
            foreach (Resolution resolution in resolutions)
            {
                if (currentWidth != resolution.width || currentHeight != resolution.height)
                {
                    tempResolutions.Add(resolution);
                    currentWidth = resolution.width;
                    currentHeight = resolution.height;
                }
            }
            resolutions = tempResolutions;
        }

        List<string> options = new List<string>();
        foreach (Resolution resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            if (hasHz)
            {
                option += $" {resolution.refreshRateRatio}Hz";
            }
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = ResolutionIndex;

        fullScreenToggle.isOn = IsFullScreen;
    }

    public void DropboxOptionChange(int resolutionIndex)
    {
        ResolutionIndex = resolutionIndex;
    }
    public void FullScreenButton(bool isFull)
    {
        IsFullScreen = isFull;
        fullScreenToggle.isOn = IsFullScreen;
        
    }
    public void Apply()
    {
        screenMode = IsFullScreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(resolutions[ResolutionIndex].width, resolutions[ResolutionIndex].height, screenMode);
    }
}
