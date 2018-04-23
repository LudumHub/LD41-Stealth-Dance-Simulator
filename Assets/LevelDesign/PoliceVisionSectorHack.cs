using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceVisionSectorHack : MonoBehaviour {
    public Dancer police;
    public Dancer npc;

    static float shift = -30f;
	void Update () {
        transform.position = npc.transform.position;

        var dir = police.transform.position - npc.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + shift;
        var newRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        newRotation.eulerAngles = new Vector3
             (60f, newRotation.eulerAngles.y, newRotation.eulerAngles.z);

        transform.rotation = newRotation;
    }
}
