using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScipt : MonoBehaviour
{
    public GameObject gameOverUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
    }



    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void quit()
    {
        Application.Quit();
    }


}
