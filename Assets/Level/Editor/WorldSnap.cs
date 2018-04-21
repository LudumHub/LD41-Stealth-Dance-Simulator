using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSnap : MonoBehaviour {
    Vector3 prevPosition = Vector3.zero;
    void Update () {
        if (prevPosition != transform.position)
            Snap();
	}

    private void Snap()
    {
        transform.position = 

        prevPosition = transform.position;
    }
}
