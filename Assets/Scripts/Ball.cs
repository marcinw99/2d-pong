using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    private float START_X_VELOCITY = 6;
    
    [SerializeField] private Rigidbody2D rigidBody2D;

    private void Start()
    {
        Random random = new Random();

        double yVelocity = (random.NextDouble() * 0.6) - 0.3; // range from -0.3 to 0.3
        rigidBody2D.velocity = new Vector2(START_X_VELOCITY, (float)yVelocity);
    }
}
