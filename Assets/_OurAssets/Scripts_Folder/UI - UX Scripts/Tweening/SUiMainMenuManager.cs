using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SUiMainMenuManager : MonoBehaviour
{
    [SerializeField] private Button mQuitButton;
    [SerializeField] private Button mStartButton;

    [SerializeField] private GameObject mSettingsPanel;
    [SerializeField] private GameObject mControlsPanel;
    private void Awake()
    {
        mStartButton.onClick.AddListener(() => StartButton());
        mQuitButton.onClick.AddListener(() => QuitButton());
    }
    private void Start()
    {
        mSettingsPanel.SetActive(false);
        mControlsPanel.SetActive(false);
    }
    private void StartButton()
    {
        Debug.Log("Starting the game");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }
    private void QuitButton()
    {
        Debug.Log("Quit button is working");
        Application.Quit();
    }
}
