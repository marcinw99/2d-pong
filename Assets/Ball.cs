using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;

    private void Start()
    {
        rigidBody2D.velocity = new Vector2(3, 0.1f);
    }

    private void Update()
    {
        Debug.Log(rigidBody2D.velocity);
    }
}
