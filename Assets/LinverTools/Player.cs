using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float suspiciousness;
    [SerializeField] private float suspiciousnessDropCooldown = 1f;
    [SerializeField] private AudioSource nani;
    [SerializeField] private AudioSource bustedMusicBox;
    [SerializeField] private AudioClip bustedTrack;
    [SerializeField] private AudioClip failureJingle;
    [SerializeField] private Animator bustingAnimator;
    [SerializeField] private AlertMark alertMark;
    [SerializeField] private DJ dj;
    private float timeSinceLastSuspiciousnessUpdate;
    public DanceStyle DanceStyle;
    private Movement movement;
    public static float SuspiciousnessPoint = 0.2f;
    private bool isBusted;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public RectTransform SuspIndicator;
    private float maxSuspIndicatorSize = 100;

    public float Suspiciousness
    {
        get { return suspiciousness; }
        private set
        {
            suspiciousness = Mathf.Clamp(value, 0, 1f);
            if (Math.Abs(suspiciousness - 1f) < Mathf.Epsilon)
                StartCoroutine(StartBusting());
        }
    }

    public bool StillBaka { get; set; }

    public bool IsSuspicious
    {
        get { return Suspiciousness > 0f; }
    }

    public void RaiseSuspiciousness()
    {
        if (isBusted) return;
        StillBaka = true;
        Suspiciousness += SuspiciousnessPoint * Time.deltaTime;
        timeSinceLastSuspiciousnessUpdate = 0f;
    }

    private void Update()
    {
        if (isBusted) return;
        timeSinceLastSuspiciousnessUpdate += Time.deltaTime;
        if (timeSinceLastSuspiciousnessUpdate > suspiciousnessDropCooldown)
        {
            Suspiciousness -= SuspiciousnessPoint * Time.deltaTime * 2;
        }

        var size = 0f;
        if (suspiciousness > 0)
            size = maxSuspIndicatorSize * suspiciousness;
        SuspIndicator.transform.localScale = new Vector3(size, 1, 1);
    }

    public void SlowDown()
    {
        movement.MaxSpeed = DanceStyle.MaxSpeed / 10f;
    }

    public void RestoreSpeed()
    {
        movement.MaxSpeed = DanceStyle.MaxSpeed;
    }

    public IEnumerator PlayNani()
    {
        nani.Play();
        alertMark.SetRed();
        alertMark.Appear();
        while (nani.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void Stop()
    {
        movement.enabled = false;
    }

    static float selfSuspicionessDelay = 0.5f;
    private float timer = selfSuspicionessDelay;
    public Color CorrectColor;
    private void LateUpdate()
    {
        if (isBusted) return;
        timer += Time.deltaTime;
        if (timer > 1 + selfSuspicionessDelay)
        {
            timer--;
            var cell = FloorDictionary.instance.FindCellByWordspaceCoords(transform.position);
            if (cell == null) return;

            CorrectColor = cell.multColor;
            if (CorrectColor != DanceStyle.PlayerColor)
            {
                RaiseSuspiciousness();
                alertMark.SetYellow();
                alertMark.Appear();
            }
            else
                alertMark.Disappear();
        }

        if (CorrectColor != DanceStyle.PlayerColor)
            RaiseSuspiciousness();
        if (CorrectColor == DanceStyle.blueColor)
            RaiseSuspiciousness();
    }

    private IEnumerator StartBusting()
    {
        isBusted = true;
        dj.VolumeMultiplier = 0.25f;
        movement.enabled = false;
        foreach (var otherMovement in FindObjectsOfType<Movement>())
            otherMovement.enabled = false;
        bustingAnimator.SetTrigger("start");
        bustedMusicBox.clip = bustedTrack;
        bustedMusicBox.Play();
        yield return null;
    }

    public void StartFailureBustingDialogue()
    {
        StartCoroutine(FailureBustingDialogue());
    }

    private IEnumerator FailureBustingDialogue()
    {
        yield return null;
    }
}