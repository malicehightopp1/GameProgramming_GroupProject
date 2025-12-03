using System.Collections.Generic;
using UnityEngine;

public class SDishCreation : MonoBehaviour, IInteractable
{
    [SerializeField] private SDishItemProfile mDishProfile;

    [SerializeField] private SInventory mInventory;

    [SerializeField] private Transform mDishProducedLocation;

    private int mCorrectNumIngredients = 0;

    private bool bHasProducedDish = false;

    public void CookDish()
    {
        mInventory = GameObject.FindGameObjectWithTag("GameController").GetComponent<SInventory>();

        foreach (SFoodItemProfile ingredients in mDishProfile.IngredientsList)
        {
            foreach (SFoodItemProfile inventoryIngredients in mInventory.FoodItemProfiles)
            {
                mCorrectNumIngredients++;
            }
        }

        if (mCorrectNumIngredients >= mDishProfile.IngredientsList.Count)
        {
            if (!bHasProducedDish)
            {
                Instantiate(mDishProfile.DishItemPrefab, mDishProducedLocation);
                bHasProducedDish = true;
            }
        }
    }

    public void OnInteract(SInteraction playerInteract)
    {
        CookDish();
    }
}
