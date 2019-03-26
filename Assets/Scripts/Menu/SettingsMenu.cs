using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    #region Variables
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    //set up an array for resolutions
    Resolution[] resolutions;
    #endregion
    #region Resolution
    private void Start()
    {
        resolutions = Screen.resolutions;
        //Clear out options in resolutions dropdown
        resolutionDropdown.ClearOptions();
        //create a list of strings which are the options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        //create formatted list and 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        //add it to the dropdown options
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    #endregion
    #region Volume Slider


    //attach the volume slider to the mixer volume
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    #endregion
    #region Quality Settings
    //attach quality settings to dropdown options
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion
    #region FullScreen
    public void SetFullScreen(bool isFulleScreen)
    {
        Screen.fullScreen = isFulleScreen;
    }
    #endregion
}
