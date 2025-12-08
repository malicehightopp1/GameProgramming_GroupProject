using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SLiquidIngridient : MonoBehaviour
{
    [SerializeField] List<SFoodItemProfile> mIngredientsInside = new List<SFoodItemProfile>();
    [SerializeField] float mNeededAmountToCreateIngredient = 100f;
    [SerializeField] float mAmountInside = 0f;

    [SerializeField] GameObject mFillPrefab;
    [SerializeField] Transform mFillTransform;
    [SerializeField] Transform mLiquidHolder;

    [SerializeField] Color mMaterialColor;
    private void GetIngredient(GameObject particle)
    {
        if (mAmountInside >= mNeededAmountToCreateIngredient)
        {
            mAmountInside = 0f;
            GameObject layer = Instantiate(mFillPrefab, mFillTransform);
            HandleMaterialColor(particle, layer);

            layer.transform.SetParent(mLiquidHolder.transform);
            Vector3 newLocation = new Vector3(mFillTransform.position.x, mFillTransform.position.y + layer.transform.localScale.y, mFillTransform.position.z);
            mFillTransform.position = newLocation;
            CapsuleCollider Collider = this.GetComponent<CapsuleCollider>();
            Collider.height += layer.transform.localScale.y;
            Vector3 newCenter = new Vector3(0f, Collider.center.y + (layer.transform.localScale.y), 0f);
            float oldCenterY = Collider.center.y;
            Collider.center = newCenter;

            mIngredientsInside.Add(particle.GetComponent<SParticle>().GiveIngredient());
        }
    }

    private void HandleMaterialColor(GameObject particle, GameObject layer)
    {
        Renderer layerRenderer = layer.GetComponent<Renderer>();
        Material layerMaterial = layerRenderer.material;

        SpriteRenderer particleSprite = particle.GetComponent<SParticle>().GiveChildSprite();

        mMaterialColor = particleSprite.color;

        layerMaterial.color = mMaterialColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LiquidParticle"))
        {
            mAmountInside++;
            GetIngredient(other.gameObject);
            Destroy(other.gameObject, 0.2f);
        }
    }
}
