using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuClickWaiter : MonoBehaviour {
	void Update () {
        if (Input.GetMouseButtonDown(0))
            Fade.instance.LoadNextLevel();
	}
}
