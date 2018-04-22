using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DanceStyle
{
    public string Name;
    public float SecFromLastTap;
    public float MaxSpeed;
    public string PlayerAnimation;
    public Color PlayerColor;

    public static DanceStyle Slow
    {
        get
        {
            return new DanceStyle
            {
                Name = "Slow",
                SecFromLastTap = float.MaxValue,
                PlayerAnimation = "slow",
                MaxSpeed = 1f,
                PlayerColor = Color.green
            };
        }
    }

    public static DanceStyle Average
    {
        get
        {
            return new DanceStyle
            {
                Name = "Average",
                SecFromLastTap = 1f,
                PlayerAnimation = "average",
                MaxSpeed = 2f,
                PlayerColor = Color.yellow
            };
        }
    }

    public static DanceStyle Fast
    {
        get
        {
            return new DanceStyle
            {
                Name = "Fast",
                SecFromLastTap = 0.5f,
                PlayerAnimation = "fast",
                MaxSpeed = 3f,
                PlayerColor = Color.red
            };
        }
    }

    public static DanceStyle Idle
    {
        get
        {
            return new DanceStyle
            {
                Name = "Idle",
                PlayerAnimation = "slow",
                PlayerColor = Color.blue
            };
        }
    }

    public static IEnumerable<DanceStyle> AllStyles
    {
        get
        {
            yield return Slow;
            yield return Average;
            yield return Fast;
        }
    }
}