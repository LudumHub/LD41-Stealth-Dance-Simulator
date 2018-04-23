using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickWaiter : MonoBehaviour {
    public TimeCounter timeCounter;

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            timeCounter.enabled = true;
            Fade.instance.LoadNextLevel();
        }
	}
}
