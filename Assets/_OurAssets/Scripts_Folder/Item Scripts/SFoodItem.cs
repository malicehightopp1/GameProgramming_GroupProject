using Unity.VisualScripting;
using UnityEngine;

public class SFoodItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SFoodItemProfile mFoodItemData;

    [SerializeField] private SInventory mInventory;

    [SerializeField] private bool bHasGivenItem = false;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventory>();
        Instantiate(mFoodItemData.FoodItemPrefab, this.transform);
    }

    public void OnInteract(SInteraction playerInteract)
    {
        if (bHasGivenItem == false)
        {
            bHasGivenItem = true;
            Debug.Log("Pickup Item!");
            ItemGrab();
        }
    }

    private void ItemGrab()
    {
        if (mFoodItemData != null)
        {
            mInventory.AddIngredientToList(mFoodItemData);
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + this.GetComponent<CapsuleCollider>().center, this.GetComponent<CapsuleCollider>().radius);
    }
}
