using UnityEngine;

public class SParticle : MonoBehaviour
{
    [SerializeField] float Timer;
    [SerializeField] float ForceX;
    [SerializeField] float ForceY;
    [SerializeField] Vector2 ForceXRange;
    [SerializeField] Vector2 ForceYRange;
    [SerializeField] Transform OriginTransform;

    [SerializeField] SFoodItemProfile mLiquidIngredient;
    

    private void Start()
    {
        ForceX = Random.Range(ForceXRange.x, ForceXRange.y);
        ForceY = Random.Range(ForceYRange.x, ForceYRange.y);
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector2 force = new Vector2 (ForceX, ForceY);
        rb.AddForce(force * OriginTransform.up);
        Destroy(this.gameObject, Timer);
    }

    public void GetRotationTransform(Transform origin)
    {
        OriginTransform = origin;
    }

    public void SetLiquidIngredient(SFoodItemProfile ingredient)
    {
        mLiquidIngredient = ingredient;
    }

    public SFoodItemProfile GiveIngredient()
    {
        return mLiquidIngredient;
    }

    public SpriteRenderer GiveChildSprite()
    {
        SpriteRenderer ChildSprite = GetComponentInChildren<SpriteRenderer>();
        return ChildSprite;
    }
}
