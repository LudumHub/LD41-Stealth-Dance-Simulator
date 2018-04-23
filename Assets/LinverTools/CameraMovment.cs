using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour {
    public Transform target;
    public float smoothDampTime = 0.2f;
    public float maxDistance = 4;
    public float minSize = 3;
    public float maxSize = 6;

    float sizeDampVelocity;
    Vector3 smoothDampVelocity;
    float sizeVelocity;
    Vector3 destination;
    public bool resetZoom;

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

    float timer = 0;
    public float maxWaitingSecForTap = 3;
    public float minWaitingSecForTap = 0.3f;
    float secWaited = 3f;
    private void Update()
    {
        if (resetZoom)
        {
            secWaited = 3f;
            return;
        }
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            secWaited = Mathf.Max(timer, minWaitingSecForTap);
            timer = 0;
        }

        if (timer > maxWaitingSecForTap)
            secWaited = maxWaitingSecForTap;
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        var pointer = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pointer.z = target.position.z;

        destination = Vector3.Lerp(pointer, target.position, 0.3f);
        var distanceVector = destination - target.position;
        var distance = distanceVector.magnitude;
        if (distance > maxDistance)
            destination = target.position +
                distanceVector.normalized * maxDistance;

        destination.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref smoothDampVelocity, smoothDampTime);

        var zoom = Mathf.Lerp(minSize, maxSize, secWaited / (maxWaitingSecForTap - minWaitingSecForTap));
        Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, zoom, ref sizeVelocity, 0.5f);
    }

}
