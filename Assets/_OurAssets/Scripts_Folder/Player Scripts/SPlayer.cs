using UnityEngine;

public class SPlayer : MonoBehaviour
{
    //This Script is for methods that need a general player script so the movement controller isn't filled with a lot

    //For Holding the dishes
    [SerializeField] private GameObject mHeldItem;
    [SerializeField] private Transform mHeldItemTransform;
    [SerializeField] private Transform mHeldTransformPivot;
    [SerializeField] private Camera mHeldCamera;

    private void Start()
    {
        mHeldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        //This is for the Held Item to follow
        mHeldTransformPivot.rotation = mHeldCamera.transform.rotation;
    }
    public void HoldItem(GameObject heldItem) 
    {
        mHeldItem = Instantiate(heldItem, mHeldItemTransform);
        Rigidbody heldRigidbody = mHeldItem.GetComponent<Rigidbody>();
        heldRigidbody.isKinematic = true;
        heldRigidbody.useGravity = false;

    }

    public void DropItem() 
    {
        mHeldItem.GetComponent<SDishItem>().CallDropItem();
        Destroy(mHeldItem);
        mHeldItem = null;
    }
}
