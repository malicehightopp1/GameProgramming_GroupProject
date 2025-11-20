using UnityEngine;

public class SAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource mSFXAudioSource;
    [SerializeField] private AudioClip mButtonClick;
    public void PlayButtonClickSound() //method to play button click sound
    {
        mSFXAudioSource.PlayOneShot(mButtonClick);
    }
}