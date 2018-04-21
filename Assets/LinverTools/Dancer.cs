using UnityEngine;

public class Dancer : MonoBehaviour
{
    public AlertMark AlertMark;
    public DanceStyle DanceStyle;
    public Collider2D BodyCollider;
    public Collider2D AreaOfEffect;

    private void OnCollisionExit2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if (other.otherCollider == BodyCollider)
        {
            AlertMark.SetYellow();
            player.StillBaka = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        if (other.otherCollider == BodyCollider)
        {
            AlertMark.SetRed();
            AlertMark.Appear();
            player.RaiseSuspiciousness();
            player.RaiseSuspiciousness();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        AlertMark.Disappear();
        if (player.DanceStyle.Name != DanceStyle.Name)
        {
            AlertMark.SetYellow();
            AlertMark.Appear();
            player.RaiseSuspiciousness();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null) return;
        AlertMark.Disappear();
        player.StillBaka = false;
    }
}