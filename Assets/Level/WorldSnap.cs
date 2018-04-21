﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldSnap : MonoBehaviour {
    Vector3 prevPosition = Vector3.zero;
    void Update () {
        if (!Application.isPlaying &&
            prevPosition != transform.position)
            Snap();
    }

    Vector3 awakeCoords;
    void Awake()
    {
        UpdateCellCoords();
        awakeCoords = transform.position;

        if (Application.isPlaying)
            transform.position = new Vector3(awakeCoords.x, awakeCoords.y - 5, awakeCoords.z);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(Mathf.Abs(x + y) * 0.1f);
        while (transform.position != awakeCoords)
        {
            transform.position = Vector3.MoveTowards(transform.position, awakeCoords, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }

        if (x % 2 == 0)
            yield return new WaitForSeconds(0.5f);

        var renderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            renderer.color = Color.gray * multColor;
            yield return new WaitForSeconds(1f);
            renderer.color = Color.white * multColor;
            yield return new WaitForSeconds(1f);
        }
    }

    static float cellSize = 0.6f;
    float x;
    float y;
    private void Snap()
    {
        UpdateCellCoords();

        transform.localPosition = new Vector3(x * cellSize,
            y * cellSize / 2, transform.localPosition.z);

        prevPosition = transform.position;
    }

    private void UpdateCellCoords()
    {
        x = GetSnapValue(transform.localPosition.x, cellSize);
        y = GetSnapValue(transform.localPosition.y, cellSize / 2);

        if ((x + y) % 2 == 0)
            x++;
    }

    private float GetSnapValue(float value, float cellsize)
    {
        return Mathf.FloorToInt(value / cellsize);
    }

    private Color multColor = Color.white;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var dancer = other.GetComponent<Dancer>();
        if (dancer == null) return;
        multColor = dancer.DanceStyle.PlayerColor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        multColor = Color.white;
    }
}


