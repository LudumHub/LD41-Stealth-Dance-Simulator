using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private PatrolPath path;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool restOnStart;
    private Movement movement;
    private float remainingRest;
    private IEnumerator<Vector3> positions;

    private void Awake()
    {
        IsActive = true;
        movement = GetComponent<Movement>();

        if (path == null)
            return;

        ResetPoints();
        if (restOnStart)
            RestAndMoveNext();
    }

    public bool IsActive { get; set; }

    private bool IsResting
    {
        get { return remainingRest > 0; }
    }

    private void Update()
    {
        if (path == null)
            return;

        if (!IsActive) return;
        if (IsResting)
        {
            remainingRest -= Time.deltaTime;
            if (remainingRest > 0)
                return;
        }

        var currentTarget = positions.Current;
        currentTarget.z = transform.position.z;
        if (Vector3.Distance(currentTarget, movement.Destination) > Mathf.Epsilon)
            movement.Destination = currentTarget;
        if (Vector3.Distance(currentTarget, transform.position) > 0.1f)
            return;
        RestAndMoveNext();
    }

    private void RestAndMoveNext()
    {
        if (!positions.MoveNext())
            ResetPoints();
        remainingRest = restTime;
    }

    private void ResetPoints()
    {
        positions = path.Positions.GetEnumerator();
        positions.MoveNext();
    }
}