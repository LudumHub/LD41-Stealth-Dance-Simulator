using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float suspiciousness;
    [SerializeField] private float suspiciousnessDropCooldown = 1f;
    [SerializeField] private AudioSource nani;
    [SerializeField] private AlertMark alertMark;
    private float timeSinceLastSuspiciousnessUpdate;
    public DanceStyle DanceStyle;
    private Movement movement;
    private float movementSpeed;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        movementSpeed = movement.MaxSpeed;
    }

    public float Suspiciousness
    {
        get { return suspiciousness; }
        private set { suspiciousness = Mathf.Clamp(value, 0, 1f); }
    }

    public bool StillBaka { get; set; }

    public bool IsSuspicious
    {
        get { return Suspiciousness > 0f; }
    }

    public void RaiseSuspiciousness()
    {
        StillBaka = true;
        Suspiciousness += 0.1f;
        timeSinceLastSuspiciousnessUpdate = 0f;
    }

    private void Update()
    {
        timeSinceLastSuspiciousnessUpdate += Time.deltaTime;
        if (timeSinceLastSuspiciousnessUpdate > suspiciousnessDropCooldown)
        {
            Suspiciousness -= 0.1f;
        }
    }

    public void SlowDown()
    {
        movement.MaxSpeed = movementSpeed / 10f;
    }

    public void RestoreSpeed()
    {
        movement.MaxSpeed = movementSpeed;
    }

    public IEnumerator PlayNani()
    {
        nani.Play();
        alertMark.SetRed();
        alertMark.Appear();
        while (nani.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void Stop()
    {
        movement.enabled = false;
    }
}