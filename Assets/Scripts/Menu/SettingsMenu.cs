using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void Start()
    {
        List<string> options = new List<string>();
    }

    [Tooltip("Gère le volume sonore")]
    public void SetVolume(float Volume)
    {
       audioMixer.SetFloat("Volume", Volume);
    }

    [Tooltip("Met le jeu en plein écran")]
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////
}
