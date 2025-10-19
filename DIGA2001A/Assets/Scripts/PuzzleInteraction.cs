using UnityEngine;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

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
                interactionPrompt.text = isPuzzleActivated ? "Press 'Q' to open the puzzle" : "Press 'Q' to close the puzzle";
                interactionPrompt.gameObject.SetActive(true);
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
