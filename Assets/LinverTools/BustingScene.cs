using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BustingScene : MonoBehaviour
{
    [SerializeField] private DJ dj;
    [SerializeField] private DialogueCanvas dialogueCanvas;
    [SerializeField] private AudioSource bustedMusicBox;
    [SerializeField] private AudioSource failureMusicBox;
    [SerializeField] private AudioClip bustedTrack;
    [SerializeField] private AudioClip failureJingle;
    private Animator animator;
    private bool isClicked;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartBusting()
    {
        dj.VolumeMultiplier = 0.25f;
        foreach (var otherMovement in FindObjectsOfType<Movement>())
            otherMovement.enabled = false;
        animator.SetTrigger("start");
        bustedMusicBox.clip = bustedTrack;
        bustedMusicBox.Play();
    }

    [UsedImplicitly]
    public void StartDialogue()
    {
        dialogueCanvas.BackgroundColor = DanceStyle.blueColor;
        dialogueCanvas.Portrait = Portrait.Police;
        dialogueCanvas.Text = "POKAZHITE VASHY DOKUMENTY";
        dialogueCanvas.Appear();
        StartCoroutine(Dialogue());
    }

    private IEnumerator Dialogue()
    {
        while (!isClicked)
            yield return new WaitForEndOfFrame();
        dialogueCanvas.ShowFailure();
        failureMusicBox.Play();
        yield return new WaitForSeconds(2f);
        Fade.instance.ResetLevel();
    }

    public void HandleClick()
    {
        isClicked = true;
    }
}