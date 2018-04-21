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

[RequireComponent(typeof(Movement))]
public class PlayerMovment : MonoBehaviour
{
    private Movement movement;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

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
        dance = 0;
        movement = GetComponent<Movement>();
    }

    void Update ()
    {
        Dance();
        MovePlayer();
    }

    float timer = 0f;
    int dance;
    private void Dance()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
        {
            for (var i = 1; i < dances.Length; i++)
                if (timer > dances[i].timeFromPrevTap)
                {
                    ApplayDance(i);
                    break;
                }

            animator.SetTrigger(dances[dance].name);
            timer = 0;
        }

        if (timer > Mathf.Max(dances[dance].timeFromPrevTap * 2, 0.5f)
            && dance > 0)
            ApplayDance(dance-1);
    }

    private void ApplayDance(int danceId)
    {
        var danceMode = dances[danceId];
        dance = danceId;
        spriteRenderer.color = danceMode.color;
        movement.MaxSpeed = danceMode.speed;
    }

    private void MovePlayer()
    {
        var destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = transform.position.z;
        movement.Destination = destination;
    }
}