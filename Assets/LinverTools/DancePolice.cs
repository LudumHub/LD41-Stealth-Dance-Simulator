using UnityEngine;

[RequireComponent(typeof(Movement))]
public class DancePolice: MonoBehaviour
{
    private Movement movement;
    private Player target;
    [SerializeField] private float maxSpeed;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        target = FindObjectOfType<Player>();
    }

    private void Update()
    {
        movement.Destination = target.transform.position;
        movement.MaxSpeed = maxSpeed * target.Suspiciousness;
    }
}