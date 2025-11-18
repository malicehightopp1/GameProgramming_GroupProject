using UnityEngine;

public class SFoodItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SFoodItemProfile mFoodItemData;
    [SerializeField] private float mPickupDistance;

    [SerializeField] private SInventory mInventory;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventory>();
    }

    public void OnInteract(SInteraction playerInteract)
    {
        float distanceFromPlayer = Vector3.Distance(this.transform.position, playerInteract.transform.position);
        if (distanceFromPlayer <= mPickupDistance)
        {
            Debug.Log("Pickup Item!");
            ItemGrab();
        }
    }

    private void ItemGrab()
    {
        if (mFoodItemData != null)
        {
            mInventory.AddFoodToList(mFoodItemData);
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, mPickupDistance);
    }
}
