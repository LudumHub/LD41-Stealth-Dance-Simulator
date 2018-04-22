﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DanceStyle
{
    public string Name;
    public float MinClicksPerSecond;
    public float MaxClicksPerSecond;
    public float MaxSpeed;
    public string PlayerAnimation;
    public Color PlayerColor;

    public static DanceStyle Idle
    {
        get
        {
            return new DanceStyle
            {
                Name = "Idle",
                MinClicksPerSecond = 0f,
                MaxClicksPerSecond = 1f,
                PlayerAnimation = "idle",
                MaxSpeed = 2f,
                PlayerColor = Color.white
            };
        }
    }

    public static DanceStyle Slow
    {
        get
        {
            return new DanceStyle
            {
                Name = "Slow",
                MinClicksPerSecond = 1f,
                MaxClicksPerSecond = 2f,
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
                MinClicksPerSecond = 2f,
                MaxClicksPerSecond = 4f,
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
                MinClicksPerSecond = 4f,
                MaxClicksPerSecond = float.MaxValue,
                PlayerAnimation = "fast",
                MaxSpeed = 3f,
                PlayerColor = Color.red
            };
        }
    }

    public static IEnumerable<DanceStyle> AllStyles
    {
        get
        {
            yield return Idle;
            yield return Slow;
            yield return Average;
            yield return Fast;
        }
    }
}