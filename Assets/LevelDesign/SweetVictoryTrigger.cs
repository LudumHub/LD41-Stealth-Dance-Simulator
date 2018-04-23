using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetVictoryTrigger : MonoBehaviour {
    public RectTransform LoveMeter;
    public Transform Player;

    float startDistance;
    private void Awake()
    {
        startDistance = DistanceToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
            Victory();
    }

    private void Update()
    {
        LoveMeter.localScale = new Vector3(
            (1 - DistanceToPlayer() / startDistance) * 100f, //IT'S MAGIC
            LoveMeter.localScale.y,
            LoveMeter.localScale.z
            );
    }

    private float DistanceToPlayer()
    {
        return (Player.transform.position - transform.position).magnitude;
    }

    private void Victory()
    {
        throw new NotImplementedException("YOU~WON. Boss say: I like you!");
    }
}
