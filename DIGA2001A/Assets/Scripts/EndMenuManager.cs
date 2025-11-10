using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


/// Title: WINNING LEVELS - How to make a Video Game in Unity (E09)
/// Date: 26 Mar 2017
/// Availabiliy: https://youtu.be/Iv7A8TzreY4?si=vQ3utD-d0jANLClA



public class EndMenuManager : MonoBehaviour
{
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game...");
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}

