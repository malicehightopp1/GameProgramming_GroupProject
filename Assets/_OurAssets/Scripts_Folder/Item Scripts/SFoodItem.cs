using Unity.VisualScripting;
using UnityEngine;

public class SFoodItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SFoodItemProfile mFoodItemData;

    [SerializeField] private SInventory mInventory;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventory>();
        Instantiate(mFoodItemData.FoodItemPrefab, this.transform);
    }

    public void OnInteract(SInteraction playerInteract)
    {
        Debug.Log("Pickup Item!");
        ItemGrab();
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
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + this.GetComponent<CapsuleCollider>().center, this.GetComponent<CapsuleCollider>().radius);
    }
}
