using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void UpdateMaster(float value)
    {
        audioMixer.SetFloat("master", value);
    }

    public void UpdateSFX(float value)
    {
        audioMixer.SetFloat("sfx", value);
    }

    public void UpdateMusic(float value)
    {
        audioMixer.SetFloat("music", value);
    }
}
