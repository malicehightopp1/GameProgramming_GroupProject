using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodProfile", menuName = "ChefsKiss/DishProfile")]
public class SDishItemProfile : ScriptableObject
{
    public string DishItemID;
    public string DishName;
    public Sprite DishItemIcon;
    public GameObject DishItemPrefab;

    public List<SFoodItemProfile> IngredientsList = new List<SFoodItemProfile>();
}
