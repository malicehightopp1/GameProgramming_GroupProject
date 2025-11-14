using UnityEngine;

public class SMoveCamera : MonoBehaviour
{
    [SerializeField] private Transform mPlayerLocation;

    private void Update()
    {
        this.transform.position = mPlayerLocation.position;
    }
}
