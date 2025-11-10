using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using System.Collections;

/// Title: How to make AWESOME Scene Transitions in Unity!
/// Author: Brackeys
/// Date: 12 January 2020
/// Code version: Unity 2019.3.0f3
/// Availabiliy: https://www.youtube.com/watch?v=CE9VOZivb3I
/// 

public class PuzzleInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.Q;
    public TextMeshProUGUI interactionPrompt;

    private bool isPlayerInRange = false;
    private bool isPuzzleActivated = false;

    //public Animator sceneTransition;
    //public float transitionTime;

    void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (interactionPrompt != null)
            {
                interactionPrompt.text = isPuzzleActivated ? "Congragulations on completing the puzzle!" +
                    "Press 'Q' to close the puzzle" : "Press 'Q' to open the puzzle";
                interactionPrompt.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                LoadNextLevel(); 
            }
        }
        else
        {
            if (interactionPrompt != null)
            {
                interactionPrompt.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //sceneTransition.SetTrigger("Start");

        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(1);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactionPrompt.text = isPuzzleActivated ? "Press 'Q' to open the puzzle" : "Press 'Q' to close the puzzle";
            interactionPrompt.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;

        }
    }
}
