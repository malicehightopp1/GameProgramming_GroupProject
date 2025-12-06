using UnityEngine;

public class SParticle : MonoBehaviour
{
    [SerializeField] float Timer;
    [SerializeField] float ForceX;
    [SerializeField] float ForceY;
    [SerializeField] Vector2 ForceXRange;
    [SerializeField] Transform OriginTransform;
    

    private void Start()
    {
        ForceX = Random.Range(ForceXRange.x, ForceXRange.y);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2 (ForceX, ForceY);
        rb.AddForce(force * OriginTransform.up);
        Destroy(this.gameObject, Timer);
    }

    public void GetRotationTransform(Transform origin)
    {
        OriginTransform = origin;
    }
}
