using System.Collections.Generic;
using UnityEngine;

public class SDishCreation : MonoBehaviour, IInteractable
{
    [SerializeField] private SDishItemProfile mDishProfile;

    [SerializeField] private SInventory mInventory;

    [SerializeField] private Transform mDishProducedLocation;

    private int mCorrectNumIngredients = 0;

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
            Instantiate(mDishProfile.DishItemPrefab, mDishProducedLocation);
        }
    }

    public void OnInteract(SInteraction playerInteract)
    {
        CookDish();
    }
}
