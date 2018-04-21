using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {
    public Transform target;
    public float smoothDampTime = 0.2f;
    public float predictDistance = 0.5f;

    Vector3 smoothDampVelocity;
    float sizeVelocity;
    Vector3 destination;

    public void OnEnable()
    {
        if (target == null)
            return;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(destination, 0.1f);
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        var pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination = Vector3.Lerp(pointer, target.position, predictDistance);
        destination.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref smoothDampVelocity, smoothDampTime);
    }

}
