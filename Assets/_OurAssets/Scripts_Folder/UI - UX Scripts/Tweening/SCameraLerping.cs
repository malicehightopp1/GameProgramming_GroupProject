using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCameraLerping : MonoBehaviour
{
    private Menu_Inputs mMenuInputs;
    [SerializeField] private bool mTriggered = false;
    private bool mReadyForInput = false;
    [SerializeField] private GameObject mTarget;

    [SerializeField] private Transform StartPOS;
    [SerializeField] private Transform EndPOS;
    [SerializeField] private GameObject mStartCanvas;
    public bool IsTriggered
    {
        get { return mTriggered; }
        set { mTriggered = value; }
    }
    private void Awake() 
    {
        mTriggered = false;
        mMenuInputs = new Menu_Inputs();
    }
    private void Start()
    {
        mMenuInputs.MainMenu.Start.performed += Lerp;
        mTriggered = false;
    }
    private void OnEnable()
    {
        mMenuInputs.Enable();
        StartCoroutine(WaitForInputRelease());
    }
    private void OnDisable()
    {
        mMenuInputs.Disable();
    }
    private IEnumerator WaitForInputRelease()
    {
        while (mMenuInputs.MainMenu.Start.IsPressed())
            yield return null;
        mReadyForInput = true; 
    }
    private void Lerp(InputAction.CallbackContext context)
    {
        if (!mReadyForInput) return;

        if(!mTriggered)
        {
            StartCoroutine(LerpCamera());
            mStartCanvas.SetActive(false);
            mTriggered = true;
        }
    }
    private IEnumerator LerpCamera()
    {
        Quaternion cameraOriginRotation = mTarget.transform.rotation;
        Quaternion camerNewRotation = StartPOS.transform.rotation;

        Vector3 start = mTarget.transform.position;
        Vector3 EndPOS = StartPOS.transform.position;

        float speed = 0;
        float duration = 1f;
        while (speed < 1)
        {
            speed += Time.unscaledDeltaTime / duration;
            mTarget.transform.rotation = Quaternion.Lerp(cameraOriginRotation, camerNewRotation, speed);
            mTarget.transform.position = Vector3.Lerp(start, EndPOS, speed);
            yield return null;
        }
    }
}
