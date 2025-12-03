using UnityEngine;

public class SPlayer : MonoBehaviour
{
    //This Script is for methods that need a general player script so the movement controller isn't filled with a lot

    //For Holding the dishes
    [SerializeField] private GameObject mHeldItem;
    [SerializeField] private Transform mHeldItemTransform;
    [SerializeField] private Camera mHeldCamera;

    private void Start()
    {
        mHeldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        mHeldItemTransform.position = mHeldItemTransform.position;
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
        SDishItem DishItem = mHeldItem.GetComponent<SDishItem>();
        GameObject ItemDrop = Instantiate(DishItem.GiveDropItem().DishItemPrefab, mHeldItemTransform);
        ItemDrop.transform.SetParent(null, true);
        foreach (Transform child in mHeldItemTransform)
        {
            Destroy(child.gameObject);
        }
        mHeldItem = null;
    }
}
