using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SCameraLerping : MonoBehaviour
{
    private Menu_Inputs mMenuInputs;
    private bool mTriggered = false;
    private GameObject mTarget;

    [SerializeField] private Transform StartPOS;
    [SerializeField] private Transform EndPOS;
    [SerializeField] private GameObject mStartCanvas;
    private void Awake()
    {
        mMenuInputs = new Menu_Inputs();
        mMenuInputs.MainMenu.Start.performed += Lerp;
    }
    private void Start()
    {
        mTriggered = false;
        mTarget = this.gameObject;
    }
    private void OnEnable()
    {
        mMenuInputs.Enable();
    }
    private void OnDisable()
    {
        mMenuInputs.Disable();
    }
    private void Lerp(InputAction.CallbackContext context)
    {
        if(mTriggered == false)
        {
            mTriggered = true;
            StartCoroutine(LerpCamera());
            mStartCanvas.SetActive(false);
        }
    }
    private IEnumerator LerpCamera()
    {
        Quaternion cameraOriginRotation = Quaternion.Euler(0, 0, 0);
        Quaternion camerNewRotation = Quaternion.Euler(61.553f, 0, 0);

        float speed = 0;
        float duration = 1f;
        while (speed < 1)
        {
            speed += Time.deltaTime / duration;
            mTarget.transform.rotation = Quaternion.Lerp(cameraOriginRotation, camerNewRotation, speed);
            mTarget.transform.position = Vector3.Lerp(EndPOS.position, StartPOS.position, speed);
            yield return null;
        }
    }
}
