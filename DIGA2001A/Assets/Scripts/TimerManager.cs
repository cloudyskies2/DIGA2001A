
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    public float timeLimit = 60f; 
    private float currentTime;

    [Header("UI References")]
    public TMP_Text timerText;        
    public GameObject endMenuUI;  

    private bool timerRunning = true;
    private bool goalReached = false;

    void Start()
    {
        currentTime = timeLimit;
        UpdateTimerUI();
    }

    void Update()
    {
        if (!timerRunning || goalReached) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ReachGoal()
    {
        goalReached = true;
        timerRunning = false;
        Debug.Log("Goal Reached!");
    }

    void GameOver()
    {
        timerRunning = false;
        Debug.Log("Time’s up! Game Over!");

        endMenuUI.SetActive(true);

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var move = player.GetComponent<FPController>();
            if (move != null) move.enabled = false;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

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
