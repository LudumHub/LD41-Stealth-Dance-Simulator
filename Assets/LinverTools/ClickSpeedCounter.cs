using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClickSpeedCounter
{
    private const float SecondsToTrack = 3f;
    private readonly Queue<float> clickTimestamps = new Queue<float>();

    public float ClicksPerSecond
    {
        get { return clickTimestamps.Any() ? clickTimestamps.Count / SecondsToTrack : 0; }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
            clickTimestamps.Enqueue(Time.time);
        while (clickTimestamps.Any() && Time.time - clickTimestamps.Peek() >= SecondsToTrack)
            clickTimestamps.Dequeue();
    }
}