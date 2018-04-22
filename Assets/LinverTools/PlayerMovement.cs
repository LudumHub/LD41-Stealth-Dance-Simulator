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

    float timer = 0f;
    private void Dance()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            DanceStyle newStyle;
            if (timer <= DanceStyle.Fast.SecFromLastTap)
                newStyle = DanceStyle.Fast;
            else 
                newStyle = DanceStyle.Average;

            AssignStyle(newStyle);
            timer = 0f;
        }

        if (timer > Mathf.Max(player.DanceStyle.SecFromLastTap, 0.5f))
        {
            if (player.DanceStyle.Name == "Fast")
                AssignStyle(DanceStyle.Average);
            else
                AssignStyle(DanceStyle.Slow);

            timer = 0f;
        }

        var isMoving = (movement.Destination - transform.position).magnitude > 0.5f;
        if (player.DanceStyle.Name == "Slow" && !isMoving)
            AssignStyle(DanceStyle.Idle);
        if (player.DanceStyle.Name == "Idle" && isMoving)
            AssignStyle(DanceStyle.Slow);
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