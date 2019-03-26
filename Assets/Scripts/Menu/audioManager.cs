using UnityEngine.Audio;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    #region Variables
    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;
    #endregion
    #region Button Sounds
    //Set Sound on hover over buttons
    public void onHover()
    {
        source.PlayOneShot(hover);
    }

    //Set sound on click buttons
    public void onClick()
    {
        source.PlayOneShot(click);
    }
    #endregion
}

