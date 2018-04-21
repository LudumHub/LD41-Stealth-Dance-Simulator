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
        var newStyle = DanceStyle.AllStyles
            .Last(s => speedCounter.ClicksPerSecond >= s.MinClicksPerSecond);
        var currentStyle = player.DanceStyle;
        if (currentStyle != null && currentStyle.Name == newStyle.Name)
            return;
        spriteRenderer.color = newStyle.PlayerColor;
        // Hack?
        movement.MaxSpeed = newStyle.MinClicksPerSecond;
        if (currentStyle != null)
            animator.ResetTrigger(currentStyle.PlayerAnimation);
        animator.SetTrigger(newStyle.PlayerAnimation);
        player.DanceStyle = newStyle;
    }

    private void MovePlayer()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = transform.position.z;
        movement.Destination = destination;
    }
}