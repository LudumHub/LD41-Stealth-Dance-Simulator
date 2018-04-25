using UnityEngine;

public class Exit : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player == null)
            return;
        player.HasAlibi = true;
        Ytics.LevelComplete();
        Fade.instance.LoadNextLevel();
    }
}
