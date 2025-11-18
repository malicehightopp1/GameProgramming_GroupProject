using UnityEngine;
using UnityEngine.InputSystem;

public class SCameraController : MonoBehaviour
{
    [SerializeField] private float mSensitivity;

    [SerializeField] private Transform mPlayerOrientation;

    private float xRotation;
    private float yRotation;

    private Vector2 mLookInput;

    [SerializeField] float mPitchMin = -89f;
    [SerializeField] float mPitchMax = 89f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Setting up the Mouse Input
        yRotation += mLookInput.x;
        xRotation -= mLookInput.y;

        xRotation = Mathf.Clamp(xRotation, mPitchMin, mPitchMax);

        //Rotate Cam and Orientation
        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        mPlayerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void SetLookInput(InputAction.CallbackContext context)
    {
        mLookInput = context.ReadValue<Vector2>() * Time.deltaTime * mSensitivity;
    }

    

}
