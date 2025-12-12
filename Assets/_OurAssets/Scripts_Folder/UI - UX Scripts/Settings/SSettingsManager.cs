using UnityEngine;
using UnityEngine.UI;

public class SSettingsManager : MonoBehaviour
{
    [SerializeField] private Slider mSensitivtySlider;

    [SerializeField] private SCameraController mCameraController;

    void Start()
    {
        if(PlayerPrefs.HasKey("CameraSensitivty"))
        {
            float savedSens = PlayerPrefs.GetFloat("CameraSensitivty");
            mSensitivtySlider.value = savedSens;
            mCameraController.cameraSens = savedSens;
        }
        else
        {
            PlayerPrefs.SetFloat("CameraSensitivity",mSensitivtySlider.value);
             mCameraController.cameraSens = mSensitivtySlider.value;
        }
        mSensitivtySlider.onValueChanged.AddListener(SetSens);
    }
    public void SetSens(float value)
    {
        mCameraController.cameraSens = value;
        PlayerPrefs.SetFloat("CameraSensitivty", value);
    }
}
