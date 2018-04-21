using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class AlertMark : MonoBehaviour
{
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite red;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Appear()
    {
        myAnimator.ResetTrigger("disappear");
        myAnimator.SetTrigger("appear");
    }

    public void Disappear()
    {
        myAnimator.ResetTrigger("appear");
        myAnimator.SetTrigger("disappear");
    }

    public void SetYellow()
    {
        mySpriteRenderer.sprite = yellow;
    }

    public void SetRed()
    {
        mySpriteRenderer.sprite = red;
    }
}