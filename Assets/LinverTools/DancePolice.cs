using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Patrol))]
public class DancePolice: MonoBehaviour
{
    private Movement movement;
    private Player target;
    private Patrol patrol;
    [SerializeField] private float maxSpeed;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        patrol = GetComponent<Patrol>();
        target = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (!target.IsSuspicious)
        {
            patrol.IsActive = true;
            return;
        }

        patrol.IsActive = false;
        var targetPosition = target.transform.position;
        var direction = Vector3.Normalize(targetPosition - transform.position);
        movement.Destination = targetPosition - direction / 2;
        movement.MaxSpeed = maxSpeed * target.Suspiciousness;
    }
}