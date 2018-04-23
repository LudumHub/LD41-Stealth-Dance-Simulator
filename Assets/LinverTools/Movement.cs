using UnityEngine;


public class Movement: MonoBehaviour
{
    [SerializeField] private Vector3 smoothDampVelocity;
    public Vector3 Destination;
    public static float SmoothDampTime = 1f;
    public float MaxSpeed = 1f;
    private Rigidbody2D myRigidbody;

    public bool doesFlipsAllowed = true;
    Vector3 NormalFlip;
    Vector3 MirroredFlip;
    private void Awake()
    {
        Destination = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();

        NormalFlip = transform.localScale;
        MirroredFlip = new Vector3(NormalFlip.x * -1, NormalFlip.y, NormalFlip.z);
    }

    private void Update()
    {
        var targetPosition = Vector3.SmoothDamp(transform.position, Destination,
            ref smoothDampVelocity, SmoothDampTime, MaxSpeed);

        if (myRigidbody != null)
            myRigidbody.MovePosition(targetPosition);
        else
            transform.position = targetPosition;

        if (doesFlipsAllowed)
            transform.localScale = transform.position.x < Destination.x ? NormalFlip : MirroredFlip;
        else
            transform.localScale = NormalFlip;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y*10);
    }

    public void Stop()
    {
        var isCloseToDestination = Vector3.Distance(transform.position, Destination) < 1f;
        if (isCloseToDestination)
            return;
        Destination = transform.position + Vector3.Normalize(Destination);
    }
}