using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BustingScene : MonoBehaviour
{
    [SerializeField] private DJ dj;
    [SerializeField] private AudioSource bustedMusicBox;
    [SerializeField] private AudioSource failureMusicBox;
    private DialogueCanvas dialogueCanvas;
    private Animator animator;
    private bool isClicked;

    private void Awake()
    {
        dialogueCanvas = FindObjectOfType<DialogueCanvas>();
        animator = GetComponent<Animator>();
    }

    public void StartBusting()
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
        animator.SetTrigger("start");
        bustedMusicBox.Play();
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
        yield return new WaitUntil(() => isClicked);
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