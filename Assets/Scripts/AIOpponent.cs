using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    private float MOVEMENT_VELOCITY = 8f;

    [SerializeField] private Transform aiOpponent;
    [SerializeField] private Transform ball;


    void Update()
    {
        MoveToBallAltitude();
    }

    void MoveToBallAltitude()
    {
        float ballAltitude = ball.transform.position.y;

        float aiOpponentAltitude = aiOpponent.transform.position.y;

        float directionBasedAltitudeChange = 0f;

        if (ballAltitude > aiOpponentAltitude)
        {
            directionBasedAltitudeChange = MOVEMENT_VELOCITY;
        }
        else
        {
            directionBasedAltitudeChange = MOVEMENT_VELOCITY * -1;
        }

        Vector3 currentPosition = aiOpponent.transform.position;

        float targetY = Player.GetAltitudeRestrictedToPlayground(currentPosition.y + directionBasedAltitudeChange * Time.deltaTime);

        Vector3 targetPosition = currentPosition;
        targetPosition.y = targetY;

        aiOpponent.transform.position = targetPosition;
    }
}