using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour {
    public float timePlayed = 0f;

	void Start () {
        DontDestroyOnLoad(this);
	}
	
	void Update () {
        timePlayed += Time.deltaTime;
    }
}
