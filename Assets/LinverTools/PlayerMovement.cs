using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    private readonly ClickSpeedCounter speedCounter = new ClickSpeedCounter();
    private Movement movement;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private Player player;
    [SerializeField] private float idleRadius = 50f;
    [SerializeField] private float slowRadius = 100f;
    [SerializeField] private float averageRadius = 200f;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        player = GetComponent<Player>();
    }

    private void Update ()
    {
        speedCounter.Update();
        Dance();
        MovePlayer();
    }

    private void Dance()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        var mousePosition = Input.mousePosition;
        var distance = Vector3.Distance(screenPosition, mousePosition);
        DanceStyle newStyle;
        if (distance < idleRadius)
            newStyle = DanceStyle.Idle;
        else if (distance < slowRadius)
            newStyle = DanceStyle.Slow;
        else if (distance < averageRadius)
            newStyle = DanceStyle.Average;
        else
            newStyle = DanceStyle.Fast;
        AssignStyle(newStyle);
    }

    private void AssignStyle(DanceStyle style)
    {
        var currentStyle = player.DanceStyle;
        if (currentStyle != null && currentStyle.Name == style.Name)
            return;
        spriteRenderer.color = style.PlayerColor;
        // Hack?
        movement.MaxSpeed = style.MinClicksPerSecond;
        if (currentStyle != null)
            animator.ResetTrigger(currentStyle.PlayerAnimation);
        animator.SetTrigger(style.PlayerAnimation);
        player.DanceStyle = style;
    }

    private void MovePlayer()
    {
        if (Input.GetMouseButtonUp(0))
        {
            movement.Stop();
            return;
        }
        if (!Input.GetMouseButton(0)) return;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var destination = mousePosition;
        destination.z = transform.position.z;
        movement.Destination = destination;
    }
}