using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldSnap : MonoBehaviour {
    Vector3 prevPosition = Vector3.zero;
    static Color defaultColor = Color.green;

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
        FloorDictionary.instance.Add(coords, this);

        if (!Application.isPlaying)
            yield return null;

        StartCoroutine(DanceFloor());

        yield return new WaitForSeconds(Mathf.Abs(coords.x + coords.y) * 0.1f);
        while (transform.position != awakeCoords)
        {
            transform.position = Vector3.MoveTowards(transform.position, awakeCoords, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }
    }

    public SpriteRenderer spriteRenderer;
    IEnumerator DanceFloor()
    {
        if (coords.x % 2 == 0)
            yield return new WaitForSeconds(1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            spriteRenderer.color = Color.gray * multColor;
            yield return new WaitForSeconds(1f);
            spriteRenderer.color = Color.white * multColor;
            yield return new WaitForSeconds(1f);
        }
    }

    static float cellSize = 0.6f;
    Vector2 coords;
    private void Snap()
    {
        UpdateCellCoords();

        transform.localPosition = new Vector3(coords.x * cellSize,
            coords.y * cellSize / 2, transform.localPosition.z);

        prevPosition = transform.position;
    }

    private void UpdateCellCoords()
    {
        coords = GetIsometryCoords(transform.localPosition);
    }

    public static Vector2 GetIsometryCoords(Vector3 worldspace)
    {
        var x = GetSnapValue(worldspace.x, cellSize);
        var y = GetSnapValue(worldspace.y, cellSize / 2);

        if ((x + y) % 2 == 0)
            x++;

        return new Vector2(x, y);
    }

    private static int GetSnapValue(float value, float cellsize)
    {
        return Mathf.RoundToInt(value / cellsize);
    }

    [NonSerialized]
    public Color multColor = defaultColor;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (transform.position != awakeCoords) return; //Wait for StartingAnimation ending HACK

        var dancer = other.GetComponent<FloorPainter>();
        if (dancer == null) return;
        if (dancer.tag == "Dancer" || multColor == defaultColor)
        multColor = dancer.DanceStyle.PlayerColor;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var dancer = other.GetComponent<FloorPainter>();
        if (dancer == null) return;
        multColor = defaultColor;
    }
}


