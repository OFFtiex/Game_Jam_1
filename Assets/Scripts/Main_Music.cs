using UnityEngine;

public class Main_Music : MonoBehaviour
{
    private AudioSource audio_source;
    public AudioClip Main_Music_Clip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_source = GetComponent<AudioSource>();

        PlaySFX(Main_Music_Clip);
    }

    


    public void PlaySFX(AudioClip audioClip)
    {
        audio_source.clip = audioClip;
        audio_source.Play();
    }
}
