using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SMovementController : MonoBehaviour
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

    [Header("Gravity")]
    [SerializeField] private float mMaxFallSpeed;
    [SerializeField] float mAirCheckRadius = 0.5f;
    [SerializeField] LayerMask mAirCheckLayerMask = 1;

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
        UpdateGravity();
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

        //if (mMoveSpeed.sqrMagnitude > 0)
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(mMoveSpeed.normalized, Vector3.up), Time.deltaTime * TurnLerpRate);
        //}
    }

    private void UpdateGravity()
    {
        if (mCharacterController.isGrounded)
        {
            mMoveSpeed.y = -1f;
            return;
        }

        //Free Falling
        if (mMoveSpeed.y > -mMaxFallSpeed)
        {
            mMoveSpeed.y += Physics.gravity.y * Time.deltaTime;
        }
    }

    bool IsInAir()
    {
        if (mCharacterController.isGrounded)
        {
            return false;
        }

        Collider[] airCheckColliders = Physics.OverlapSphere(transform.position, mAirCheckRadius, mAirCheckLayerMask);

        foreach (Collider collider in airCheckColliders)
        {
            if (collider.gameObject != gameObject)
            {
                return false;
            }
        }
        return true;
    }

}
