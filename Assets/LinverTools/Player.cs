using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float suspiciousness;
    [SerializeField] private float suspiciousnessDropCooldown = 1f;
    private float timeSinceLastSuspiciousnessUpdate;

    public float Suspiciousness
    {
        get { return suspiciousness; }
        private set { suspiciousness = Mathf.Clamp(value, 0, 1f); }
    }

    public void RaiseSuspiciousness()
    {
        Suspiciousness += 0.1f;
        timeSinceLastSuspiciousnessUpdate = 0f;
    }

    private void Update()
    {
        timeSinceLastSuspiciousnessUpdate += Time.deltaTime;
        if (timeSinceLastSuspiciousnessUpdate > suspiciousnessDropCooldown)
            Suspiciousness -= 0.1f;
    }
}