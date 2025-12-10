using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class SInventory : MonoBehaviour
{
    public int InventoryMaxUniqueItemAmount = 28;
    public int InventoryCurrentUniqueItemAmount = 0;

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
            SItemPanel itemPanel = GetItemPanelForIngredient(ingredientProfile);
            if (itemPanel == null)
            {
                Debug.Log($"Existing Item not Found");
                IngredientItemProfile.Add(ingredientProfile);
                UpdateInventoryUI(1, null);
                Destroy(ingredientProfile.GameObject());
                InventoryCurrentUniqueItemAmount++;
                return true;
            }

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
                        UpdateInventoryUI(1, childPanel);
                        Destroy(ingredientProfile.GameObject());
                        return true;
                    }
                }
                else if (childPanel.HeldIngredient != ingredientProfile)
                {
                    Debug.Log($"Existing Item not Found");
                    IngredientItemProfile.Add(ingredientProfile);
                    UpdateInventoryUI(1, null);
                    InventoryCurrentUniqueItemAmount++;
                    Destroy(ingredientProfile.GameObject());
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
                    UpdateInventoryUI(-1, childPanel);
                    return true;
                }
            }
        }
        return false;
    }


    private SItemPanel GetItemPanelForIngredient(SFoodItemProfile ingredientProfile)
    {
        Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
        foreach (Transform child in allChildren)
        {
            SItemPanel childPanel = child.GetComponent<SItemPanel>();
            if (childPanel.HeldIngredient == ingredientProfile)
            {
                return childPanel;
            }
        }
        return null;
    }
    public void UpdateInventoryUI(int count, SItemPanel childPanel)
    {
        //Transform allChildren = UIInventoryPanelItems.GetComponent<Transform>();
        //foreach (Transform child in allChildren)
        //{
        //    Destroy(child.gameObject);
        //    Debug.Log(child.name);
        //    Debug.Log("All Items Deleted.");
        //}

        foreach (var item in IngredientItemProfile)
        {
            SItemPanel itemPanelScript;

            if (childPanel == null)
            {
                //Create new Icon if Item is Unique
                GameObject itemObject;
                itemObject = Instantiate(UIItemImage, UIInventoryPanelItems.gameObject.transform, false);
                itemObject.GetComponent<Image>().sprite = item.FoodItemIcon;
                itemPanelScript = itemObject.GetComponent<SItemPanel>();
            }
            else
            {
                itemPanelScript = childPanel;
            }
            
            itemPanelScript.SetHeldIngredient(item);
            Debug.Log($"Updating Count");
            itemPanelScript.UpdateCount(count);
            Debug.Log("Item Generated.");
        }
    }
}
