using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{

    public AudioMixer mixer;

    public void OnMusicVolumeChanged(float val)
    {
        mixer.SetFloat("Music", val);
    }

    public void OnSFXVolumeChanged(float val)
    {
        mixer.SetFloat("SFX", val);
    }

    public void OnMasterChanged(float val)
    {
        mixer.SetFloat("Master", val);
    }
}
