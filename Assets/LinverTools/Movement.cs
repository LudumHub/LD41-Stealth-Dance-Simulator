using UnityEngine;

public class Movement: MonoBehaviour
{
    [SerializeField] private Vector3 smoothDampVelocity;
    public Vector3 Destination;
    public float SmoothDampTime = 2f;
    public float MaxSpeed = 1f;
    private Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var targetPosition = Vector3.SmoothDamp(transform.position, Destination,
            ref smoothDampVelocity, SmoothDampTime, MaxSpeed);
        if (myRigidbody != null)
            myRigidbody.MovePosition(targetPosition);
        else
            transform.position = targetPosition;
    }

    public void Stop()
    {
        var isCloseToDestination = Vector3.Distance(transform.position, Destination) < 1f;
        if (isCloseToDestination)
            return;
        Destination = transform.position + Vector3.Normalize(Destination);
    }
}