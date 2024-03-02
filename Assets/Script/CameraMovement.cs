using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f; 
    public float zoomSpeed = 2.0f;
    public float minZoom = 1.0f; // Minimum zoom distance
    public float maxZoom = 1000.0f; // Maximum zoom distance

    void Update()
    {
        // Get input from arrow keys or other keys (e.g., WASD)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        // Move the camera based on input
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ZoomIn();
        }

        // Camera Zoom - Zoom Out
        if (Input.GetKeyDown(KeyCode.X))
        {
            ZoomOut();
        }
    }

    void ZoomIn()
    {
        Vector3 zoomDirection = -transform.up * zoomSpeed; //calculates how far to zoom
        transform.Translate(zoomDirection); //zooms out
        
    }

    void ZoomOut()
    {
        Vector3 zoomDirection = transform.up * zoomSpeed; //calculates how far to zoom
        transform.Translate(zoomDirection); //zooms in
        
    }
}

