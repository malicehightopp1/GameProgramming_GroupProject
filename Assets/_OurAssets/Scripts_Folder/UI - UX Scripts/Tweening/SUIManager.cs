using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SUIManager : MonoBehaviour
{
    [Header("UI Buttons References")]
    private Button[] mTweeningButtons;
    [SerializeField] private Button mQuitButton;
    [SerializeField] private Button mStartButton;

    //[SerializeField] private GameObject mMainCanvas;
    [SerializeField] private GameObject mSettingsCanvas;
    [SerializeField] private GameObject mControlsCanvas;
    private void Awake()
    {
        mStartButton.onClick.AddListener(() => StartButton());
        mQuitButton.onClick.AddListener(() => QuitButton());
    }
    private void Start()
    {
        mTweeningButtons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        mSettingsCanvas.SetActive(false);
        mControlsCanvas.SetActive(false);
        SetupButtons();
    }
    #region ButtonTweening
    private void SetupButtons() //setting up and adding listeners to all buttons in scene
    {
        foreach (Button buttons in mTweeningButtons)
        {
            buttons.onClick.AddListener(() => StartCoroutine(TweeningButton(buttons, new Vector3(0.9f, 0.9f, 0.9f), 0.1f)));
        } 
    }
    private IEnumerator TweeningButton(Button button, Vector3 targetScale, float duration) //tweening the button using local scale
    {
        Vector3 startscale = button.transform.localScale;
        float elapsed = 0;
        while (elapsed < duration) //lerping local scale to give smooth feeling
        {
            button.interactable = false;
            elapsed += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(startscale, targetScale, elapsed / duration);
            yield return null;
        }
        elapsed = 0;
        while (elapsed < duration) //lerping local scale to give smooth feeling
        {
            elapsed += Time.deltaTime;
            button.transform.localScale = Vector3.Lerp(targetScale, startscale, elapsed / duration);
            yield return null;
        }
        button.interactable = true;
    }
    #endregion
    private void StartButton()
    {
        Debug.Log("Starting the game");
        //TODO: Setup next scene
    }
    private void QuitButton()
    {
        Debug.Log("Quit button is working");
        Application.Quit();
    }
}
