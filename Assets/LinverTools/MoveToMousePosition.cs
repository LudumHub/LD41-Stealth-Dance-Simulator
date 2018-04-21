using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToMousePosition : MonoBehaviour {
    Vector3 _smoothDampVelocity;
    public float smoothDampTime = 2f;
    public float maxSpeed = 1f;

    void Update () {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, destination, 
            ref _smoothDampVelocity, smoothDampTime, maxSpeed);      
    }
}
