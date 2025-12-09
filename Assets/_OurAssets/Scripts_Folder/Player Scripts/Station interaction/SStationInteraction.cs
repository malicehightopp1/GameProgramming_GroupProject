using System.Collections;
using UnityEngine;
public class SStationInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform mCameraTarget;
    [SerializeField] private Transform mEndPos;
    [SerializeField] private Transform mStartPos;
    private float mOriginalSens;

    private Quaternion prePauseRotation; //for saving camera position before pausing

    private bool mIsInteracted = false;
    void IInteractable.OnInteract(SInteraction playerInteract)
    {
        if(!mIsInteracted)
        {
            StartCoroutine(OnInteraction(playerInteract));
        }
        else
        {
            StartCoroutine(OnInteractionEnd(playerInteract));
        }
    }
    private IEnumerator OnInteraction(SInteraction playerInteract)
    {
        playerInteract.mStartedInteraction = true;
        mOriginalSens = playerInteract.mCameraController.cameraSens;

        playerInteract.mMovementController.InputBlocked = true;
        playerInteract.mCameraController.lockRotation = true;
        Debug.Log($"{mOriginalSens}");
        playerInteract.mCameraController.cameraSens = 0f;

        yield return null;

        Transform cam = playerInteract.mCameraController.transform;
        prePauseRotation = cam.rotation; //saving the rotation before pausing   

        Quaternion endRot = mEndPos.rotation;
        Quaternion startRot = cam.rotation;

        Vector3 startPos = mStartPos.position;
        Vector3 endPos = mEndPos.position;

        float t = 0;
        float duration = 1f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            mCameraTarget.position = Vector3.Lerp(startPos, endPos, t);
            mCameraTarget.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        mIsInteracted = true;
        playerInteract.mStartedInteraction = false;

        Vector3 e = mCameraTarget.rotation.eulerAngles;
        playerInteract.mCameraController.SetRotation(e.x, e.y);
    }
    private IEnumerator OnInteractionEnd(SInteraction playerInteract)
    {
        playerInteract.mStartedInteraction = true;
        Transform cam = playerInteract.mCameraController.transform;

        Quaternion endRot = cam.rotation;
        Quaternion startRot = mEndPos.rotation;

        Vector3 startPos = mEndPos.position;
        Vector3 endPos = mStartPos.position;

        float t = 0;
        float duration = 1f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            mCameraTarget.position = Vector3.Lerp(startPos, endPos, t);
            mCameraTarget.rotation = Quaternion.Slerp(startRot, endRot, t);
            yield return null;
        }
        playerInteract.mCameraController.cameraSens = mOriginalSens;
        playerInteract.mCameraController.lockRotation = false;

        playerInteract.mMovementController.InputBlocked = false;
        playerInteract.mStartedInteraction = false;
        mIsInteracted = false;

        Vector3 e = mCameraTarget.rotation.eulerAngles;
        playerInteract.mCameraController.SetRotation(e.x, e.y);
    }
}
