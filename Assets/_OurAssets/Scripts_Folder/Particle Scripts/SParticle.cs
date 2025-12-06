using UnityEngine;

public class SParticle : MonoBehaviour
{
    [SerializeField] float Timer;
    [SerializeField] float ForceX;
    [SerializeField] float ForceY;
    [SerializeField] Vector2 ForceXRange;
    [SerializeField] Vector2 ForceYRange;
    [SerializeField] Transform OriginTransform;
    

    private void Start()
    {
        ForceX = Random.Range(ForceXRange.x, ForceXRange.y);
        ForceY = Random.Range(ForceYRange.x, ForceYRange.y);
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
