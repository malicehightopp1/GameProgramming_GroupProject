using UnityEngine;

[CreateAssetMenu(fileName = "NewFoodProfile", menuName = "ChefsKiss/FoodProfile")]
public class SFoodItemProfile : ScriptableObject
{
    public string FoodItemID;
    public string FoodItemName;
    public Sprite FoodItemIcon;
    public EFoodType FoodItemType;
}

public enum EFoodType
{
    Vegetable,
    Meat,
    Mushroom,
    Fruit,
    Condiment,
    AnimalProduct,
    Other
}
