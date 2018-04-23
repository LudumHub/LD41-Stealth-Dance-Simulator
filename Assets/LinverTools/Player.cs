using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    [SerializeField] private float suspiciousness;
    [SerializeField] private float suspiciousnessDropCooldown = 1f;
    [SerializeField] private AudioSource nani;
    [SerializeField] private AlertMark alertMark;
    [SerializeField] private BustingScene bustingScene;
    private float timeSinceLastSuspiciousnessUpdate;
    public DanceStyle DanceStyle;
    private Movement movement;
    public static float SuspiciousnessPoint = 0.2f;

    private void Awake()
    {
        movement = GetComponent<Movement>();
        DanceStyle = DanceStyle.Slow;
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
                StartBusting();
        }
    }

    public bool HasAlibi { get; set; }

    public bool StillBaka { get; set; }

    public bool IsSuspicious
    {
        get { return Suspiciousness > 0f; }
    }

    public bool IsVictorious { get; set; }

    public void RaiseSuspiciousness()
    {
        if (HasAlibi) return;
        StillBaka = true;
        Suspiciousness += SuspiciousnessPoint * Time.deltaTime;
        timeSinceLastSuspiciousnessUpdate = 0f;
    }

    private void Update()
    {
        if (HasAlibi) return;
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
    private float timer = 1 + selfSuspicionessDelay;
    public Color CorrectColor;
    private void LateUpdate()
    {
        if (HasAlibi) return;
        timer += Time.deltaTime;
        if (timer > 1 + selfSuspicionessDelay)
        {
            timer--;
            var cell = FloorDictionary.instance.FindCellByWordspaceCoords(transform.position);
            if (cell == null) return;

            CorrectColor = cell.multColor.a == 1f ? cell.multColor : DanceStyle.greenColor;
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

    public void StartBusting()
    {
        HasAlibi = true;
        movement.enabled = false;
        if (IsVictorious)
            bustingScene.StartBustingPrank();
        else
            bustingScene.StartBusting();
    }
}