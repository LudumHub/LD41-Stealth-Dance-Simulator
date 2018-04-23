using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DJ : MonoBehaviour
{
    [SerializeField] private AudioSource slowTrack;
    [SerializeField] private AudioSource averageTrack;
    [SerializeField] private AudioSource fastTrack;
    [SerializeField] private AudioSource idleTrack;
    private readonly List<WorldSnap> tiles = new List<WorldSnap>();

    private void Update()
    {
        var musicMap = new Dictionary<AudioSource, Color>
        {
            {slowTrack, DanceStyle.greenColor},
            {averageTrack, DanceStyle.yellowColor},
            {fastTrack, DanceStyle.redColor},
            {idleTrack, DanceStyle.blueColor},
        };
        var colorCounts = tiles
            .GroupBy(t => t.multColor)
            .ToDictionary(g => g.Key, g => g.Count());
        var totalCount = colorCounts
            .Sum(kvp => kvp.Value);
        var volumes = musicMap
            .ToDictionary(
                kvp => kvp.Key,
                kvp => GetValueOrDefault(colorCounts, kvp.Value, 0) / (float) totalCount);
        foreach (var kvp in volumes)
        {
            var track = kvp.Key;
            var volume = kvp.Value;
            AdjustVolume(track, volume);
        }
    }

    private void AdjustVolume(AudioSource audioSource, float value)
    {
        const float maxVolumeStep = 0.05f;
        var currentVolume = audioSource.volume;
        float resultVolume;
        if (Math.Abs(currentVolume - value) < maxVolumeStep)
            resultVolume = value;
        else
            resultVolume = currentVolume + Mathf.Sign(value - currentVolume) * maxVolumeStep;
        audioSource.volume = resultVolume;
    }

    private TValue GetValueOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key, TValue @default)
    {
        TValue value;
        return dictionary.TryGetValue(key, out value) ? value : @default;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var tile = other.GetComponent<WorldSnap>();
        if (tile == null) return;
        if (tiles.Contains(tile)) return;
        tiles.Add(tile);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var tile = other.GetComponent<WorldSnap>();
        if (tile == null) return;
        if (!tiles.Contains(tile)) return;
        tiles.Remove(tile);
    }
}