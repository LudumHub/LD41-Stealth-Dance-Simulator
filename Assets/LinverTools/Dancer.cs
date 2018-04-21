using UnityEngine;

public class Dancer : MonoBehaviour
{
    public DanceStyle DanceStyle;
    public Collider2D BodyCollider;
    public Collider2D AreaOfEffect;

    private void OnCollisionStay2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if (other.collider == BodyCollider)
        {
            player.RaiseSuspiciousness();
            player.RaiseSuspiciousness();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if (player.DanceStyle.Name != DanceStyle.Name)
            player.RaiseSuspiciousness();
    }
}