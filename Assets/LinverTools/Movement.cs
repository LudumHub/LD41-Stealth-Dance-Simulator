using UnityEngine;

public class Movement: MonoBehaviour
{
    [SerializeField] private Vector3 smoothDampVelocity;
    public Vector3 Destination;
    public float SmoothDampTime = 2f;
    public float MaxSpeed = 1f;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Destination,
            ref smoothDampVelocity, SmoothDampTime, MaxSpeed);
    }
}