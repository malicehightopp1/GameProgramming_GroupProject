using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SPauseManager : MonoBehaviour
{
    [Header("Input Actions")]
    private Menu_Inputs mMenuInputs;

    [Header("UI")]
    private bool IsPaused = false;
    [SerializeField] private GameObject mPauseBook;

    [SerializeField] private Button mPauseQuitButton;
    [SerializeField] private Button mPauseMainMenuButton;
    [SerializeField] private GameObject mSettingsMenu;
    [SerializeField] private GameObject mControlsMenu;

    [Header("Tweening")]
    [SerializeField] private GameObject mTarget; //camera
    [SerializeField] private Transform mEndPOS;
    [SerializeField] private Transform mStartPOS;
    private Quaternion prePauseRotation; //for saving camera position before pausing

    [Header("References")]
    [SerializeField] private SCameraController mCameraController;

    private void Awake()
    {
        mMenuInputs = new Menu_Inputs(); //creating new input system

        mMenuInputs.InGame.Pause.performed += PauseGame; //on esc pressed pause the game
        mPauseQuitButton.onClick.AddListener(() => Quit());
        mPauseMainMenuButton.onClick.AddListener(() => MainMenu());
    }
    private void Start()
    {
        mSettingsMenu.SetActive(false);
        mControlsMenu.SetActive(false);
        mPauseBook.SetActive(false); //making sure pause menu is off on start
        IsPaused = false; //making sure game is not paused on start
    }
    #region Input For Pause
    private void PauseGame(InputAction.CallbackContext context) //check if game is paused or not then going to either pause or resume the game
    {
        if(IsPaused) //if game is paused unpause it
        {
            StartCoroutine(SetCameraLerpBack());
        }
        else //if game is not paused pause it
        {
            StartCoroutine(LerpCamera());
        }
    }
    private void OnEnable()
    {
        mMenuInputs.Enable();
    }
    private void OnDisable()
    {
        mMenuInputs.Disable();
    }
    #endregion
    #region Pause and resume
    private void Pause() //pausing the game
    {
        Debug.Log("Game paused");
        IsPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        mPauseBook.SetActive(true);
    }
    private void Resume() //resumeing the game
    {
        Debug.Log("Game resumed");
        IsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        mPauseBook.SetActive(false);
    }
    #endregion
    #region Button Functions
    private void MainMenu() //function to going to mainmenu
    {
        Debug.Log("Loading main menu");
        SceneManager.LoadScene(0);
    }
    private void Quit() //function for quitting game
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }
    #endregion
    #region Camera Lerping and Tweening
    private IEnumerator LerpCamera()
    {
        mCameraController.lockRotation = true; //locking camera movement
        mCameraController.cameraSens = 0f; //locking movement of camera

        yield return null; //wait a frame so when it lerps its not jittery or when player moves doesnt put camera in wrong position

        Transform cam = mCameraController.transform;
        prePauseRotation = cam.rotation; //saving the rotation before pausing   

        //variables for lerping
        Quaternion newrotation = mStartPOS.rotation;//rotation 
        Quaternion oldrotation = cam.rotation;//rotation 

        Vector3 startPos = mStartPOS.position;//tranform
        Vector3 endPos = mEndPOS.position;//tranform

        float speed = 0;
        float duration = 0.5f;

        Pause(); //pausing game - this NEEDS to be here to avoid jittery camera
        while (speed < 1) //smooth transition for lerping
        {
            speed += Time.unscaledDeltaTime / duration;
            cam.rotation = Quaternion.Lerp(oldrotation, newrotation, speed);
            cam.transform.position = Vector3.Lerp(endPos, startPos, speed);
            yield return null;
        }

        //syncing camera rotation with SCameraController script
        Vector3 e = cam.rotation.eulerAngles;
        mCameraController.SetRotation(e.x, e.y);
    }
    private IEnumerator SetCameraLerpBack()
    {
        mCameraController.lockRotation = true; //locking camera movement
        mCameraController.cameraSens = 0f; //locking the movement of camera


        //setting up vaiables for lerping
        Transform cam = mCameraController.transform; 

        Quaternion newrotation = mStartPOS.rotation;//rotation 
        Quaternion oldrotation = prePauseRotation;//rotation 

        Vector3 startPos = mStartPOS.position;//tranform
        Vector3 endPos = mEndPOS.position;//tranform

        float speed = 0;
        float duration = 0.5f;
        while (speed < 1) //to lerp over a course of time
        {
            speed += Time.unscaledDeltaTime / duration;
            cam.transform.rotation = Quaternion.Lerp(newrotation, oldrotation, speed);
            cam.transform.position = Vector3.Lerp(startPos, endPos, speed);
            yield return null;
        }

        //setting the rotation to sync with the SCameraController script
        Vector3 e = cam.rotation.eulerAngles;
        mCameraController.SetRotation(e.x, e.y);

        //setting the camera back to normal
        mCameraController.cameraSens = 2f;
        mCameraController.lockRotation = false;
        Resume(); //starting resume function to unpause the game
    }
    #endregion
}
