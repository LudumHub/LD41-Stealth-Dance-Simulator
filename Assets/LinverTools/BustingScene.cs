using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BustingScene : MonoBehaviour
{
    [SerializeField] private DJ dj;
    [SerializeField] private AudioSource bustedMusicBox;
    [SerializeField] private AudioSource failureMusicBox;
    [SerializeField] private AudioSource victoryMusicBox;
    private DialogueCanvas dialogueCanvas;
    private Animator animator;

    private void Awake()
    {
        dialogueCanvas = FindObjectOfType<DialogueCanvas>();
        animator = GetComponent<Animator>();
    }

    public void StartBusting()
    {
        StartCommonBusting();
        animator.SetTrigger("start");
    }

    [UsedImplicitly]
    public void StartDialogue()
    {
        dialogueCanvas.Portrait = Portrait.Police;
        dialogueCanvas.Text = "POKAZHITE VASHY DOKUMENTY";
        dialogueCanvas.Appear();
        StartCoroutine(Dialogue());
    }

    private IEnumerator Dialogue()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        dialogueCanvas.ShowFailure();
        failureMusicBox.Play();
        yield return new WaitForSeconds(2f);
        Fade.instance.ResetLevel();
    }

    public void StartBustingPrank()
    {
        StartCommonBusting();
        animator.SetTrigger("prank");
    }

    private void StartCommonBusting()
    {
        dj.VolumeMultiplier = 0.25f;
        foreach (var otherMovement in FindObjectsOfType<Movement>())
            otherMovement.enabled = false;
        foreach (var dancer in FindObjectsOfType<Dancer>())
        {
            var animators = dancer.GetComponentsInChildren<Animator>();
            foreach (var dancerAnimator in animators)
                dancerAnimator.enabled = false;
        }
        bustedMusicBox.Play();
    }

    [UsedImplicitly]
    private void StartVictoryJingle()
    {
        dj.VolumeMultiplier = 0;
        victoryMusicBox.Play();
    }

    [UsedImplicitly]
    private IEnumerator StartVicotoryDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(dialogueCanvas.StartVictorySequence());
    }
}