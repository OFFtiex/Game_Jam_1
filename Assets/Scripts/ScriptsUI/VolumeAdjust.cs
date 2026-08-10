using UnityEngine;
using UnityEngine.Audio;
public class VolumeAdjust : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float slideValue){
        float volume = Mathf.Log10(Mathf.Clamp(slideValue, 0.0001f, 1f)) * 20f; // converting a linear value to decibels
        audioMixer.SetFloat("VolumeValue", volume);
    }
}
