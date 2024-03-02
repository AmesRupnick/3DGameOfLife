using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    private bool isPaused = false;
    InputHandler input;

    public void Update()
    {
        // Check for user input to toggle pause
        if (Input.GetKeyDown(KeyCode.P)) // can use p but have a button so it looks better
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Handle pause and resume actions
        if (isPaused)
        {
            Time.timeScale = 0; // Pause the game
        }
        else
        {
            Time.timeScale = 1; // Resume the game
        }
    }
}
