using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static float MAX_Y = 3.1f;

    private float DEFAULT_MOUSE_SENSITIVITY = 10f;

    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform player;

    private void Awake()
    {
        mouseSensitivity = DEFAULT_MOUSE_SENSITIVITY;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        Vector3 currentPosition = player.transform.position;

        float targetY = Player.GetAltitudeRestrictedToPlayground(currentPosition.y + mouseY);

        Vector3 targetPosition = currentPosition;
        targetPosition.y = targetY;

        player.transform.position = targetPosition;
    }

    public static float GetAltitudeRestrictedToPlayground(float input)
    {
        if (Math.Abs(input) > MAX_Y)
        {
            return MAX_Y * Math.Sign(input);
        }

        return input;
    }
}