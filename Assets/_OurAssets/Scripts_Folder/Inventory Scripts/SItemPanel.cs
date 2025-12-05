using UnityEngine;
using TMPro;

public class SItemPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mItemCounter;
    public SFoodItemProfile HeldIngredient;
    [SerializeField] private int mItemCountHeld;

    private void Start()
    {
        CheckIfTMProExists();
    }
    public void UpdateCount(int itemNum) 
    {
        Debug.Log($"Checking Update Count, adding {itemNum}");
        CheckIfTMProExists();
        mItemCountHeld = mItemCountHeld + itemNum;
        mItemCounter.text = mItemCountHeld.ToString();
    }

    private void CheckIfTMProExists()
    {
        mItemCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetHeldIngredient(SFoodItemProfile ingredient)
    {
        HeldIngredient = ingredient;
    }

    public int GiveCount()
    {
        return mItemCountHeld;
    }
}
