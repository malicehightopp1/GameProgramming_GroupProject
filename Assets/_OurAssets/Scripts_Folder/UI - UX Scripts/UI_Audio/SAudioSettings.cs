using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SAudioSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mAudioMixer;
    [SerializeField] private Slider mAudioSlider;
    [SerializeField] private Slider mSFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolume") || PlayerPrefs.HasKey("SFX"))
        {
            LoadVolume();
            LoadSFX();
        }
        else
        {
            SetAudioMixer(); //set the audio mixer volume at start based on the slider value
            SetSFXAudio();
        }
    }
    public void SetAudioMixer() //setting the audio mixer volume based on the slider value
    {
        float sliderValue = mAudioSlider.value;
        mAudioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20); //convert linear 0-1 slider value to logarithmic decibel value
        PlayerPrefs.SetFloat("musicVolume", sliderValue);
    }
    private void LoadVolume()
    {
        mAudioSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetAudioMixer();
    }
    public void SetSFXAudio()
    {
        float SFXvalue = mSFXSlider.value;
        mAudioMixer.SetFloat("SFX", Mathf.Log10(SFXvalue) * 20);
        PlayerPrefs.SetFloat("SFX", SFXvalue);
    }
    private void LoadSFX()
    {
        mSFXSlider.value = PlayerPrefs.GetFloat("SFX");
        SetSFXAudio();
    }
}
