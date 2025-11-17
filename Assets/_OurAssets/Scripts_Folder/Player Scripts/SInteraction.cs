using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SInteraction : MonoBehaviour
{
    [SerializeField] private float InteractRange;
    [SerializeField] private Camera mCamera;

    public void InteractionSystem()
    {

        Ray ray = mCamera.ScreenPointToRay(mCamera.transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, InteractRange))
        {
            Debug.Log($"Item hit is: {hit.transform.name}");
            if (hit.transform.TryGetComponent<IInteractable>(out var item))
            {
                item.OnInteract(this);
                Debug.Log("Has Interactable");
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Debug Draw
        Debug.DrawRay(mCamera.transform.position, mCamera.transform.forward * InteractRange, Color.blue);
    }
}
