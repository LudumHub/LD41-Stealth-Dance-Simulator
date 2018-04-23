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
    public static Color greenColor = new Color(135 / 255.0f, 220 / 255.0f, 34/ 255.0f);
    public static Color yellowColor = new Color(245 / 255.0f, 250 / 255.0f, 0 / 255.0f);
    public static Color redColor = new Color(255 / 255.0f, 92 / 255.0f, 36 / 255.0f);
    public static Color blueColor = new Color(13 / 255.0f, 144 / 255.0f, 255 / 255.0f);
    public static Color bossColor = new Color(255 / 255.0f, 87 / 255.0f, 129 / 255.0f);

    public static DanceStyle Slow
    {
        get
        {
            return new DanceStyle
            {
                Name = "Slow",
                SecFromLastTap = float.MaxValue,
                PlayerAnimation = "slow",
                MaxSpeed = 2f,
                PlayerColor = DanceStyle.greenColor
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
                PlayerColor = DanceStyle.yellowColor
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
                SecFromLastTap = 0.4f,
                PlayerAnimation = "fast",
                MaxSpeed = 3f,
                PlayerColor = DanceStyle.redColor
            };
        }
    }

    public static DanceStyle Police
    {
        get
        {
            return new DanceStyle
            {
                Name = "Police",
                PlayerColor = DanceStyle.blueColor
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
            yield return Police;
        }
    }
}