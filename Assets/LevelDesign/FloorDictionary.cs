using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDictionary : MonoBehaviour {
    public Dictionary<Vector2, WorldSnap> cells = new Dictionary<Vector2, WorldSnap>();
    public static FloorDictionary instance;

    public void Awake()
    {
        instance = this;
    }

    public void Add(Vector2 vector, WorldSnap cell)
    {
        if (!cells.ContainsKey(vector))
            cells.Add(vector, cell);
    }

    public WorldSnap FindCellByWordspaceCoords(Vector3 coords)
    {
        var key = WorldSnap.GetIsometryCoords(coords);
        if (cells.ContainsKey(key))
            return cells[key];
        else
            return null;
    }
}
