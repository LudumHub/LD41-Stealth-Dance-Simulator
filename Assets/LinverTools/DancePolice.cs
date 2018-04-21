using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Patrol))]
public class DancePolice: MonoBehaviour
{
    private Movement movement;
    private Player player;
    private Patrol patrol;
    [SerializeField] private AlertMark alertMark;
    [SerializeField] private AudioSource druzhokPirozhok;
    [SerializeField] private AudioSource youAreAlreadyDead;
    [SerializeField] private float bakaAwait = 1f;
    private float bakaAwaitRemaining = 1f;
    private bool isNaniActive;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        patrol = GetComponent<Patrol>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isNaniActive)
            return;
        if (!player.IsSuspicious)
        {
            bakaAwaitRemaining = bakaAwait;
            player.RestoreSpeed();
            alertMark.Disappear();
            patrol.IsActive = true;
            return;
        }
        alertMark.SetYellow();
        alertMark.Appear();
        if (patrol.IsActive)
            druzhokPirozhok.Play();
        patrol.IsActive = false;
        var targetPosition = player.transform.position;
        var direction = Vector3.Normalize(targetPosition - transform.position);
        movement.Destination = targetPosition - direction / 2;
        var distance = Vector3.Distance(targetPosition, transform.position);
        if (distance < 2f)
            player.SlowDown();
        if (distance < 1.5f)
        {
            bakaAwaitRemaining -= Time.deltaTime;
            if (bakaAwaitRemaining < 0)
            {
                StartCoroutine(NaniCoroutine());
                isNaniActive = true;
            }
        }
    }

    private IEnumerator NaniCoroutine()
    {
        player.Stop();
        alertMark.Disappear();
        yield return new WaitForSeconds(0.12f);
        alertMark.SetRed();
        alertMark.Appear();
        youAreAlreadyDead.Play();
        while (youAreAlreadyDead.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return StartCoroutine(player.PlayNani());
        SceneManager.LoadScene(0);
    }
}