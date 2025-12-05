using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SInventory : MonoBehaviour
{
    public int InventoryMaxUniqueItemAmount = 28;
    public int InventoryCurrentUniqueItemAmount = 0;

    [SerializeField] private List<int> mSavedItemCount = new List<int>();

    public int TotalMaxItemCount = 16;

    public List<SFoodItemProfile> IngredientItemProfile = new List<SFoodItemProfile>();

    public GameObject UIInventoryPanelItems;

    public GameObject UIItemImage;


    private void Start()
    {
        SearchingPanels();
    }
    private void SearchingPanels()
    {
        UIInventoryPanelItems = GameObject.FindGameObjectWithTag("ItemsPanel");
        Debug.Log("Finding...");
    }

    public bool AddIngredientToList(SFoodItemProfile ingredientProfile)
    {
        if (InventoryCurrentUniqueItemAmount >= InventoryMaxUniqueItemAmount) { return false; }

        Debug.Log($"Starting Adding");

        if (ingredientProfile != null)
        {
            Debug.Log($"IngredientProfile is not NULL");
            Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
            foreach (Transform child in allChildren)
            {
                Debug.Log($"Searching in Childen");
                SItemPanel childPanel = child.GetComponent<SItemPanel>();
                if (childPanel != null)
                {
                    Debug.Log($"Child Panel is not Null");
                    if (childPanel.HeldIngredient == ingredientProfile)
                    {
                        Debug.Log($"Found Existing Item");
                        UpdateInventoryUI();
                        UpdateCountList(1);
                        return true;
                    }
                }
                else
                {
                    Debug.Log($"Found Existing Item not Found");
                    IngredientItemProfile.Add(ingredientProfile);
                    mSavedItemCount.Add(1);
                    UpdateInventoryUI();
                    UpdateCountList(0);
                    InventoryCurrentUniqueItemAmount++;
                    return true;
                }

            }
        }
        return false;
    }
    public bool RemoveFoodFromList(SFoodItemProfile ingredientProfile)
    {
        Debug.Log($"Updating UI");
        var ingredientToRemove = IngredientItemProfile.SingleOrDefault(item => item.FoodItemID == ingredientProfile.FoodItemID);

        //Checking if the FoodItem exists
        if (ingredientToRemove.FoodItemID != string.Empty)
        {
            //Getting The parent item that holds the children
            Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
            foreach (Transform child in allChildren)
            {
                SItemPanel childPanel = child.GetComponent<SItemPanel>();
                if (childPanel.HeldIngredient == ingredientToRemove)
                {
                    if(childPanel.GiveCount() <= 1)
                    {
                        IngredientItemProfile.Remove(ingredientProfile);
                    }
                    UpdateInventoryUI();
                    UpdateCountList(-1);
                    return true;
                }
            }
        }
        return false;
    }

    public void UpdateCountList(int itemCount)
    {
        for (int i = 0; i <= mSavedItemCount.Count; i++)
        {
            Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
            foreach (Transform child in allChildren)
            {
                mSavedItemCount[i] = child.GetComponent<SItemPanel>().GiveCount() + itemCount;
            }
        }
    }
    public void UpdateInventoryUI()
    {
        Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            Destroy(child.gameObject);
            Debug.Log(child.name);
            Debug.Log("All Items Deleted.");
        }

        foreach (var item in IngredientItemProfile)
        {
            //TODO: Items Spawning with same Icon no matter what.
            GameObject itemObject = Instantiate(UIItemImage, UIInventoryPanelItems.gameObject.transform, false);
            itemObject.GetComponent<Image>().sprite = item.FoodItemIcon;
            SItemPanel itemPanelScript = itemObject.GetComponent<SItemPanel>();
            itemPanelScript.SetHeldIngredient(item);
            Debug.Log($"Updating Count");
            //TODO: Add Item Count here
            Debug.Log("Item Generated.");
        }
    }
}
