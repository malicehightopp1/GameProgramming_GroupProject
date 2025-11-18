using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SInventory : MonoBehaviour
{
    //This is just a setup for the complete Inventory system.
    //It only has the list and both a way to add and delete from the list
    //There is no max and Min on the list, the UI should determine that.

    public List<SFoodItemProfile> FoodItemProfiles = new List<SFoodItemProfile>();

    public bool AddFoodToList(SFoodItemProfile ingredientProfile)
    {
        if (ingredientProfile != null)
        {
            FoodItemProfiles.Add(ingredientProfile);
            return true;
        }

        return false;
    }

    public bool RemoveFoodFromList(SFoodItemProfile ingredientProfile)
    {
        var ingredientToRemove = FoodItemProfiles.SingleOrDefault(item => item.FoodItemID == ingredientProfile.FoodItemID);

        if (ingredientToRemove.FoodItemID != string.Empty)
        {
            FoodItemProfiles.Remove(ingredientToRemove);
            return true;
        }

        return false;
    }
}
