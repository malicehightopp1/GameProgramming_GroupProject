using UnityEngine;

public class SDishItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SDishItemProfile mDishItemProfile;
    public void OnInteract(SInteraction playerInteract)
    {
        playerInteract.GetComponent<SPlayer>().HoldItem(mDishItemProfile.DishItemPrefab);
        Destroy(this.gameObject);
    }

    public void CallDropItem(Transform dropPos)
    {
        ItemDrop(dropPos);
    }
    private void ItemDrop(Transform dropPos)
    {
        Instantiate(this.gameObject, dropPos);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + this.GetComponent<CapsuleCollider>().center, this.GetComponent<CapsuleCollider>().radius);
    }
}
