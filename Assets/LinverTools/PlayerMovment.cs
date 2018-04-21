using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMode
{
    public string name;
    public float timeFromPrevTap;
    public float speed;
    public Color color;
}

public class PlayerMovment : MonoBehaviour {
    Vector3 _smoothDampVelocity;
    public float smoothDampTime = 2f;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    float maxSpeed = 1f;

    public DanceMode[] dances = new DanceMode[4]
    {
        new DanceMode() {
            name = "idle",
            timeFromPrevTap =  2f,
            speed = 0f,
            color = Color.white
        },
        new DanceMode() {
            name = "slow",
            timeFromPrevTap =  1f,
            speed = 0.5f,
            color = Color.green
        },
        new DanceMode() {
            name = "average",
            timeFromPrevTap =  0.5f,
            speed = 1f,
            color = Color.yellow
        },
        new DanceMode() {
            name = "fast",
            timeFromPrevTap =  0, 
            speed = 2f,
            color = Color.red
        },
    };

    private void Awake()
    {
        dance = dances[0];
    }

    void Update ()
    {
        Dance();
        MovePlayer();
    }

    float timer = 0f;
    DanceMode dance;
    private void Dance()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            for (var i = 0; i < dances.Length; i++)
                if (timer > dances[i].timeFromPrevTap)
                {
                    ApplayDance(dances[i]);
                    break;
                }

            animator.SetTrigger(dance.name);
            timer = 0;
        }

        if (timer > dances[0].timeFromPrevTap)
            ApplayDance(dances[0]);
    }

    private void ApplayDance(DanceMode danceMode)
    {
        dance = danceMode;
        spriteRenderer.color = danceMode.color;
        maxSpeed = danceMode.speed;
    }

    private void MovePlayer()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(transform.position, destination,
            ref _smoothDampVelocity, smoothDampTime, maxSpeed);
    }
}
