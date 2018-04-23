using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ConstantMove : MonoBehaviour {
    public Vector3 direction;
    public float Speed = 1f;
    public float t = 0f;
    public float startingT = -9f;

    private void Awake()
    {
        t = startingT;
    }

    void LateUpdate () {
        transform.position = direction * t;

        if (Application.isPlaying)
            t += Time.deltaTime * Speed;
    }
}
