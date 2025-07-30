using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;

    private Camera mainCamera;
    [SerializeField] private Rigidbody playerRb;

    private Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        KeepPlayerOnScreen();

        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }

        playerRb.AddForce(movementDirection * forceMagnitude * Time.deltaTime, ForceMode.Force);
        playerRb.velocity = Vector3.ClampMagnitude(playerRb.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.press.isPressed == true)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            ////print(worldPosition);
            ////print(touchPosition);

            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        if (viewPortPosition.x > 1f)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }

        else if (viewPortPosition.x < 0f)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        else if (viewPortPosition.y < 0f)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        else if (viewPortPosition.y > 1f)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }

        transform.position = newPosition;
    }

    private void RotateToFaceVelocity()
    {
        if (playerRb.velocity == Vector3.zero) { return; }

        Quaternion targetRotation = Quaternion.LookRotation(playerRb.velocity, Vector3.back);

        transform.rotation = Quaternion.Lerp(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}