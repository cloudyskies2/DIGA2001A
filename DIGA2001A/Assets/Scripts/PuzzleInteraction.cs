using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;

public class PuzzleInteraction : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.Q;
    public TextMeshProUGUI interactionPrompt;

    private bool isPlayerInRange = false;
    private bool isPuzzleActivated = false;

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

            if(Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(2);
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
