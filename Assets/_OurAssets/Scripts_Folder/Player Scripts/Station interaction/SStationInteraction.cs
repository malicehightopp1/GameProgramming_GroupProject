using System.Collections;
using UnityEngine;
public class SStationInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject mTarget;
    [SerializeField] private Transform mStartPOS;

    [SerializeField] private SInteraction mInteraction;
    [SerializeField] private SCameraController mCameraController;
    [SerializeField] private SMovementController mMovementController;
    public void OnInteract(SInteraction playerInteraction)
    {
        StartInteraction();
    }
    void StartInteraction()
    {
        if(mInteraction.mStartedInteraction == false)
        {
            StartCoroutine(StationStartInteraction());
        }
        else
        {
            Debug.Log("Already Interacting with Station");
        }
    }
    private IEnumerator StationStartInteraction()
    {
        mMovementController.CanMove = false; //locking player movement
        mCameraController.lockRotation = true; //locking camera movement
        mCameraController.cameraSens = 0f; //locking movement of camera

        mInteraction.mStartedInteraction = true;

        Quaternion cameraOriginRotation = mTarget.transform.rotation;
        Quaternion camerNewRotation = mStartPOS.transform.rotation;

        Vector3 start = mTarget.transform.position;
        Vector3 EndPOS = mStartPOS.transform.position;

        float speed = 0;
        float duration = 1f;
        while (speed < 1)
        {
            speed += Time.unscaledDeltaTime / duration;
            mTarget.transform.rotation = Quaternion.Lerp(cameraOriginRotation, camerNewRotation, speed);
            mTarget.transform.position = Vector3.Lerp(start, EndPOS, speed);
            yield return null;
        }
        mInteraction.mStartedInteraction = false;
    }
}