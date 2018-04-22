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
        DanceStyle newStyle;

        if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            newStyle = DanceStyle.Fast;
        else if (Input.GetMouseButton(0))
            newStyle = DanceStyle.Slow;
        else if (Input.GetMouseButton(1))
            newStyle = DanceStyle.Average;
        else
            newStyle = DanceStyle.Idle;

        AssignStyle(newStyle);
    }

    private void AssignStyle(DanceStyle style)
    {
        var currentStyle = player.DanceStyle;
        if (currentStyle != null && currentStyle.Name == style.Name)
            return;
        spriteRenderer.color = style.PlayerColor;
        movement.MaxSpeed = style.MaxSpeed;

        if (currentStyle != null && currentStyle.PlayerAnimation != "")
            animator.ResetTrigger(currentStyle.PlayerAnimation);
        animator.SetTrigger(style.PlayerAnimation);
        player.DanceStyle = style;
    }

    private void MovePlayer()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var destination = mousePosition;
        destination.z = transform.position.z;
        movement.Destination = destination;
    }
}