using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Obstacle : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.RaiseSuspiciousness();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.StillBaka = false;
    }
}