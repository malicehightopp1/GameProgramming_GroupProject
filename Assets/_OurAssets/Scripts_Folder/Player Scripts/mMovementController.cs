using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class mMovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3 mMoveSpeed;
    [SerializeField] private float mMaxMoveSpeed;
    [SerializeField] private float mMoveAccel;
    [SerializeField] float TurnLerpRate;

    [SerializeField] private Transform mPlayerOrientation;

    private Vector2 mMovementInput;
    private Vector3 mMoveDir;

    private CharacterController mCharacterController;

    private void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        mMovementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        MovePlayer();
        UpdateTransform();
    }

    private void MovePlayer()
    {
        //Calculate Movement Direction
        mMoveDir = mPlayerOrientation.forward * mMovementInput.y + mPlayerOrientation.right * mMovementInput.x;

        if (mMoveDir.sqrMagnitude > 0)
        {
            mMoveSpeed += (mMoveDir * mMoveAccel * Time.deltaTime);
            mMoveSpeed = Vector3.ClampMagnitude(mMoveSpeed, mMaxMoveSpeed);
        }
        else
        {
            if (mMoveSpeed.sqrMagnitude > 0)
            {
                mMoveSpeed -= mMoveSpeed.normalized * mMoveAccel * Time.deltaTime;
                if (mMoveSpeed.sqrMagnitude < 0.1f)
                {
                    mMoveSpeed = Vector3.zero;
                }
            }
        }
    }
    private void UpdateTransform()
    {
        mCharacterController.Move((mMoveSpeed) * Time.deltaTime);

        if (mMoveSpeed.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(mMoveSpeed.normalized, Vector3.up), Time.deltaTime * TurnLerpRate);
        }
    }

}
