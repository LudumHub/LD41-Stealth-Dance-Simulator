using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (!Application.isPlaying)
            Snap();
        else
            UpdateCellCoords();
        awakeCoords = transform.position;

        if (Application.isPlaying)
            transform.position = new Vector3(awakeCoords.x, awakeCoords.y - 5, awakeCoords.z);
    }

    IEnumerator Start()
    {
        if (!Application.isPlaying)
            yield return null;

        FloorDictionary.instance.Add(coords, this);
        StartCoroutine(DanceFloor());

        yield return new WaitForSeconds(Mathf.Abs(coords.x + coords.y) * 0.1f);
        while (transform.position != awakeCoords)
        {
            transform.position = Vector3.MoveTowards(transform.position, awakeCoords, Time.deltaTime * 10);
            yield return new WaitForEndOfFrame();
        }
    }

    public SpriteRenderer spriteRenderer;
    static Color grey = new Color(0.9f, 0.9f, 0.9f);
    IEnumerator DanceFloor()
    {
        if (coords.x % 2 == 0)
            yield return new WaitForSeconds(1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            spriteRenderer.color = grey * multColor;
            yield return new WaitForSeconds(1f);
            spriteRenderer.color = Color.white * multColor * 2;
            yield return new WaitForSeconds(1f);
        }
    }

    static float cellSize = 0.6f;
    Vector2 coords;
    private void Snap()
    {
        UpdateCellCoords();

        transform.localPosition = new Vector3(coords.x * cellSize,
            coords.y * cellSize / 2,
            transform.localPosition.z + (coords.x * 0.1f + coords.y * 0.2f));

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

    private FloorPainter staticPainter;
    private readonly List<FloorPainter> movingPainters = new List<FloorPainter>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        var painter = other.GetComponent<FloorPainter>();
        if (painter == null) return;
        var movement = other.GetComponent<Movement>();
        if (movement == null)
            staticPainter = painter;
        else if (!movingPainters.Contains(painter))
            movingPainters.Add(painter);
        UpdateColor();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var painter = other.GetComponent<FloorPainter>();
        if (painter == null) return;
        if (staticPainter == painter)
            staticPainter = null;
        else
            movingPainters.Remove(painter);
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (movingPainters.Any())
            UpdateColor(movingPainters.Last());
        else if (staticPainter != null)
            UpdateColor(staticPainter);
        else
            multColor = defaultColor;
    }

    private void UpdateColor(FloorPainter painter)
    {
        multColor = painter.DanceStyle.PlayerColor;
    }
}


