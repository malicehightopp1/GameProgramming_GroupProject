using UnityEngine;

public class SDishItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SDishItemProfile mDishItemProfile;
    public void OnInteract(SInteraction playerInteract)
    {
        playerInteract.GetComponent<SPlayer>().HoldItem(mDishItemProfile.DishItemPrefab);
        Destroy(this.gameObject);
    }

    public SDishItemProfile GiveDropItem()
    {
        return mDishItemProfile;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + this.GetComponent<CapsuleCollider>().center, this.GetComponent<CapsuleCollider>().radius);
    }
}
