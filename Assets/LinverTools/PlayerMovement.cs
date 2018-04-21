using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Dancer))]
public class PlayerMovement : MonoBehaviour
{
    private readonly ClickSpeedCounter speedCounter = new ClickSpeedCounter();
    private Movement movement;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update ()
    {
        speedCounter.Update();
        Dance();
        MovePlayer();
    }

    private DanceStyle currentStyle;

    private void Dance()
    {
        var newDance = DanceStyle.AllStyles
            .Last(s => speedCounter.ClicksPerSecond >= s.MinClicksPerSecond);
        if (currentStyle != null && currentStyle.Name == newDance.Name)
            return;
        spriteRenderer.color = newDance.PlayerColor;
        // Hack?
        movement.MaxSpeed = newDance.MinClicksPerSecond;
        if (currentStyle != null)
            animator.ResetTrigger(currentStyle.PlayerAnimation);
        animator.SetTrigger(newDance.PlayerAnimation);
        currentStyle = newDance;
    }

    private void MovePlayer()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = transform.position.z;
        movement.Destination = destination;
    }
}