using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DialogueCanvas : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image portrait;
    [SerializeField] private Text text;
    [SerializeField] private Sprite playerPortrait;
    [SerializeField] private Sprite policePortrait;
    [SerializeField] private Sprite bossPortrait;
    [SerializeField] private AudioSource jinglePlayer;
    [SerializeField] private AudioClip victoryJingle1;
    [SerializeField] private AudioClip victoryJingle2;
    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public Portrait Portrait
    {
        set
        {
            Sprite sprite;
            switch (value)
            {
                case Portrait.Player:
                    background.color = DanceStyle.greenColor;
                    sprite = playerPortrait;
                    break;
                case Portrait.Police:
                    background.color = DanceStyle.blueColor;
                    sprite = policePortrait;
                    break;
                case Portrait.Boss:
                    background.color = DanceStyle.bossColor;
                    sprite = bossPortrait;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("value", value, null);
            }

            portrait.sprite = sprite;
        }
    }

    public string Text
    {
        set { text.text = value; }
    }

    public void ShowFailure()
    {
        myAnimator.SetTrigger("failure");
    }

    public void Appear()
    {
        myAnimator.SetTrigger("appear");
        myAnimator.ResetTrigger("disappear");
    }

    public void Disappear()
    {
        myAnimator.SetTrigger("disappear");
        myAnimator.ResetTrigger("appear");
    }

    public IEnumerator StartVictorySequence()
    {
        myAnimator.SetTrigger("time");
        jinglePlayer.clip = victoryJingle1;
        jinglePlayer.Play();
        yield return new WaitWhile(() => jinglePlayer.isPlaying);
        myAnimator.SetTrigger("score");
        jinglePlayer.clip = victoryJingle2;
        jinglePlayer.Play();
        yield return new WaitWhile(() => jinglePlayer.isPlaying);
    }
}