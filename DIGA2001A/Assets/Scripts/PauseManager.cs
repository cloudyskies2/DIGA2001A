using UnityEngine;
using UnityEngine.SceneManagement;

/*
  Title:How To Make a Simple Pause Menu in UNITY
  Author: LewisGamer
  Date: 7 Nov 2023
  Code version: 
  Availability:https://youtu.be/qjgZTVEUqgo?si=E8iQyRM2EabPfGLa
  */

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign your pause menu Canvas here
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);

        // Disable player movement and AI
        ToggleGameComponents(false);

        // Optionally lock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        // Re-enable player movement and AI
        ToggleGameComponents(true);

        // Optionally hide cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ToggleGameComponents(bool state)
    {
        // Example: disable player and enemy scripts manually
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var move = player.GetComponent<FPController>();
            if (move != null) move.enabled = state;
        }

       
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
