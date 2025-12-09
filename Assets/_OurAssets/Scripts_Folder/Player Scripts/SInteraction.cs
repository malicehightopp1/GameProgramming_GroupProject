using UnityEngine;

public class SInteraction : MonoBehaviour
{
    [SerializeField] private float InteractRange;
    [SerializeField] private Camera mCamera;
    private bool mStarted = false;
    public bool mStartedInteraction
    {
        get { return mStarted; }
        set { mStarted = value; }
    }
    public void InteractionSystem()
    {
        Ray ray = new Ray(mCamera.transform.position, mCamera.transform.forward * InteractRange);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractRange))
        {
            Debug.Log($"Item hit is: {hit.transform.name}");
            if (hit.transform.TryGetComponent<IInteractable>(out var item))
            {
                if(mStarted == false)
                {
                    item.OnInteract(this);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        //Debug Draw
        Ray ray = new Ray(mCamera.transform.position, mCamera.transform.forward * InteractRange);
        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
    }
}
