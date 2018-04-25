using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public static class Ytics
{
    public static void LevelComplete()
    {
        AnalyticsEvent.LevelComplete(CurrentLevel);
    }

    public static void LevelFail(Color playerColor, Color floorColor, Vector3 position)
    {
        var eventData = new Dictionary<string, object>
        {
            { "player_color", playerColor.ToString() },
            { "floor_color", floorColor.ToString() },
            { "position", position.ToString() }
        };
        AnalyticsEvent.LevelFail(CurrentLevel, eventData);
    }

    public static void IntroStart()
    {
        AnalyticsEvent.CutsceneStart(CurrentLevel.ToString());
    }

    public static void LevelStart()
    {
        AnalyticsEvent.LevelStart(CurrentLevel);
    }

    private static int CurrentLevel
    {
        get { return SceneManager.GetActiveScene().buildIndex; }
    }
}