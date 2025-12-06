using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SParticleProducer : MonoBehaviour
{
    [SerializeField] private float ParticleSpeed;
    [SerializeField] private Transform mParticleOrigin;
    [SerializeField] private GameObject mParticle;

    [SerializeField] private bool mIsActive;

    [SerializeField] private float ConversionSpeed;

    [SerializeField] private Color mParticleColor;

    

    void Start()
    {
        ConversionSpeed = ParticleSpeed;
        mIsActive = true;
        StartCoroutine(GenerateParticle());
    }

    void Update()
    {
        SetSpeed();
    }

    private IEnumerator GenerateParticle()
    {
        while (mIsActive)
        {
            GameObject Particle = Instantiate(mParticle, mParticleOrigin);
            Particle.GetComponent<SParticle>().GetRotationTransform(this.transform);
            Color color = new Vector4(mParticleColor.r, mParticleColor.g, mParticleColor.b, 255);
            Particle.GetComponentInChildren<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(ParticleSpeed);
        }
    }

    private void SetSpeed()
    {

        float originAngle = mParticleOrigin.transform.eulerAngles.z;
        if (originAngle > 180)
        {
            originAngle -= 360;
        }

        float absOriginAngle = Mathf.Abs(originAngle);

        if (absOriginAngle <= 45f)
        {
            ParticleSpeed = ConversionSpeed / 0.5f;
        }
        else if (absOriginAngle <= 90f)
        {
            ParticleSpeed = ConversionSpeed;
        }
        else if (absOriginAngle <= 135f)
        {
            ParticleSpeed = ConversionSpeed / 1.5f;
        }
        else
        {
            ParticleSpeed = ConversionSpeed / 2f;
        }

    }

    private void Toggle()
    {
        mIsActive = !mIsActive;
    }
}
