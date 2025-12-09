using UnityEngine;

public class SLiquidContainer : MonoBehaviour
{
    [SerializeField] SParticleProducer mParticleOrigin;

    private void Start()
    {
        mParticleOrigin = GetComponentInChildren<SParticleProducer>();
    }

    private void Update()
    {
        SetActive();
    }

    void SetActive()
    {
        float originAngle = this.transform.eulerAngles.z;
        if (originAngle > 180)
        {
            originAngle -= 360;
        }

        float absOriginAngle = Mathf.Abs(originAngle);

        if (absOriginAngle <= 25f)
        {
            mParticleOrigin.ToggleFalse();
        }
        else
        {
            mParticleOrigin.ToggleTrue();
        }
    }
}
