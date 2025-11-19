using UnityEngine;
using UnityEngine.InputSystem;

public class SCameraController : MonoBehaviour
{
    [Header("Camera Variables")]
    [SerializeField] private float mSensitivity; //assess for ui and animations to pause camera movement
    private float xRotation;
    private float yRotation;
    [SerializeField] float mPitchMin = -89f;
    [SerializeField] float mPitchMax = 89f;
    private bool LockRotation;

    [Header("References")]
    [SerializeField] private Transform mPlayerOrientation;
    private Vector2 mLookInput;
    public float cameraSens //for locking in animation
    { 
        get { return mSensitivity; }
        set { mSensitivity = value; }
    }
    public bool lockRotation //for locking in animation
    {
        get { return LockRotation; }
        set { LockRotation = value; }
    }
    private void Start()
    {
        LockMouse();
    }
    private void Update() //updating the camera rotation based on mouse input
    {
        if (LockRotation) return; //if camera is locked in animation dont update rotation

        //Setting up the Mouse Input
        yRotation += mLookInput.x;
        xRotation -= mLookInput.y;

        xRotation = Mathf.Clamp(xRotation, mPitchMin, mPitchMax);

        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        mPlayerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    private void LockMouse()//locking the cursor to the center of the screen
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void SetLookInput(InputAction.CallbackContext context) //mouse inputs for moving camera
    {
        mLookInput = context.ReadValue<Vector2>() * Time.deltaTime * mSensitivity;
    }
    public void SetRotation(float newX, float newY) //this is to sync the position of the camera with the tweening for smooth animations
    {
        xRotation = newX;
        yRotation = newY;

        this.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        mPlayerOrientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
