using UnityEngine;

public class SFoodItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SFoodItemProfile mFoodItemData;

    [SerializeField] private SInventory mInventory;

    private void Start()
    {
        mInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventory>();
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
}
